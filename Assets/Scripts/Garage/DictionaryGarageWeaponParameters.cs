using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageWeaponParam
{
    public string paramName;
    public string paramString;
    public string nextParamString;
    public float param;
    public float nextParam;
    public bool isStringParam;
}
public class DictionaryGarageWeaponParameters : MonoBehaviour
{
    
    public static Dictionary<string, GarageWeaponParam> DictGarageWeaponParam()
        {
            Dictionary<string, GarageWeaponParam> dict = new Dictionary<string, GarageWeaponParam>();
            dict.Add("attack", new GarageWeaponParam() { paramName = "атака", param = 0, nextParam = 0, isStringParam = false });
            dict.Add("speed", new GarageWeaponParam() { paramName = "скорость", param = 0, nextParam = 0, isStringParam = false });
            dict.Add("countBullet", new GarageWeaponParam() { paramName = "количество снарядов", param = 0, nextParam = 0, isStringParam = false });
            dict.Add("cooldawn", new GarageWeaponParam() { paramName = "перезарядка", param = 0, nextParam = 0, isStringParam = false });
            dict.Add("repulsion", new GarageWeaponParam() { paramName = "отталкивание", param = 0, nextParam = 0, isStringParam = false });
            dict.Add("direction", new GarageWeaponParam() { paramName = "направление", paramString = "", nextParamString = "", isStringParam = true });

            return dict;
        }
    public Dictionary<string, GarageWeaponParam> DicGarageWeaponParam = DictGarageWeaponParam();

    public void FillDictionary(string weaponLevel1Key, string weaponLevel2Key)
    {
        ActWeapon weaponLevel1 = DictionaryActivWeapon.DictActWeapon()[weaponLevel1Key];
        ActWeapon weaponLevel2 = DictionaryActivWeapon.DictActWeapon()[weaponLevel2Key];
        
        foreach (var key in DicGarageWeaponParam.Keys)
        {
            var paramInfo = DicGarageWeaponParam[key];

            // Получение значений параметров из двух уровней оружия
            var value1 = weaponLevel1.GetType().GetField(key)?.GetValue(weaponLevel1);
            var value2 = weaponLevel2.GetType().GetField(key)?.GetValue(weaponLevel2);
            
            // Проверка типа данных и сохранение в словарь
            if (value1 is float && value2 is float)
            {
                paramInfo.param = (float)value1;
                paramInfo.nextParam = (float)value2;
            }
            else if (value1 is string && value2 is string)
            {
                paramInfo.paramString = (string)value1;
                paramInfo.nextParamString = (string)value2;
            }
        }
    }

}
