using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public string Name;
    public string Description;
    public string MissionName;
    public List<string> SpecialWeapon;
    public List<int> SpecialWeaponLevel;
    
    public List<string> AdditionalTask;
}
public class DictionaryMap : MonoBehaviour
{
    public static Dictionary<string, Map> Dict()
    {
        Dictionary<string, Map> DicMap = new Dictionary<string, Map>();
        DicMap.Add("NoMission", new Map() { Name = "тут миссии нет", SpecialWeapon = new List<string>() { }, Description = "беги, кораблик, беги", MissionName = "NoMission", AdditionalTask = new List<string>() {  } });
        DicMap.Add("MissionRescue", new Map() { Name = "мисси€ спасени€", SpecialWeapon = new List<string>() { }, Description = "—паси заблудившихс€ водолазов", MissionName = "MissionRescue", AdditionalTask = new List<string>() { "task1" } });
        //DicMap.Add("Violet", new Map() { Name = "second", SpecialWeapon = new List<string>() { "RocketMini" }, SpecialWeaponLevel = new List<int>() { 1 } ,Description = "ѕрошли годы... ѕодростки повзрослели, и ничто, казалось, не предвещало новой беды", MissionName = "", AdditionalTask = new List<string>() { "task4", "task2" } });
       // DicMap.Add("Blue", new Map() { Name = "third", SpecialWeapon = new List<string>() { }, Description = "Ќо кошмар прошлого вернулс€, неведома€ сила повлекла семерых друзей назад, в новую битву со «лом.", MissionName = "", AdditionalTask = new List<string>() { "task3" } });
        return DicMap;
    }
    public Dictionary<string, Map> DicMaps = Dict();
}
