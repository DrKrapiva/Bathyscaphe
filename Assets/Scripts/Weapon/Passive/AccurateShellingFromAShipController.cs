using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccurateShellingFromAShipController : MonoBehaviour
{
    private AccurateShellingFromAShip _accurateShellingFromAShip;
    private Transform player;
    [SerializeField] private GameObject prefab;
    private Coroutine coroutineGenerateShellingFromAShip;
    private int maxWeaponLevel;
    private int weaponLevel = 0;
    //место для ссылки на объект пассивного оружия иконка
    private GameObject iconPassiveWeapon;
    private GameObject currentAccurateShellingFromAShip;

    public static event Action<string> OnLevelUp;

    public static AccurateShellingFromAShipController Instance
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
        GetInfo(PassiveWeaponClasses.Instance.AccurateShellingFromAShip);
        maxWeaponLevel = PassiveWeaponClasses.Instance.MaxWeaponLevel;

        //созданием запросить ссылку на объект
        iconPassiveWeapon = LevelController.Instance.GetPassiveWeaponIcon();
        //iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().FillInfo();
    }
    static private AccurateShellingFromAShipController _instance;
    
    private void GetInfo(AccurateShellingFromAShip accurateShellingFromAShip)
    {
        _accurateShellingFromAShip = accurateShellingFromAShip;
        CalculateParamPassiveWeapon(DictionaryPassiveWeapon.Instance.GetWeaponLevelByKey(_accurateShellingFromAShip.Name));
        StartCoroutineGenerateShellingFromAShip();
    }
    private void CalculateParamPassiveWeapon(int levelWeapon)//для прокачки вне уровня
    {
        ///как улучшать
    }
    public void ChangeParamPassiveWeapon()
    {
        weaponLevel++;

        OnLevelUp?.Invoke(_accurateShellingFromAShip.Name);
        //меняю параметры экземпляра
        _accurateShellingFromAShip.RechargeSpeed -= _accurateShellingFromAShip.RechargeSpeed * 0.05f;
        _accurateShellingFromAShip.Damage += _accurateShellingFromAShip.Damage * 0.1f;
        if (weaponLevel % 2 == 0)
        {
            _accurateShellingFromAShip.Amount++;
        }
        // Немедленно уничтожить текущее currentAccurateShellingFromAShip
        DestroyCurrentAccurateShellingFromAShip();

        // Запустить генерацию нового currentAccurateShellingFromAShip
        StartCoroutineGenerateShellingFromAShip();

        if(weaponLevel == maxWeaponLevel)
        {
            UpgradePassiveWeapon.Instance.ReachingMaxLevelWeapon(_accurateShellingFromAShip.Name);
        }
        //у объекта вызываем скрипт PassiveWeaponSlot, вызываем функцию меняния
        iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().ShowLevel();
    }
    private void DestroyCurrentAccurateShellingFromAShip()
    {
        if (currentAccurateShellingFromAShip != null)
        {
            Destroy(currentAccurateShellingFromAShip);
        }
    }
    private void StartCoroutineGenerateShellingFromAShip()
    {
        if(coroutineGenerateShellingFromAShip != null)
             StopCoroutine(coroutineGenerateShellingFromAShip);
        coroutineGenerateShellingFromAShip = StartCoroutine(GenerateShellingFromAShip());
    }
    IEnumerator GenerateShellingFromAShip()
    {
        for (; ; )
        {
            currentAccurateShellingFromAShip = Instantiate(prefab, player, false);
            currentAccurateShellingFromAShip.GetComponent<AccurateShellingFromAShipObject>().FillInfo(_accurateShellingFromAShip);

            yield return new WaitForSeconds(_accurateShellingFromAShip.RechargeSpeed);
        }
    }

}
