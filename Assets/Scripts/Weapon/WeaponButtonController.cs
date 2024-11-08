using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButtonController : MonoBehaviour
{
    private List<string> weaponNames;
    [SerializeField] private Button[] buttonWeapon;
    //�������� �������
    public static WeaponButtonController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    static private WeaponButtonController _instance;
    private void Start()
    {
        LoadListWeaponNames();


        for (int i = 0; i < buttonWeapon.Length; i++)
        {
            int index = i;

            if (ActivWeapon.Instance.CheckIfKeyExists(weaponNames[index]))
            {
                //Debug.Log("exist");
                buttonWeapon[i].onClick.AddListener(() => SetPrefab(weaponNames[index]));
                string key = ActivWeapon.Instance.GetNameWeaponRarity(weaponNames[index]);
                float coolDown = ActivWeapon.Instance.GetActiveWeaponInfo(key).cooldawn;
                int countBullet = ActivWeapon.Instance.GetActiveWeaponInfo(key).countBullet;
               // Debug.Log(weaponNames[index]);
                buttonWeapon[i].gameObject.GetComponent<ButtonShoot>().FillInfo(weaponNames[index], coolDown, countBullet);
                
                buttonWeapon[i].gameObject.tag = weaponNames[index];
            }
            else Debug.Log("not exist");
            
        }
    }
    private void LoadListWeaponNames()
    {
        weaponNames = SaveGame.Instance.LoadSelectedWeaponNames();
    }


    private void SetPrefab(string weaponName)
    {
        // ��������� ����� � ���������
        string longWeaponName = SaveWeaponUpgrade.Instance.GetWeaponLevelRarity(weaponName);

        ActivWeapon.Instance.CreateWeapon(Resources.Load<GameObject>("Prefab/Bullet/" + longWeaponName), weaponName);

        
    }

    public void ChangeCoolDown(int decreaseCooldown, string buttonWeaponName)//���� � ������ ������������ buttonWeaponName, ��� ��� ��������� �������� ������ � ������� ������
    {
        GameObject[] buttonWithTag = GameObject.FindGameObjectsWithTag(buttonWeaponName);

        foreach(GameObject button in buttonWithTag)
        {
            button.GetComponent<ButtonShoot>().ChangeCoolDown(decreaseCooldown);
        }
    }

}
