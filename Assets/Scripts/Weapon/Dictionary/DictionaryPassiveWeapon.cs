using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveWeaponParam
{
    public int WeaponLevel;
    public string Description;
}
public class DictionaryPassiveWeapon : MonoBehaviour
{
    public static DictionaryPassiveWeapon Instance
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
    static private DictionaryPassiveWeapon _instance;
    private void Start()
    {
        LoadSaveListWeaponLevels();
    }
    public static Dictionary<string, PassiveWeaponParam> DictPassWeaponLevel()
    {
        Dictionary<string, PassiveWeaponParam> dict = new Dictionary<string, PassiveWeaponParam>();
        dict.Add("ForceField", new PassiveWeaponParam() { WeaponLevel = 0, Description = "������� ��� ������ ��������� �����" });//
        dict.Add("DeepSeaBombsFromAShip", new PassiveWeaponParam() { WeaponLevel = 0, Description = "������ � ������� � ���� ����" });
        dict.Add("AccurateShellingFromAShip", new PassiveWeaponParam() { WeaponLevel = 0, Description = "������ � ������� � ���� ������� �������� ������" });
        dict.Add("DroneAssistants", new PassiveWeaponParam() { WeaponLevel = 0, Description = "����� ���������" });
        dict.Add("DischargeElectricity", new PassiveWeaponParam() { WeaponLevel = 0, Description = "������ ������������� ����������� ������ �� �����-�� �����" });//
        dict.Add("Magnet", new PassiveWeaponParam() { WeaponLevel = 0, Description = "������ ����������� ������ � ������" });
        dict.Add("CloudyWater", new PassiveWeaponParam() { WeaponLevel = 0, Description = "������ ���� ��������� ������ �� �����-�� �����" });
        dict.Add("Regeneration", new PassiveWeaponParam() { WeaponLevel = 0, Description = "����������� ��������������� ��������" });
        dict.Add("Mines", new PassiveWeaponParam() { WeaponLevel = 0, Description = "���� ������� ���� ������ ��� �������" });
        return dict;
    }
    public Dictionary<string, PassiveWeaponParam> DicPassWeaponLevel = DictPassWeaponLevel();

    public List<string> GetKeysFromDictionary()
    {
        List<string> keysList = new List<string>(DicPassWeaponLevel.Keys);
        return keysList;
    }
    public int NumberKeysFromDictionary()
    {
        return DicPassWeaponLevel.Keys.Count;
    }
    public int GetWeaponLevelByKey(string key)
    {
        if (DicPassWeaponLevel.ContainsKey(key))
        {
            //Debug.Log(DicPassWeaponLevel[key].WeaponLevel + key);
            return DicPassWeaponLevel[key].WeaponLevel;
        }
        else
        {
            return 0;
        }
    }
    public string GetDescriptionByKey(string key)
    {
        if (DicPassWeaponLevel.ContainsKey(key))
        {
            return DicPassWeaponLevel[key].Description;
        }
        else
        {
            return "�������� �� �������."; 
        }
    }
    private void LoadSaveListWeaponLevels()
    {
        List<int> levels = SaveGame.Instance.LoadListPassiveWeaponLevels();
        List<string> keys = GetKeysFromDictionary(); // �������� ������ ������

        // ���������, ����� ���������� ������� ��������������� ���������� ������
        if (levels.Count == keys.Count)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                string key = keys[i]; // �������� ���� ��� ������� �������
                if (DicPassWeaponLevel.ContainsKey(key)) // �������������� �������� �� ������ ������
                {
                    // ��������� ������� ������ � ������������ � ����������� ������� �������
                    DicPassWeaponLevel[key].WeaponLevel = levels[i];
                }
            }
        }
    }
    public void ChangeLevel(string keyName)//�������� ��� ������
    {

        DicPassWeaponLevel[keyName].WeaponLevel++;
        //Debug.Log(DicPassWeaponLevel[keyName].WeaponLevel + keyName + "ChangeLevel");
        SaveListWeaponLevel();
    }
    private void SaveListWeaponLevel()
    {
        List<int> levels = new List<int>();
        foreach(var level in DicPassWeaponLevel.Values)
        {
            levels.Add(level.WeaponLevel);
        }
        SaveGame.Instance.SaveListPassiveWeaponLevels(levels);
    }
}
