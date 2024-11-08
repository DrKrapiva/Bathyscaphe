using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationController : MonoBehaviour
{
    private Regeneration _regeneration;
    private int maxWeaponLevel;
    private int weaponLevel = 0;
    //место дл€ ссылки на объект пассивного оружи€ иконка
    private GameObject iconPassiveWeapon;

    public static event Action<string> OnLevelUp;
    public static RegenerationController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        GetInfo(PassiveWeaponClasses.Instance.Regeneration);
        maxWeaponLevel = PassiveWeaponClasses.Instance.MaxWeaponLevel;

        //созданием запросить ссылку на объект
        iconPassiveWeapon = LevelController.Instance.GetPassiveWeaponIcon();
        //iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().FillInfo();
    }
    static private RegenerationController _instance;

    private void GetInfo(Regeneration regeneration)
    {
        _regeneration = regeneration;
        CalculateParamPassiveWeapon(DictionaryPassiveWeapon.Instance.GetWeaponLevelByKey(_regeneration.Name));
        StartRegeneration();
    }
    private void CalculateParamPassiveWeapon(int levelWeapon)//дл€ прокачки вне уровн€
    {
        ///как улучшать
    }
    public void ChangeParamPassiveWeapon()
    {
        weaponLevel++;
        OnLevelUp?.Invoke(_regeneration.Name);
        //мен€ю параметры экземпл€ра
        _regeneration.HealHp += _regeneration.HealHp * 0.1f;

        StartRegeneration();

        if (weaponLevel == maxWeaponLevel)
        {
            UpgradePassiveWeapon.Instance.ReachingMaxLevelWeapon(_regeneration.Name);
        }

        //у объекта вызываем скрипт PassiveWeaponSlot, вызываем функцию мен€ни€
        iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().ShowLevel();
    }
    private void StartRegeneration()
    {
        HeroHPController.Instance.StartRegeniration(_regeneration.HealHp);
    }
}
