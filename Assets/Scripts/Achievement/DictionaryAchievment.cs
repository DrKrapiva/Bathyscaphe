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
    public int Value;//������� ���� �������� ��� ���������� ������
    public int ValueChange;//���� �������� �� ���:  value * 2; new value * 2 � �.�
    public int Level;
    public int AddCoin;
    public int AddCoinChange;//���� �������� �� ���
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
        DictAchievementSum.Add("Longevity", new Achievement() { Name = "�����������", Parameter = "time", Description = "����� ����� �� ������", Value = 20, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("Treasurer", new Achievement() { Name = "��������", Parameter = "coin", Description = "����� ������� �����", Value = 150, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("Flash", new Achievement() { Name = "����", Parameter = "acceleration", Description = "����� ������� ���������", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("DieHard", new Achievement() { Name = "������� ������", Parameter = "maxArmor", Description = "����� ������� �������� �����", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("LordOfTorpedoes1", new Achievement() { Name = "���������� ������ 1", Parameter = "RocketMini", Description = "����� ������� ������ 1", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("LordOfTorpedoes2", new Achievement() { Name = "���������� ������ 2", Parameter = "Rocket", Description = "����� ������� ������ 2", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("YouAreTheBomb", new Achievement() { Name = "�� �����", Parameter = "Explosion", Description = "����� ������� �������", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("Murderer", new Achievement() { Name = "������", Parameter = "deathEnemy", Description = "����� ����� ������", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        DictAchievementSum.Add("YouDied", new Achievement() { Name = "�� ����", Parameter = "deathHero", Description = "����� �� ����", Value = 50, ValueChange = 2, AddCoin = 200, AddCoinChange = 2, Level = 0 });
        return DictAchievementSum;
    }
    
    public static Dictionary<string, Achievement> DictAchievementMaxOnLevel()
    {
        Dictionary<string, Achievement> DictAchievementMaxOnLevel = new Dictionary<string, Achievement>();
        DictAchievementMaxOnLevel.Add("Longevity", new Achievement() { Name = "�����������", Parameter = "time", Description = "����� �� �������", Value = 3, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("Treasurer", new Achievement() { Name = "��������", Parameter = "coin", Description = "������� ����� �� �������", Value = 50, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("Flash", new Achievement() { Name = "����", Parameter = "acceleration", Description = "������� ��������� �� �������", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("DieHard", new Achievement() { Name = "������� ������", Parameter = "maxArmor", Description = "������� ������������ ����� �� �������", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("LordOfTorpedoes1", new Achievement() { Name = "���������� ������ 1", Parameter = "RocketMini", Description = "������� ������ 1 �� �������", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("LordOfTorpedoes2", new Achievement() { Name = "���������� ������ 2", Parameter = "Rocket", Description = "������� ������ 2 �� �������", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("YouAreTheBomb", new Achievement() { Name = "�� �����", Parameter = "Explosion", Description = "������� ������� �� �������", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        DictAchievementMaxOnLevel.Add("Murderer", new Achievement() { Name = "������", Parameter = "deathEnemy", Description = "����� ������ �� �������", Value = 5, ValueChange = 2, AddCoin = 50, AddCoinChange = 2, Level = 0 });
        return DictAchievementMaxOnLevel;
    }
    public Dictionary<string, Achievement> DicAchievementsSum = DictAchievementSum();
    public Dictionary<string, Achievement> DicAchievementsMaxOnLevel = DictAchievementMaxOnLevel();

    //�������� �� ���������� ������ LevelSum
    //�������� �� ���������� ������ LevelOnLevel

    //��������� ������ LevelSum  
    //��������� ������ LevelOnLevel  
    public List<string> GetKeysFromDictionary(Dictionary<string, Achievement> dictionary)//�� ������������
    {
        List<string> keysList = new List<string>(dictionary.Keys);
        return keysList;
    }
    
    public int GetNumberOfKeysInDictionary(Dictionary<string, Achievement> dictionary)//��� �������� ������ � ����������
    {
        return dictionary.Count;
    }
    private void LoadSaveListWeaponLevels(Dictionary<string, Achievement> achievementsDict, List<int> savedLevels)//��������� ������� �� ����������
    {
        List<string> keys = new List<string>(achievementsDict.Keys); // �������� ������ ������

        // ���������, ����� ���������� ������� ��������������� ���������� ������
        if (savedLevels.Count == keys.Count)
        {
            for (int i = 0; i < savedLevels.Count; i++)
            {
                string key = keys[i]; // �������� ���� ��� ������� �������
                if (achievementsDict.ContainsKey(key)) // �������������� �������� �� ������ ������
                {
                    // ��������� ������� � ������������ � ����������� ������� �������
                    achievementsDict[key].Level = savedLevels[i];
                    //Debug.Log(  key +" "+ achievementsDict[key].Level);
                }
            }
        }
        else
        {
            // ��������� ������ ��� �������������� � �������������� ������
            Debug.LogWarning("The number of saved levels does not match the number of achievements.");
        }
    }
    
    public int ValueForCheck(string key, bool isSum, int? level = null)
    {
        Dictionary<string, Achievement> dicToUse = isSum ? DicAchievementsSum : DicAchievementsMaxOnLevel;
        if (dicToUse.TryGetValue(key, out Achievement achievement))
        {
            int effectiveLevel = level ?? achievement.Level;// ���������� ���������� �������, ���� �� ����; ���� ���, ���� ������� �� �������
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
            int effectiveLevel = level ?? achievement.Level;// ���������� ���������� �������, ���� �� ����; ���� ���, ���� ������� �� �������
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
