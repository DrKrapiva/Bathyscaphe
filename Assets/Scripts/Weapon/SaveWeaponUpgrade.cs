using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWeaponUpgrade : DictionaryCostOfActiveWeapon
{
    public static SaveWeaponUpgrade Instance
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

    static private SaveWeaponUpgrade _instance;
    private void Start()
    {
        LoadInfoLevelRarityWeapon();
    }
    private void LoadInfoLevelRarityWeapon()
    {
        List<int> levelRar = SaveGame.Instance.LoadRarityLevelActiveWeapon();
        int i = 0;
        foreach (var level in DicCostWeapon.Keys)
        {
            DicCostWeapon[level].level = levelRar[i];
            i++;
        }
    }
    public string GetWeaponLevelRarity(string key)
    {
       // Debug.Log("SaveWeaponUpgrade");

         
        string s = key + listNameRarity[DicCostWeapon[key].level];
        return s;
    }
    public bool CheckIfKeyExists(string key)
    {
        return DicCostWeapon.ContainsKey(key);
    }


}
