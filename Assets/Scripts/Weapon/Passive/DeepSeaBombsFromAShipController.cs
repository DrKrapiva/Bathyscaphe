using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeepSeaBombsFromAShipController : MonoBehaviour
{
    private DeepSeaBombFromAShip _deepSeaBombFromAShip;
    private Transform player;
    [SerializeField] private GameObject prefab;
    private Coroutine coroutineGenerateBombs;
    private float minDistanceSpawnObject = 5f;
    private float maxDistanceSpawnObject;
    private int maxWeaponLevel;
    private int weaponLevel = 0;
    //����� ��� ������ �� ������ ���������� ������ ������
    private GameObject iconPassiveWeapon;


    public static event Action<string> OnLevelUp;
    public static DeepSeaBombsFromAShipController Instance
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
        GetInfo(PassiveWeaponClasses.Instance.DeepSeaBombFromAShip);
        maxWeaponLevel = PassiveWeaponClasses.Instance.MaxWeaponLevel;

        //��������� ��������� ������ �� ������
        iconPassiveWeapon = LevelController.Instance.GetPassiveWeaponIcon();
        //iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().FillInfo();
    }
    static private DeepSeaBombsFromAShipController _instance;

    private void GetInfo(DeepSeaBombFromAShip deepSeaBombFromAShip)
    {
        _deepSeaBombFromAShip = deepSeaBombFromAShip;
        maxDistanceSpawnObject = _deepSeaBombFromAShip.MaxDistance;
        CalculateParamPassiveWeapon(DictionaryPassiveWeapon.Instance.GetWeaponLevelByKey(_deepSeaBombFromAShip.Name));
        StartCoroutineGenerateBombs();
    }
    private void CalculateParamPassiveWeapon(int levelWeapon)//��� �������� ��� ������
    {
        ///��� ��������
    }
    public void ChangeParamPassiveWeapon()
    {
        weaponLevel++;
        OnLevelUp?.Invoke(_deepSeaBombFromAShip.Name);
        //����� ��������� ����������
        _deepSeaBombFromAShip.Size += _deepSeaBombFromAShip.Size * 0.05f;
        _deepSeaBombFromAShip.Damage += _deepSeaBombFromAShip.Damage * 0.1f;
        if (weaponLevel % 2 == 0)
        {
            _deepSeaBombFromAShip.Amount++;
        }


        StartCoroutineGenerateBombs();

        if (weaponLevel == maxWeaponLevel)
        {
            UpgradePassiveWeapon.Instance.ReachingMaxLevelWeapon(_deepSeaBombFromAShip.Name);
        }

        //� ������� �������� ������ PassiveWeaponSlot, �������� ������� �������
        iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().ShowLevel();
    }
    public void StartCoroutineGenerateBombs()
    {
        if(coroutineGenerateBombs != null)
            StopCoroutine(coroutineGenerateBombs);
        coroutineGenerateBombs = StartCoroutine(GenerateBombs());
    }
    IEnumerator GenerateBombs()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(_deepSeaBombFromAShip.RechargeSpeed);

            SpawnObjectNearPlayer(_deepSeaBombFromAShip.Amount);
        }
    }
    private void SpawnObjectNearPlayer(int amount)
    {
        
        // ������� ������
        for (int i = 0; i < amount; i++)
        {
            // �������� ��������� ���������� � �������� ��������
            float distance = UnityEngine.Random.Range(minDistanceSpawnObject, maxDistanceSpawnObject);

            // �������� ��������� ����������� ������ �� ���� X � Z
            Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;

            // ��������� ������� ��� �������� �������
            Vector3 spawnPosition = player.transform.position + randomDirection * distance;

            // ������������� ������ Y ������ ������ ������ (��� ����� ������ ����������� ������)
           //spawnPosition.y = player.transform.position.y;
            spawnPosition.y = 0.5f;

            GameObject bomb = Instantiate(prefab, spawnPosition, Quaternion.identity);
            bomb.GetComponent<DeepSeaBombsFromAShipObject>().FillInfo(_deepSeaBombFromAShip);
            
        }
    }
}
