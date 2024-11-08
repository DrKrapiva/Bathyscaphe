using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationController : MonoBehaviour
{
    private Regeneration _regeneration;
    private int maxWeaponLevel;
    private int weaponLevel = 0;
    //����� ��� ������ �� ������ ���������� ������ ������
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

        //��������� ��������� ������ �� ������
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
    private void CalculateParamPassiveWeapon(int levelWeapon)//��� �������� ��� ������
    {
        ///��� ��������
    }
    public void ChangeParamPassiveWeapon()
    {
        weaponLevel++;
        OnLevelUp?.Invoke(_regeneration.Name);
        //����� ��������� ����������
        _regeneration.HealHp += _regeneration.HealHp * 0.1f;

        StartRegeneration();

        if (weaponLevel == maxWeaponLevel)
        {
            UpgradePassiveWeapon.Instance.ReachingMaxLevelWeapon(_regeneration.Name);
        }

        //� ������� �������� ������ PassiveWeaponSlot, �������� ������� �������
        iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().ShowLevel();
    }
    private void StartRegeneration()
    {
        HeroHPController.Instance.StartRegeniration(_regeneration.HealHp);
    }
}
