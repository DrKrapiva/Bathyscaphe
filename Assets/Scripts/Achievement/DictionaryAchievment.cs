using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Achievement
{
    public string Name;
    public string Parameter;
    public string Description;
    public int Value;//сколько надо получить для достижения уровня
    public int ValueChange;//хочу умножать на два:  value * 2; new value * 2 и т.д
    public int Level;
    public int AddCoin;
    public int AddCoinChange;//хочу умножать на два
}

public class DictionaryAchievment : MonoBehaviour
{
   protected int maxLevel = 3;
    public int MaxLevel => maxLevel;
    private void Start()
    {
        //Debug.Log("Sum");
        LoadSaveListWeaponLevels(DicAchievementsSum, SaveGame.Instance.LoadListAchievementsSumLevels());
        //Debug.Log("MaxOnLevel");
        LoadSaveListWeaponLevels(DicAchievementsMaxOnLevel, SaveGame.Instance.LoadListAchievementsMaxOnLevelLevels());
        
    }
    public static Dictionary<string, Achievement> DictAchievementSum()
    {
        Dictionary<string, Achievement> DictAchievementSum = new Dictionary<string, Achievement>();
        DictAchievementSum.Add("Longevity", new Achievement() { Name = "Долгожитель", Parameter = "time", Description = "Всего время на уровне", Value = 20, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("Treasurer", new Achievement() { Name = "Казначей", Parameter = "coin", Description = "Всего собрано монет", Value = 150, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("Flash", new Achievement() { Name = "Флеш", Parameter = "acceleration", Description = "Всего собрано ускорений", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("DieHard", new Achievement() { Name = "Крепкий орешек", Parameter = "maxArmor", Description = "Всего собрано максимум брони", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("LordOfTorpedoes1", new Achievement() { Name = "Повелитель Торпед 1", Parameter = "RocketMini", Description = "Всего собрано торпед 1", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("LordOfTorpedoes2", new Achievement() { Name = "Повелитель Торпед 2", Parameter = "Rocket", Description = "Всего собрано торпед 2", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("YouAreTheBomb", new Achievement() { Name = "Ты бомба", Parameter = "Explosion", Description = "Всего собрано взрывов", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("Murderer", new Achievement() { Name = "Убийца", Parameter = "deathEnemy", Description = "Всего убито врагов", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("YouDied", new Achievement() { Name = "Ты умер", Parameter = "deathHero", Description = "Всего ты убит", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        return DictAchievementSum;
    }
    
    public static Dictionary<string, Achievement> DictAchievementMaxOnLevel()
    {
        Dictionary<string, Achievement> DictAchievementMaxOnLevel = new Dictionary<string, Achievement>();
        DictAchievementMaxOnLevel.Add("Longevity", new Achievement() { Name = "Долгожитель", Parameter = "time", Description = "Время за уровень", Value = 3, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("Treasurer", new Achievement() { Name = "Казначей", Parameter = "coin", Description = "Собрано монет за уровень", Value = 50, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("Flash", new Achievement() { Name = "Флеш", Parameter = "acceleration", Description = "Собрано ускорений за уровень", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("DieHard", new Achievement() { Name = "Крепкий орешек", Parameter = "maxArmor", Description = "Собрано максисальной брони за уровень", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("LordOfTorpedoes1", new Achievement() { Name = "Повелитель Торпед 1", Parameter = "RocketMini", Description = "Собрано торпед 1 за уровень", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("LordOfTorpedoes2", new Achievement() { Name = "Повелитель Торпед 2", Parameter = "Rocket", Description = "Собрано торпед 2 за уровень", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("YouAreTheBomb", new Achievement() { Name = "Ты бомба", Parameter = "Explosion", Description = "Собрано взрывов за уровень", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("Murderer", new Achievement() { Name = "Убийца", Parameter = "deathEnemy", Description = "Убито врагов за уровень", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        return DictAchievementMaxOnLevel;
    }
    public Dictionary<string, Achievement> DicAchievementsSum = DictAchievementSum();
    public Dictionary<string, Achievement> DicAchievementsMaxOnLevel = DictAchievementMaxOnLevel();

    //получать из сохранения список LevelSum
    //получать из сохранения список LevelOnLevel

    //сохранять список LevelSum  
    //сохранять список LevelOnLevel  
    public List<string> GetKeysFromDictionary(Dictionary<string, Achievement> dictionary)//не используется
    {
        List<string> keysList = new List<string>(dictionary.Keys);
        return keysList;
    }
    
    public int GetNumberOfKeysInDictionary(Dictionary<string, Achievement> dictionary)//для создания списка в сохранении
    {
        return dictionary.Count;
    }
    private void LoadSaveListWeaponLevels(Dictionary<string, Achievement> achievementsDict, List<int> savedLevels)//подгрузка уровней из сохранения
    {
        List<string> keys = new List<string>(achievementsDict.Keys); // Получаем список ключей

        // Проверяем, чтобы количество уровней соответствовало количеству ключей
        if (savedLevels.Count == keys.Count)
        {
            for (int i = 0; i < savedLevels.Count; i++)
            {
                string key = keys[i]; // Получаем ключ для текущей позиции
                if (achievementsDict.ContainsKey(key)) // Дополнительная проверка на всякий случай
                {
                    // Обновляем уровень в соответствии с загруженным списком уровней
                    achievementsDict[key].Level = savedLevels[i];
                    //Debug.Log(  key +" "+ achievementsDict[key].Level);
                }
            }
        }
        else
        {
            // Обработка ошибки или предупреждение о несоответствии данных
            Debug.LogWarning("The number of saved levels does not match the number of achievements.");
        }
    }
    
    public int ValueForCheck(string key, bool isSum, int? level = null)
    {
        Dictionary<string, Achievement> dicToUse = isSum ? DicAchievementsSum : DicAchievementsMaxOnLevel;
        if (dicToUse.TryGetValue(key, out Achievement achievement))
        {
            int effectiveLevel = level ?? achievement.Level;// Используем переданный уровень, если он есть; если нет, берём уровень из словаря
            //Debug.Log(achievement.Value + " Value");
            //Debug.Log(achievement.ValueChange + " ValueChange");
            //Debug.Log(effectiveLevel + " effectiveLevel");
            return achievement.Value * (int)Math.Pow(achievement.ValueChange, effectiveLevel);
        }
        return 0;
    }
    public int CoinsForAchievement(string key, bool isSum, int? level = null)
    {
        Dictionary<string, Achievement> dicToUse = isSum ? DicAchievementsSum : DicAchievementsMaxOnLevel;
        if (dicToUse.TryGetValue(key, out Achievement achievement))
        {
            int effectiveLevel = level ?? achievement.Level;// Используем переданный уровень, если он есть; если нет, берём уровень из словаря
            return achievement.AddCoin * (int)Mathf.Pow(achievement.AddCoinChange, effectiveLevel);
        }
        return 0;
    }
    
    public void SaveAchievementsLevels(bool isSum)
    {
        Dictionary<string, Achievement> achievementsDict = isSum ? DicAchievementsSum : DicAchievementsMaxOnLevel;
        List<int> levels = new List<int>();

        foreach (var achievement in achievementsDict.Values)
        {
            levels.Add(achievement.Level);
        }

        if (isSum)
        {
            SaveGame.Instance.SaveListAchievementsSumLevels(levels);
        }
        else
        {
            SaveGame.Instance.SaveListAchievementsMaxOnLevelLevels(levels);
        }
    }

}
