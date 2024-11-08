using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Reflection;
using System;

public class PassiveWeaponClasses : MonoBehaviour
{
    private int maxWeaponLevel = 10;
    private ForceField forceField;
    private DeepSeaBombFromAShip deepSeaBombFromAShip;
    private DischargeElectricity dischargeElectricity;
    private AccurateShellingFromAShip accurateShellingFromAShip;
    private Magnet magnet;
    private CloudyWater cloudyWater;
    private Mines mines;
    private Regeneration regeneration;
    private DroneAssistants droneAssistants;
    public int MaxWeaponLevel { get { return maxWeaponLevel; } }
    public ForceField ForceField { get { return forceField; } }
    public DeepSeaBombFromAShip DeepSeaBombFromAShip { get { return deepSeaBombFromAShip; } }
    public DischargeElectricity DischargeElectricity { get { return dischargeElectricity; } }  
    public AccurateShellingFromAShip AccurateShellingFromAShip { get { return accurateShellingFromAShip; } }
    public Magnet Magnet { get { return magnet; } }
    public CloudyWater CloudyWater { get { return cloudyWater; } }
    public Mines Mines { get { return mines; } }   
    public Regeneration Regeneration { get { return regeneration; } }
    public DroneAssistants DroneAssistants { get { return droneAssistants; } }
    [SerializeField] private Transform parent;
    public static PassiveWeaponClasses Instance
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
    static private PassiveWeaponClasses _instance;

    private void Start()
    {
        float heroSpeed = DictionaryHero.Instance.Speed(PlayerPrefs.GetString("NowHero"));
        float droneSpeed = heroSpeed * 1.3f;


        forceField = new ForceField() { Name = "ForceField", Size = 4f, Hp = 3f, EnemyRepulsionForce = 5, EnemyRepulsionForceTimer = 1, RechargeSpeed = 3f};
        deepSeaBombFromAShip = new DeepSeaBombFromAShip() { Name = "DeepSeaBombsFromAShip", Amount =1, Size = 3f, Duration = 4, Damage = 3f, DamageSpeed = 1, RechargeSpeed = 5f, MaxDistance = 3};
        dischargeElectricity = new DischargeElectricity() { Name = "DischargeElectricity", SlowingDownEnemiesSpeed = 100, RechargeSpeed = 5, Duration = 2f };
        accurateShellingFromAShip = new AccurateShellingFromAShip() {Name = "AccurateShellingFromAShip", Amount = 2, Damage = 4f, RechargeSpeed = 5f, DestroyTime = 3 };
        magnet = new Magnet() { Name = "Magnet", RechargeSpeed = 3.5f, Size = 6f, DestroyTime = 2 };
        cloudyWater = new CloudyWater() { Name = "CloudyWater", SlowingDownEnemies = 50f, RechargeSpeed=5, Duration = 2f  };
        mines = new Mines() { Name = "Mines", Amount = 2, RechargeSpeed = 3f, Damage = 3f, TimeBeforeExplosion = 8, Size = 3 };
        regeneration = new Regeneration() { Name = "Regeneration", HealHp = 1f };
        droneAssistants = new DroneAssistants() {Name = "DroneAssistants", Amount = 1, Damage = 3f, SizeZone = 10, Speed = droneSpeed, RotationSpeed = 3, BulletSpeed = 30, CoolDown = 2f };
        //CreateNewController("ForceField");
        //CreateNewController("DeepSeaBombsFromAShip");
        //CreateNewController("DischargeElectricity");
        //CreateNewController("AccurateShellingFromAShip");
        //CreateNewController("Magnet");
        //CreateNewController("CloudyWater");
        //CreateNewController("Mines");
        //CreateNewController("Regeneration");
        //CreateNewController("DroneAssistants");

    }
    public void CreateNewController(string nameWeapon)
    {
        GameObject controller = Instantiate(Resources.Load<GameObject>("Prefab/PassiveWeaponControllers/" + nameWeapon + "Controller"), parent, false);
    }
}
public class ForceField
{
    public string Name;
    public float Size;
    public float Hp;
    public float EnemyRepulsionForce;
    public float EnemyRepulsionForceTimer;
    public float RechargeSpeed ;
}

public class DeepSeaBombFromAShip
{
    public string Name;
    public float DamageSpeed;
    public float Duration;
    public float Size;
    public float RechargeSpeed;
    public int Amount;
    public int MaxDistance;
    public float Damage;
}

public class AccurateShellingFromAShip
{
    public string Name;
    public float RechargeSpeed;
    public float DestroyTime;
    public int Amount;
    public float Damage;
}

public class DroneAssistants
{
    public string Name;
    public float Damage;
    public float Speed;
    public float RotationSpeed;
    public float SizeZone;
    public float BulletSpeed;
    public int Amount;
    public float CoolDown;
}

public class DischargeElectricity
{
    public string Name;
    public float RechargeSpeed;
    public float Duration;
    public float SlowingDownEnemiesSpeed;
}

public class Magnet
{
    public string Name;
    public float Size;
    public float RechargeSpeed;
    public float DestroyTime;
}

public class CloudyWater
{
    public string Name;
    public float SlowingDownEnemies;
    public float Duration;
    public float RechargeSpeed;
}

public class Regeneration
{
    public string Name;
    public float HealHp;
}

public class Mines
{
    public string Name;
    public float RechargeSpeed;
    public float Damage;
    public int Amount;
    public int TimeBeforeExplosion;
    public int Size;
}
