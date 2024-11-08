using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DischargeElectricityController : MonoBehaviour
{
    private DischargeElectricity _dischargeElectricity;
    private Transform player;
    [SerializeField] private GameObject prefab;
    private Coroutine coroutineGenerateElectricity;
    private int maxWeaponLevel;
    private int weaponLevel = 0;
    //место для ссылки на объект пассивного оружия иконка
    private GameObject iconPassiveWeapon;
    private GameObject currentDischargeElectricity;

    public static event Action<string> OnLevelUp;
    public static DischargeElectricityController Instance
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
        GetInfo(PassiveWeaponClasses.Instance.DischargeElectricity);
        maxWeaponLevel = PassiveWeaponClasses.Instance.MaxWeaponLevel;

        //созданием запросить ссылку на объект
        iconPassiveWeapon = LevelController.Instance.GetPassiveWeaponIcon();
        //iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().FillInfo();
    }
    static private DischargeElectricityController _instance;

    private void GetInfo(DischargeElectricity dischargeElectricity)
    {
        _dischargeElectricity = dischargeElectricity;
        CalculateParamPassiveWeapon(DictionaryPassiveWeapon.Instance.GetWeaponLevelByKey(_dischargeElectricity.Name));
        StartGenerateElectricity();
    }
    private void CalculateParamPassiveWeapon(int levelWeapon)//для прокачки вне уровня
    {
        ///как улучшать
    }
    public void ChangeParamPassiveWeapon()
    {
        weaponLevel++;
        OnLevelUp?.Invoke(_dischargeElectricity.Name);
        //меняю параметры экземпляра
        _dischargeElectricity.Duration += _dischargeElectricity.Duration * 0.05f;

        // Немедленно уничтожить текущее currentDischargeElectricity
        DestroyCurrentDischargeElectricity();

        // Запустить генерацию нового currentDischargeElectricity
        StartGenerateElectricity();

        if (weaponLevel == maxWeaponLevel)
        {
            UpgradePassiveWeapon.Instance.ReachingMaxLevelWeapon(_dischargeElectricity.Name);
        }

        //у объекта вызываем скрипт PassiveWeaponSlot, вызываем функцию меняния
        iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().ShowLevel();
    }
    private void DestroyCurrentDischargeElectricity()
    {
        if (currentDischargeElectricity != null)
        {
            Destroy(currentDischargeElectricity);
        }
    }
    private void StartGenerateElectricity()
    {
        if(coroutineGenerateElectricity != null)
            StopCoroutine(coroutineGenerateElectricity);
        coroutineGenerateElectricity = StartCoroutine(GenerateElectricity());
    }
    IEnumerator GenerateElectricity()
    {
        for(; ; )
        {
            Electricity();

            yield return new WaitForSeconds(_dischargeElectricity.RechargeSpeed);
        }
    }

    private void Electricity()
    {
        currentDischargeElectricity = Instantiate(prefab, player, false);
        currentDischargeElectricity.GetComponent<DischargeElectricityObject>().FillInfo(_dischargeElectricity);
    }
}
