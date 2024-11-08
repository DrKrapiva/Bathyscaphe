using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject prefabPanelShop;
    [SerializeField] private Transform canvas;

    public void GenerateShopPanel()
    {
        GameObject shopPanel = Instantiate(prefabPanelShop, canvas, false);
    }
    
}
