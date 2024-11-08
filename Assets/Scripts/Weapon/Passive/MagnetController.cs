using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MagnetController : MonoBehaviour
{
    private Magnet _magnet;
    private Coroutine coroutineGenerateMagnet;
    private Transform player;
    [SerializeField] private GameObject prefab;
    private int maxWeaponLevel;
    private int weaponLevel = 0;
    //место для ссылки на объект пассивного оружия иконка
    private GameObject iconPassiveWeapon;
    private GameObject currentMagnet;

    public static event Action<string> OnLevelUp;
    public static MagnetController Instance
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
        GetInfo(PassiveWeaponClasses.Instance.Magnet);
        maxWeaponLevel = PassiveWeaponClasses.Instance.MaxWeaponLevel;

        //созданием запросить ссылку на объект
        iconPassiveWeapon = LevelController.Instance.GetPassiveWeaponIcon();
        //iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().FillInfo();
    }

    static private MagnetController _instance;
    
    private void GetInfo(Magnet magnet)
    {
        _magnet = magnet;
        CalculateParamPassiveWeapon(DictionaryPassiveWeapon.Instance.GetWeaponLevelByKey(_magnet.Name));
        StartCoroutineGenerateMagnet();
    }
    private void CalculateParamPassiveWeapon(int levelWeapon)//для прокачки вне уровня
    {
        ///как улучшать
    }
    public void ChangeParamPassiveWeapon()
    {
        weaponLevel++;
        OnLevelUp?.Invoke(_magnet.Name);
        //меняю параметры экземпляра
        _magnet.Size += _magnet.Size * 0.05f;
        _magnet.RechargeSpeed -= _magnet.RechargeSpeed * 0.05f;

        // Немедленно уничтожить текущее currentMagnet
        DestroyCurrentMagnet();

        // Запустить генерацию нового currentMagnet
        StartCoroutineGenerateMagnet();

        if (weaponLevel == maxWeaponLevel)
        {
            UpgradePassiveWeapon.Instance.ReachingMaxLevelWeapon(_magnet.Name);
        }

        //у объекта вызываем скрипт PassiveWeaponSlot, вызываем функцию меняния
        iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().ShowLevel();
    }
    private void DestroyCurrentMagnet()
    {
        if (currentMagnet != null)
        {
            Destroy(currentMagnet);
        }
    }
    public void StartCoroutineGenerateMagnet()
    {
        if (coroutineGenerateMagnet != null)
            StopCoroutine(coroutineGenerateMagnet);
        coroutineGenerateMagnet = StartCoroutine(GenerateMagnet());
    }
    IEnumerator GenerateMagnet()
    {

        for (; ; )
        {
            currentMagnet = Instantiate(prefab, player, false);

            // Устанавливаем высоту объекта на -1.8
            Vector3 newPosition = currentMagnet.transform.localPosition;
            newPosition.y = -1.8f;
            currentMagnet.transform.localPosition = newPosition;

            currentMagnet.GetComponent<MagnetObject>().FillInfo(_magnet);

            yield return new WaitForSeconds(_magnet.RechargeSpeed);
        }
    }
}
