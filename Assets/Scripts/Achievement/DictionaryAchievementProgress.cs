using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementProgress
{
    public int Sum;
    public int MaxOnLevel;
    public int OnLevel;
}
public class DictionaryAchievementProgress : MonoBehaviour
{
    public static DictionaryAchievementProgress Instance
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
    static private DictionaryAchievementProgress _instance;
    private void Start()
    {
        LoadSaveListAchievementProgress();
    }
    public static Dictionary<string, AchievementProgress> DictAchievementProgress()
    {
        Dictionary<string, AchievementProgress> DicAchievementProgress = new Dictionary<string, AchievementProgress>();//название ключей соответсвуют названию параметра из словар€ DictionaryAchievment
        DicAchievementProgress.Add("time", new AchievementProgress() { Sum = 0, MaxOnLevel = 0, OnLevel = 0 } );
        DicAchievementProgress.Add("coin", new AchievementProgress() { Sum = 0, MaxOnLevel = 0, OnLevel = 0 });
        DicAchievementProgress.Add("acceleration", new AchievementProgress() { Sum = 0, MaxOnLevel = 0, OnLevel = 0 });
        DicAchievementProgress.Add("maxArmor", new AchievementProgress() { Sum = 0, MaxOnLevel = 0, OnLevel = 0 });
        DicAchievementProgress.Add("RocketMini", new AchievementProgress() { Sum = 0, MaxOnLevel = 0, OnLevel = 0 });
        DicAchievementProgress.Add("Rocket", new AchievementProgress() { Sum = 0, MaxOnLevel = 0, OnLevel = 0 });
        DicAchievementProgress.Add("Explosion", new AchievementProgress() { Sum = 0, MaxOnLevel = 0, OnLevel = 0 });
        DicAchievementProgress.Add("deathEnemy", new AchievementProgress() { Sum = 0, MaxOnLevel = 0, OnLevel = 0 });
        DicAchievementProgress.Add("deathHero", new AchievementProgress() { Sum = 0, MaxOnLevel = 0, OnLevel = 0 });
        return DicAchievementProgress;
    }
    
    public Dictionary<string, AchievementProgress> DicAchievementsProgress = DictAchievementProgress();

    public int NumberKeysFromDictionary()//дл€ сохранени€
    {
        return DicAchievementsProgress.Count;
    }
    public void LoadSaveListAchievementProgress()
    {
        // «агрузка списков из сохранени€
        List<int> sumList = SaveGame.Instance.LoadListProgressSumAchiv();
        List<int> maxOnLevelList = SaveGame.Instance.LoadListProgressMaxOnLevelAchiv();
        //List<int> onLevelList = SaveGame.Instance.LoadListProgressOnLevelAchiv();

        // ѕроверка на соответствие размеров списков и словар€
        if (sumList.Count != DicAchievementsProgress.Count ||
            maxOnLevelList.Count != DicAchievementsProgress.Count /*||
            onLevelList.Count != DicAchievementsProgress.Count*/)
        {
            throw new InvalidOperationException("–азмеры загруженных списков не соответствуют словарю достижений.");
        }

        // ќбновление данных в словаре
        int index = 0;
        foreach (var key in DicAchievementsProgress.Keys.ToList())
        {
            DicAchievementsProgress[key].Sum = sumList[index];
            //Debug.Log("Sum");
            //Debug.Log(DicAchievementsProgress[key].Sum);
            DicAchievementsProgress[key].MaxOnLevel = maxOnLevelList[index];
            //Debug.Log("MaxOnLevel");
            //Debug.Log(DicAchievementsProgress[key].MaxOnLevel);
            //DicAchievementsProgress[key].OnLevel = onLevelList[index];
            //Debug.Log("OnLevel");
            //Debug.Log(DicAchievementsProgress[key].OnLevel);
            index++;
        }

    }
    

    public List<int> GetSumList()
    {
        return DicAchievementsProgress.Values.Select(x => x.Sum).ToList();
    }

    public List<int> GetMaxOnLevelList()
    {
        return DicAchievementsProgress.Values.Select(x => x.MaxOnLevel).ToList();
    }

    public List<int> GetOnLevelList()
    {
        return DicAchievementsProgress.Values.Select(x => x.OnLevel).ToList();
    }
    
    
    public void UpdateAchievementProgressAdd(string key, int newParam)
    {
        if (DicAchievementsProgress.ContainsKey(key))
        {
            DicAchievementsProgress[key].OnLevel += newParam;
            //Debug.Log("OnLevel");
            //Debug.Log(DicAchievementsProgress[key].OnLevel);
        }
    }
    public void UpdateAchievementProgressRewrite(string key, int newParam, string nameList)
    {
        if (DicAchievementsProgress.ContainsKey(key))
        {
            
            switch (nameList)
            {
                case "Sum":
                    DicAchievementsProgress[key].Sum = newParam;
                    //Debug.Log("Sum");
                    //Debug.Log(DicAchievementsProgress[key].Sum);
                    break;
                case "MaxOnLevel":
                    DicAchievementsProgress[key].MaxOnLevel = newParam;
                    //Debug.Log("MaxOnLevel");
                    //Debug.Log(DicAchievementsProgress[key].MaxOnLevel);
                    break;
                case "OnLevel":
                    DicAchievementsProgress[key].OnLevel = newParam;
                    Debug.Log("OnLevel");
                    Debug.Log(DicAchievementsProgress[key].OnLevel);
                    break;
            }
        }
    }
    public void UpdateSumFromOnLevel()
    {
        foreach (var key in DicAchievementsProgress.Keys.ToList())
        {
            // ƒл€ maxArmor особое условие: обновл€ем Sum, только если OnLevel больше.
            if (key == "maxArmor")
            {
                if (DicAchievementsProgress[key].OnLevel > DicAchievementsProgress[key].Sum)
                {
                    DicAchievementsProgress[key].Sum = DicAchievementsProgress[key].OnLevel;
                }
            }
            else
            {
                // ƒл€ всех остальных ключей просто добавл€ем значение OnLevel к Sum.
                DicAchievementsProgress[key].Sum += DicAchievementsProgress[key].OnLevel;
            }
        }
    }
    public void UpdateMaxOnLevelFromOnLevel()
    {
        foreach (var key in DicAchievementsProgress.Keys.ToList())
        {
            // ќбновл€ем MaxOnLevel, только если значение OnLevel больше текущего MaxOnLevel
            if (DicAchievementsProgress[key].OnLevel > DicAchievementsProgress[key].MaxOnLevel)
            {
                DicAchievementsProgress[key].MaxOnLevel = DicAchievementsProgress[key].OnLevel;
            }

        }

    }

    public int GetAchievementsProgress(string key, string nameList)
    {

        if (DicAchievementsProgress.ContainsKey(key))
        {
            switch (nameList)
            {
                case "Sum":
                    return DicAchievementsProgress[key].Sum;
                case "MaxOnLevel":
                    return DicAchievementsProgress[key].MaxOnLevel;
                case "OnLevel":
                    return DicAchievementsProgress[key].OnLevel;
                default:
                    Debug.LogError($"Unknown progress type: {nameList}");
                    return 0; // Ќеизвестный тип прогресса
            }
                
        }
        return 0;
    }
    public void SaveAchivProgress()
    {
        
        SaveGame.Instance.SaveListProgressSumAchiv(GetSumList());
        SaveGame.Instance.SaveListProgressMaxOnLevelAchiv(GetMaxOnLevelList());
        SaveGame.Instance.SaveListProgressOnLevelAchiv(GetOnLevelList());
    }
}
