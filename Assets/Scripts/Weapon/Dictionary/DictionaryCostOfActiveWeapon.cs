using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActWeaponCost
{
    public int level;
    public int cost;
    public int costChange;
    public int AmountYouHave; // добавить?
}
public class DictionaryCostOfActiveWeapon : MonoBehaviour
{
    protected List<string> listNameRarity = new List<string> () { "Regular", "Rare", "Legendary", "Epic" };
    public static Dictionary<string, ActWeaponCost> DictCostWeapon()
    {
        Dictionary<string, ActWeaponCost> dict = new Dictionary<string, ActWeaponCost>();
        dict.Add("RocketMini", new ActWeaponCost() { level = 0, cost = 100, costChange = 50});

        dict.Add("Rocket", new ActWeaponCost() { level = 0, cost = 100, costChange = 50});

        dict.Add("Explosion", new ActWeaponCost(){ level = 0, cost = 100, costChange = 50});

        dict.Add("Harpoon", new ActWeaponCost() { level = 0, cost = 100, costChange = 50});

        dict.Add("Net", new ActWeaponCost() { level = 0, cost = 100, costChange = 50});
        return dict;
    }
    public Dictionary<string, ActWeaponCost> DicCostWeapon = DictCostWeapon();
}
