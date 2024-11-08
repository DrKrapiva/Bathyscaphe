using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesController : MonoBehaviour
{
    private Mines _mines;
    private Transform player;
    [SerializeField] private GameObject prefab;
    private Coroutine coroutineMines;
    private int _amount;
    private int maxWeaponLevel;
    private int weaponLevel = 0;
    //место дл€ ссылки на объект пассивного оружи€ иконка
    private GameObject iconPassiveWeapon;

    public static event Action<string> OnLevelUp;
    public static MinesController Instance
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
        GetInfo(PassiveWeaponClasses.Instance.Mines);
        maxWeaponLevel = PassiveWeaponClasses.Instance.MaxWeaponLevel;

        //созданием запросить ссылку на объект
        iconPassiveWeapon = LevelController.Instance.GetPassiveWeaponIcon();
        //iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().FillInfo();
    }
    static private MinesController _instance;

    private void GetInfo(Mines mines)
    {
        _mines = mines;
        _amount = _mines.Amount;
        CalculateParamPassiveWeapon(DictionaryPassiveWeapon.Instance.GetWeaponLevelByKey(_mines.Name));
        StartGenerateMines();
    }
    private void CalculateParamPassiveWeapon(int levelWeapon)//дл€ прокачки вне уровн€
    {
        ///как улучшать
    }
    public void ChangeParamPassiveWeapon()
    {
        weaponLevel++;
        OnLevelUp?.Invoke(_mines.Name);
        //мен€ю параметры экземпл€ра
        _mines.RechargeSpeed -= _mines.RechargeSpeed * 0.1f;
        _mines.Damage += _mines.Damage * 0.1f;
        if (weaponLevel % 2 == 0)
        {
            _mines.Amount++;
        }

        StartGenerateMines();

        if (weaponLevel == maxWeaponLevel)
        {
            UpgradePassiveWeapon.Instance.ReachingMaxLevelWeapon(_mines.Name);
        }

        //у объекта вызываем скрипт PassiveWeaponSlot, вызываем функцию мен€ни€
        iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().ShowLevel();
    }

    public void ChangeAmount()
    {
        if (_amount < 2)
            _amount++;
        else _amount = 2;
        //Debug.Log(_amount + " ChangeAmount ++");
    }
    private void StartGenerateMines()
    {
        if (coroutineMines != null)
            StopCoroutine(coroutineMines);
        coroutineMines = StartCoroutine(Mines());
    }
    
    IEnumerator Mines()
    {
        for (; ; )
        {
            //Debug.Log(_amount + " --");
            if (_amount > 0)
            {
                _amount--;
                Vector3 playerPosition = new Vector3(player.transform.position.x, 1, player.transform.position.z);

                GameObject mine = Instantiate(prefab, playerPosition, Quaternion.identity);
                mine.GetComponent<MinesObject>().FillInfo(_mines);

                
            }
            yield return new WaitForSeconds(_mines.RechargeSpeed);
        }
    }
}
