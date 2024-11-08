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
        dict.Add("ForceField", new PassiveWeaponParam() { WeaponLevel = 0, Description = "Создает щит вокруг подводной лодки" });//
        dict.Add("DeepSeaBombsFromAShip", new PassiveWeaponParam() { WeaponLevel = 0, Description = "Помощь с корабля в виде бомб" });
        dict.Add("AccurateShellingFromAShip", new PassiveWeaponParam() { WeaponLevel = 0, Description = "Помощь с корабля в виде точного обстрела врагов" });
        dict.Add("DroneAssistants", new PassiveWeaponParam() { WeaponLevel = 0, Description = "Дроны помощники" });
        dict.Add("DischargeElectricity", new PassiveWeaponParam() { WeaponLevel = 0, Description = "Разряд электричества оснавливает врагов на какое-то время" });//
        dict.Add("Magnet", new PassiveWeaponParam() { WeaponLevel = 0, Description = "Магнит притягивает монеты и бонусы" });
        dict.Add("CloudyWater", new PassiveWeaponParam() { WeaponLevel = 0, Description = "Мутные воды замедляют врагов на какое-то время" });
        dict.Add("Regeneration", new PassiveWeaponParam() { WeaponLevel = 0, Description = "Регенерация восстанавливает здоровье" });
        dict.Add("Mines", new PassiveWeaponParam() { WeaponLevel = 0, Description = "Мины наносят урон врагам при касании" });
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
            return "Описание не найдено."; 
        }
    }
    private void LoadSaveListWeaponLevels()
    {
        List<int> levels = SaveGame.Instance.LoadListPassiveWeaponLevels();
        List<string> keys = GetKeysFromDictionary(); // Получаем список ключей

        // Проверяем, чтобы количество уровней соответствовало количеству ключей
        if (levels.Count == keys.Count)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                string key = keys[i]; // Получаем ключ для текущей позиции
                if (DicPassWeaponLevel.ContainsKey(key)) // Дополнительная проверка на всякий случай
                {
                    // Обновляем уровень оружия в соответствии с загруженным списком уровней
                    DicPassWeaponLevel[key].WeaponLevel = levels[i];
                }
            }
        }
    }
    public void ChangeLevel(string keyName)//прокачка вне уровня
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
