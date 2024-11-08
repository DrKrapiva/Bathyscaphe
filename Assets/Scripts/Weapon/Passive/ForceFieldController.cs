using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldController : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject prefab;
    private ForceField _forceField;
    private Coroutine coroutineGenerateForceField;
    private int maxWeaponLevel;
    private int weaponLevel = 0;
    //место для ссылки на объект пассивного оружия иконка
    private GameObject iconPassiveWeapon;
    private GameObject currentForceField;

    public static event Action<string> OnLevelUp;
    public static ForceFieldController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        player = GameObject.Find("Player").transform;
        GetInfo(PassiveWeaponClasses.Instance.ForceField);
        maxWeaponLevel = PassiveWeaponClasses.Instance.MaxWeaponLevel;

        //созданием запросить ссылку на объект
        iconPassiveWeapon = LevelController.Instance.GetPassiveWeaponIcon();
        //iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().FillInfo();
    }
    static private ForceFieldController _instance;
    
    public void GetInfo(ForceField forceField)
    {
        _forceField = forceField;
        CalculateParamPassiveWeapon(DictionaryPassiveWeapon.Instance.GetWeaponLevelByKey(_forceField.Name));
        StartCoroutineGenerateForceField();
    }
    private void CalculateParamPassiveWeapon(int levelWeapon)//для прокачки вне уровня
    {
        ///как улучшать
    }
    public void ChangeParamPassiveWeapon()
    {
        weaponLevel++;
        OnLevelUp?.Invoke(_forceField.Name);
        //меняю параметры экземпляра
        _forceField.Size += _forceField.Size * 0.05f;
        _forceField.Hp++;
        _forceField.RechargeSpeed -= _forceField.RechargeSpeed * 0.1f;

        // Немедленно уничтожить текущее силовое поле
        DestroyCurrentForceField();

        // Запустить генерацию нового силового поля
        StartCoroutineGenerateForceField();

        if (weaponLevel == maxWeaponLevel)
        {
            UpgradePassiveWeapon.Instance.ReachingMaxLevelWeapon(_forceField.Name);
        }

        //у объекта вызываем скрипт PassiveWeaponSlot, вызываем функцию меняния
        iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().ShowLevel();
    }
    private void DestroyCurrentForceField()
    {
        if (currentForceField != null)
        {
            Destroy(currentForceField);
        }
    }
    public void StartCoroutineGenerateForceField()
    {
        if(coroutineGenerateForceField != null)
            StopCoroutine(coroutineGenerateForceField);
        coroutineGenerateForceField = StartCoroutine(GenerateForceField());
    }
    IEnumerator GenerateForceField()
    {
        yield return new WaitForSeconds(_forceField.RechargeSpeed);

        
        currentForceField = Instantiate(prefab, player, false);
        currentForceField.GetComponent<ForceFieldObject>().FillInfo(_forceField);
    }
    
}
