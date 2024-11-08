using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DictionaryAchievementRewards : MonoBehaviour
{
    public static DictionaryAchievementRewards Instance
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
    static private DictionaryAchievementRewards _instance;
    private void Start()
    {
        LoadSaveListAchievementRewards(true); // «агружаем суммарные достижени€
        LoadSaveListAchievementRewards(false); // «агружаем достижени€ на уровне
    }
    public static Dictionary<string, int> DictAchievementRewardsSum()
    {
        Dictionary<string, int> DicAchievementReward = new Dictionary<string, int>();//название ключей соответсвуют названию параметра из словар€ DictionaryAchievment
        DicAchievementReward.Add("time", 0);
        DicAchievementReward.Add("coin", 0);
        DicAchievementReward.Add("acceleration", 0);
        DicAchievementReward.Add("maxArmor", 0);
        DicAchievementReward.Add("RocketMini", 0);
        DicAchievementReward.Add("Rocket", 0);
        DicAchievementReward.Add("Explosion", 0);
        DicAchievementReward.Add("deathEnemy", 0);
        DicAchievementReward.Add("deathHero", 0);
        return DicAchievementReward;
    }
    
    public static Dictionary<string, int> DictAchievementRewardsMaxOnLevel()
    {
        Dictionary<string, int> DicAchievementReward = new Dictionary<string, int>();//название ключей соответсвуют названию параметра из словар€ DictionaryAchievment
        DicAchievementReward.Add("time", 0);
        DicAchievementReward.Add("coin", 0);
        DicAchievementReward.Add("acceleration", 0);
        DicAchievementReward.Add("maxArmor", 0);
        DicAchievementReward.Add("RocketMini", 0);
        DicAchievementReward.Add("Rocket", 0);
        DicAchievementReward.Add("Explosion", 0);
        DicAchievementReward.Add("deathEnemy", 0);
        DicAchievementReward.Add("deathHero", 0);
        return DicAchievementReward;
    }
    public Dictionary<string, int> DicAchievementsRewardsSum = DictAchievementRewardsSum();
    public Dictionary<string, int> DicAchievementsRewardsOnLevel = DictAchievementRewardsMaxOnLevel();
    public int NumberKeysFromDictionary(Dictionary<string, int> dictionary)//дл€ сохранени€
    {
        return dictionary.Count;
    }

    private void LoadSaveListAchievementRewards(bool isSum)
    {
        List<int> rewards = isSum ? SaveGame.Instance.LoadListAchivRewardsSum() : SaveGame.Instance.LoadListAchivRewardsOnLevel();
        Dictionary<string, int> targetDict = isSum ? DicAchievementsRewardsSum : DicAchievementsRewardsOnLevel;

        if (rewards.Count != targetDict.Count)
        {
            throw new InvalidOperationException(" оличество элементов в списке сохранени€ не соответствует количеству ключей в словаре.");
        }
        List<string> keys = targetDict.Keys.ToList();
        for (int i = 0; i < rewards.Count; i++)
        {
            targetDict[keys[i]] = rewards[i];
        }

    }

    private void ListRewardsToSave(bool isSum)
    {
        List<int> listAchievementsRewards = (isSum ? DicAchievementsRewardsSum : DicAchievementsRewardsOnLevel).Values.ToList();
        if (isSum)
        {
            SaveGame.Instance.SaveListAchivRewardsSum(listAchievementsRewards);
        }
        else
        {
            SaveGame.Instance.SaveListAchivRewardsOnLevel(listAchievementsRewards);
        }
    }

    public int GetAchievementsRewards(string key, bool isSum)
    {
        Dictionary<string, int> targetDict = isSum ? DicAchievementsRewardsSum : DicAchievementsRewardsOnLevel;
        if (targetDict.TryGetValue(key, out int reward))
        {
            return reward;
        }
        return 0;
    }

    public void ChangeLevelAchievementRewards(string key, bool isSum)
    {
        Dictionary<string, int> targetDict = isSum ? DicAchievementsRewardsSum : DicAchievementsRewardsOnLevel;
        if (targetDict.ContainsKey(key))
        {
            targetDict[key]++;
            ListRewardsToSave(isSum);
        }
        else
        {
            Debug.LogWarning($"Key {key} not found in rewards dictionary.");
        }
    }
}
