using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hero
{
    public string Name;
    public float Hp;
    public float Speed;
    public float Armor;
    public float ArmorHP;
   // public float Attack;
}
public class DictionaryHero : MonoBehaviour
{
    public static DictionaryHero Instance
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
    static private DictionaryHero _instance;
    public static Dictionary<string, Hero> DictHero()
    {
        Dictionary<string, Hero> DictHero = new Dictionary<string, Hero>();
        DictHero.Add("bat1", new Hero() { Name = "Orange", Hp = 30.2f, Armor = 15,  Speed = 10 });
        DictHero.Add("bat2", new Hero() { Name = "Violet", Hp = 50, Armor = 25,  Speed = 20 });
        DictHero.Add("bat3", new Hero() { Name = "Blue", Hp = 70, Armor = 35,  Speed = 30 });
        return DictHero;
    }
    public Dictionary<string, Hero> DicHeroes = DictHero();

    public float Speed(string key)
    {
       return DicHeroes[key].Speed + DicHeroes[key].Speed * DictionaryUprades.Instance.HowMuchAdd("speed")/ 100;
    }
    public float Hp(string key)
    {
        return DicHeroes[key].Hp + DicHeroes[key].Hp * DictionaryUprades.Instance.HowMuchAdd("maxHP")/ 100;
    }
    public float Armor(string key)
    {
        return DicHeroes[key].Armor + DicHeroes[key].Armor * DictionaryUprades.Instance.HowMuchAdd("armor") / 100;
    }

}
