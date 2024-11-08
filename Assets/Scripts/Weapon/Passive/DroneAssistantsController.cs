using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAssistantsController : MonoBehaviour
{
    private DroneAssistants _droneAssistants;
    private Transform player;
    [SerializeField] private GameObject prefab;
    List<GameObject> allDrones = new List<GameObject>();
    private int maxWeaponLevel;
    private int weaponLevel = 0;
    //место для ссылки на объект пассивного оружия иконка
    private GameObject iconPassiveWeapon;

    public static event Action<string> OnLevelUp;
    public static DroneAssistantsController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        GetInfo(PassiveWeaponClasses.Instance.DroneAssistants);
        maxWeaponLevel = PassiveWeaponClasses.Instance.MaxWeaponLevel;

        //созданием запросить ссылку на объект
        iconPassiveWeapon = LevelController.Instance.GetPassiveWeaponIcon();
        //iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().FillInfo();
    }
    static private DroneAssistantsController _instance;

    private void GetInfo(DroneAssistants droneAssistants)
    {
        _droneAssistants = droneAssistants;
        player = GameObject.Find("Player").transform;
        CalculateParamPassiveWeapon(DictionaryPassiveWeapon.Instance.GetWeaponLevelByKey(_droneAssistants.Name));
        CreateDroneAssistants(_droneAssistants.Amount);
    }
    private void CalculateParamPassiveWeapon(int levelWeapon)//для прокачки вне уровня
    {
        ///как улучшать
    }
    public void ChangeParamPassiveWeapon()
    {
        weaponLevel++;
        OnLevelUp?.Invoke(_droneAssistants.Name);
        //Debug.Log("weaponLevel "+ weaponLevel);
        //меняю параметры экземпляра
        _droneAssistants.Damage += _droneAssistants.Damage * 0.1f;
        _droneAssistants.Speed += _droneAssistants.Speed * 0.05f;
        _droneAssistants.CoolDown -= _droneAssistants.CoolDown * 0.05f;
        if(weaponLevel % 2 == 0)
        {
            _droneAssistants.Amount++;
        }

        CreateDroneAssistants(_droneAssistants.Amount);


        if (weaponLevel == maxWeaponLevel)
        {
            UpgradePassiveWeapon.Instance.ReachingMaxLevelWeapon(_droneAssistants.Name);
        }

        //у объекта вызываем скрипт PassiveWeaponSlot, вызываем функцию меняния
        iconPassiveWeapon.GetComponent<PassiveWeaponSlot>().ShowLevel();
    }
    private void CreateDroneAssistants(int amount)
    {
        // Удаляем существующие дроны перед созданием новых
        foreach (var drone in allDrones)
        {
            Destroy(drone);
        }
        // Очищаем список, чтобы удалить ссылки на уничтоженные объекты
        allDrones.Clear();

        float radius = 3.0f; // Радиус круга, вокруг которого будут создаваться дроны
        for (int i = 0; i < amount; i++)
        {

            // Рассчитываем угол для каждого дрона, чтобы равномерно распределить их вокруг игрока
            float angle = i * (360 / _droneAssistants.Amount) * Mathf.Deg2Rad;

            // Вычисляем смещение относительно игрока на основе угла и радиуса
            Vector3 offset = new Vector3(Mathf.Sin(angle) * radius, 3, Mathf.Cos(angle) * radius);
            Vector3 location = player.transform.position + offset + new Vector3(0, 2, 0);

            GameObject drone = Instantiate(prefab, location, Quaternion.identity);
            allDrones.Add(drone);
           // drone.GetComponent<DroneAssistantsObject>().FillInfo(_droneAssistants);
        }
        foreach(var drone in allDrones)
        {
            drone.GetComponent<DroneAssistantsObject>().FillInfo(_droneAssistants, allDrones);
        }
        
    }
}
