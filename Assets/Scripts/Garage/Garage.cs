using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Garage : DictionaryCostOfActiveWeapon
{
    [SerializeField] private GameObject panelGarage;
    [SerializeField] private Transform content;
    
    public static Garage Instance
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
    static private Garage _instance;
    private void Start()
    {
        LoadInfoLevelRarityWeapon();
    }

    public string GetFullWeaponName(string shortWeaponName, int? weaponLevel = null)
    {
        // Получаем текущий уровень оружия из словаря
        int baseLevel = DicCostWeapon[shortWeaponName].level;

        // Если weaponLevel имеет значение, прибавляем его к базовому уровню, иначе используем базовый уровень
        int levelToUse = weaponLevel.HasValue ? baseLevel + weaponLevel.Value : baseLevel;

        // Проверяем, чтобы levelToUse не превышал максимально допустимый индекс в listNameRarity
        if (levelToUse >= GetListNameRarityCount())
        {
            // Если превышает, используем максимально возможный индекс
            levelToUse = GetListNameRarityCount() - 1;
        }

        // Составляем полное имя оружия
        string name = shortWeaponName + listNameRarity[levelToUse];
        return name;
    }
    public List<string> GetWeaponList()
    {
        List<string> keys = new List<string>(DicCostWeapon.Keys);
        return keys;
    }
    public int GetWeaponLevel(string shortWeaponName)
    {
        return DicCostWeapon[shortWeaponName].level;
    }
    public void GenerateGaragePanel()
    {
        GameObject panel = Instantiate(panelGarage, content, false);
        panel.GetComponent<GaragePanel>().FillInfo(GetWeaponList());
    }
    public int GetListNameRarityCount()
    {
        return listNameRarity.Count;    
    }
    public int RealCostOfWeapon(string key)
    {
        return DicCostWeapon[key].cost + DicCostWeapon[key].costChange * DicCostWeapon[key].level;
    }
    public void UpdateParamDictionary(string key)
    {
        DicCostWeapon[key].level++;
        SaveInfoLevelRarityWeapon();

    }

    private void LoadInfoLevelRarityWeapon()
     {
         List<int> levelRar = SaveGame.Instance.LoadRarityLevelActiveWeapon();
         int i = 0;
         foreach(var level in DicCostWeapon.Keys)
         {
             DicCostWeapon[level].level = levelRar[i];
             i++;
         }
     }
     private void SaveInfoLevelRarityWeapon()
     {
         List<int> levelRar = new List<int>();
         foreach(var level in DicCostWeapon.Keys)
         {
             levelRar.Add(DicCostWeapon[level].level);
         }
         SaveGame.Instance.SaveRarityLevelActiveWeapon(levelRar);
     }
}
