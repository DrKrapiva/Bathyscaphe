using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    public string Name;
    public string Description;
    public float HowMuchAdd;
    public int CostOfUpgrade;
    public int IncreaseCostOfUpgreadWithLevel;
    public int Level;
    public int MaxLevel;
}
public class DictionaryUprades : MonoBehaviour
{
    public static DictionaryUprades Instance
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
    private void Start()
    {
        LoadInfoLevelHeroUpgrade(PlayerPrefs.GetString("NowHero"));
    }
    static private DictionaryUprades _instance;
    public static Dictionary<string, Upgrades> DictUpgrades()
    {
        Dictionary<string, Upgrades> dict = new Dictionary<string, Upgrades>();
        dict.Add("regeneration", new Upgrades() { Name = "регенерация", Description = "/сек.", HowMuchAdd = 1, CostOfUpgrade = 350, IncreaseCostOfUpgreadWithLevel = 50, Level = 0, MaxLevel = 5 });
        dict.Add("speed", new Upgrades() { Name = "скорость", Description = "%", HowMuchAdd = 15f, CostOfUpgrade = 3, IncreaseCostOfUpgreadWithLevel = 50, Level = 0, MaxLevel = 4 });
        dict.Add("maxHP", new Upgrades() { Name = "макс здоровье", Description = "%", HowMuchAdd = 10f, CostOfUpgrade = 400, IncreaseCostOfUpgreadWithLevel = 50, Level = 0, MaxLevel = 6 });
        dict.Add("armor", new Upgrades() { Name = "броня", Description = "%", HowMuchAdd = 20f, CostOfUpgrade = 300, IncreaseCostOfUpgreadWithLevel = 50, Level = 0, MaxLevel = 6 });
        dict.Add("resurrection", new Upgrades() { Name = "воскрешение", Description = "раз", HowMuchAdd = 1, CostOfUpgrade = 10000, IncreaseCostOfUpgreadWithLevel = 50, Level = 0, MaxLevel = 2 });
        dict.Add("coin", new Upgrades() { Name = "больше монет", Description = "%", HowMuchAdd = 20f, CostOfUpgrade = 500, IncreaseCostOfUpgreadWithLevel = 50, Level = 0, MaxLevel = 5 });
        return dict;
    }
    public Dictionary<string, Upgrades> DictUpgrade = DictUpgrades();
    public bool IsMaxLevel(string key)
    {
        if (DictUpgrade[key].Level == DictUpgrade[key].MaxLevel)
        {
            return true;
        }
        else return false;
    }
    public float HowMuchAdd(string key)
    {
        return DictUpgrade[key].HowMuchAdd * DictUpgrade[key].Level;
    }
    public int CountNowCostOfUpgrade(string key)
    {
        return DictUpgrade[key].CostOfUpgrade + DictUpgrade[key].IncreaseCostOfUpgreadWithLevel * DictUpgrade[key].Level;
    }
    public int NumberKeysFromDictionary()//для сохранения
    {
        return DictUpgrade.Count;
    }
    private void LoadInfoLevelHeroUpgrade(string heroName)
    {
        List<int> levelUpgr = SaveGame.Instance.LoadLevelHeroUpgrade(heroName);
        int i = 0;
        foreach (var level in DictUpgrade.Keys)
        {
            DictUpgrade[level].Level = levelUpgr[i];
            i++;
        }
    }
    public void SaveInfoLevelHeroUpgrade(string heroName)
    {
        List<int> levelUpgr = new List<int>();
        foreach (var level in DictUpgrade.Keys)
        {
            levelUpgr.Add(DictUpgrade[level].Level);
        }
        SaveGame.Instance.SaveLevelHeroUpgrade(levelUpgr, heroName);
    }
}
