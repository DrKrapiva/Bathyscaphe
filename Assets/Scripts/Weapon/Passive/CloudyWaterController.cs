using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudyWaterController : MonoBehaviour
{
    private CloudyWater _cloudyWater;
    private Transform player;
    [SerializeField] private GameObject prefab;
    private Coroutine coroutineGenerateCloudyWater;
    private int maxWeaponLevel;
    private int weaponLevel = 0;
    //место для ссылки на объект пассивного оружия иконка
    private GameObject iconPassiveWeapon;
    private GameObject currentCloudyWater;

    public static event Action<string> OnLevelUp;
    public static CloudyWaterController Instance
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
        GetInfo(PassiveWeaponClasses.Instance.CloudyWater);
        maxWeaponLevel = PassiveWeaponClasses.Instance.MaxWeaponLevel;

        //созданием запросить ссылку на объект
        iconPassiveWeapon = LevelController.Instance.GetPassiveWeaponIcon();
        //iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().FillInfo();
    }
    static private CloudyWaterController _instance;

    private void GetInfo(CloudyWater cloudyWater)
    {
        _cloudyWater = cloudyWater;
        CalculateParamPassiveWeapon(DictionaryPassiveWeapon.Instance.GetWeaponLevelByKey(_cloudyWater.Name));
        StartGenerateCloudyWater();
    }
    private void CalculateParamPassiveWeapon(int levelWeapon)//для прокачки вне уровня
    {
        ///как улучшать
    }
    public void ChangeParamPassiveWeapon()
    {
        weaponLevel++;
        OnLevelUp?.Invoke(_cloudyWater.Name);
        //меняю параметры экземпляра
        _cloudyWater.SlowingDownEnemies += _cloudyWater.SlowingDownEnemies * 0.05f;
        _cloudyWater.Duration += _cloudyWater.Duration * 0.05f;

        // Немедленно уничтожить текущее CloudyWater
        DestroyCurrentCloudyWater();

        // Запустить генерацию нового CloudyWater
        StartGenerateCloudyWater();

        if (weaponLevel == maxWeaponLevel)
        {
            UpgradePassiveWeapon.Instance.ReachingMaxLevelWeapon(_cloudyWater.Name);
        }

        //у объекта вызываем скрипт PassiveWeaponSlot, вызываем функцию меняния
        iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().ShowLevel();
    }
    private void DestroyCurrentCloudyWater()
    {
        if (currentCloudyWater != null)
        {
            Destroy(currentCloudyWater);
        }
    }
    private void StartGenerateCloudyWater()
    {
        if (coroutineGenerateCloudyWater != null)
            StopCoroutine(coroutineGenerateCloudyWater);
        coroutineGenerateCloudyWater = StartCoroutine(GenerateCloudyWater());
    }
    IEnumerator GenerateCloudyWater()
    {
        for (; ; )
        {
            CloudyWater();

            yield return new WaitForSeconds(_cloudyWater.RechargeSpeed);
        }
    }

    private void CloudyWater()
    {
        currentCloudyWater = Instantiate(prefab, player, false);
        currentCloudyWater.GetComponent<CloudyWaterObject>().FillInfo(_cloudyWater);
    }
}
