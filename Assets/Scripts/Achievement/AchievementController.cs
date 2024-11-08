using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class AchievementController : DictionaryAchievment
{
    public static AchievementController Instance
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
    static private AchievementController _instance;
    public bool CheckMaxLevel(string key, bool isSum)
    {
        //Debug.Log(key);
        Dictionary<string, Achievement> dictToCheck = isSum ? DicAchievementsSum : DicAchievementsMaxOnLevel;

        if (dictToCheck.TryGetValue(key, out Achievement achievement))
        {
            //Debug.Log("achievement.Level " + achievement.Level);
            //Debug.Log("maxLevel " + maxLevel);
            return achievement.Level >= maxLevel;
        }
        return false;
    }
    
    public void CheckProgress(bool isSum)
    {
        Dictionary<string, Achievement> achievementsDict = isSum ? DicAchievementsSum : DicAchievementsMaxOnLevel;
        string nameList = isSum ? "Sum" : "MaxOnLevel";

        foreach (var key in achievementsDict.Keys)
        {
            bool levelIncreased = false;
            while (IncreaseTheLevel(key, isSum))
            {
                if (achievementsDict[key].Parameter != "maxArmor")
                {
                    int newParam = DictionaryAchievementProgress.Instance.GetAchievementsProgress(achievementsDict[key].Parameter, nameList) - ValueForCheck(key, isSum);
                    DictionaryAchievementProgress.Instance.UpdateAchievementProgressRewrite(achievementsDict[key].Parameter, newParam, nameList);

                }
                DictionaryAchievementProgress.Instance.SaveAchivProgress();

                achievementsDict[key].Level++;

                levelIncreased = true;
            }

            if (levelIncreased)
            {
                
                SaveAchievementsLevels(isSum); // Сохраняем изменения уровней достижений
            }
        }
    }
    private bool IncreaseTheLevel(string key, bool isSum)
    {
        Dictionary<string, Achievement> dictToUse = isSum ? DicAchievementsSum : DicAchievementsMaxOnLevel;
        string nameList = isSum ? "Sum" : "MaxOnLevel";
        if (!CheckMaxLevel(key, isSum))
        {
            return DictionaryAchievementProgress.Instance.GetAchievementsProgress(dictToUse[key].Parameter, nameList) >= ValueForCheck(key, isSum);
        }
        return false;
    }
}
