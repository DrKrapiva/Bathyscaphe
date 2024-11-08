using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject prefadSlot;
    [SerializeField] private TextMeshProUGUI coinText;
    public static ShopPanel Instance
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
    static private ShopPanel _instance;
    void Start()
    {
        ShowCoin();
        GeneateSlots();
    }
    public void ShowCoin()
    {
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    private void GeneateSlots()
    {
        foreach(var slot in ShopDictionary.Instance.DicShop.Keys)
        {
            GameObject slotClon = Instantiate(prefadSlot, canvas, false);
            slotClon.name = slot;
        }
    }
    public void ClosePanel()
    {
        Destroy(gameObject, 0.1f);
    }
}
