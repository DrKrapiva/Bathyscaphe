using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSet
{
    public string Name;
    public int coin;
    public int money;
    public bool isBuy;
}
public class ShopDictionary : MonoBehaviour
{
    public static ShopDictionary Instance
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
    private void Start()
    {
        LoadIsBuyList();
    }
    static private ShopDictionary _instance;

    public static Dictionary<string, ShopSet> DictShop()
    {
        Dictionary<string, ShopSet> dict = new Dictionary<string, ShopSet>();
        dict.Add("set1", new ShopSet() { Name = "малый", coin = 100, money = 10, isBuy = false });
        dict.Add("set2", new ShopSet() { Name = "средний", coin = 1000, money = 100, isBuy = false });
        dict.Add("set3", new ShopSet() { Name = "большой", coin = 10000, money = 1000, isBuy = false });
        return dict;
    }
    public Dictionary<string, ShopSet> DicShop = DictShop();

    public void ChangeIsBuy(string key, bool isBuy)
    {
        DicShop[key].isBuy = isBuy;
        SaveIsBuyList();
    }
    public int NumberKeysFromDictionary()
    {
        return DicShop.Count;
    }
    private void SaveIsBuyList()
    {
        List<bool> isBuyList = new List<bool>();
        foreach (var item in DicShop)
        {
            isBuyList.Add(item.Value.isBuy);
        }
        SaveGame.Instance.SaveShopListIsBuy(isBuyList);
    }
    public void LoadIsBuyList()
    {
        List<bool> newIsBuyList = SaveGame.Instance.LoadShopListIsBuy();
        int index = 0;
        foreach (var key in DicShop.Keys)
        {
            if (index < newIsBuyList.Count)
            {
                DicShop[key].isBuy = newIsBuyList[index];
                index++;
            }
            else
            {
                break;
            }
        }
    }
}
