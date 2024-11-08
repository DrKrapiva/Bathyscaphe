using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private GameObject prefabUpgradeSlot;
    [SerializeField] private Transform content;
    [SerializeField] private TextMeshProUGUI coin;
    public static UpgradePanel Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        
        ShowCoin();
        GenerationUpgradeSlot();
    }
    static private UpgradePanel _instance;

    public void GenerationUpgradeSlot()
    {
        foreach (var appSlot in DictionaryUprades.Instance.DictUpgrade.Keys)
        {
            GameObject slot = Instantiate(prefabUpgradeSlot, content, false);
            slot.name = appSlot;
            
        }
    }
    public void ExitPanel()
    {
        DictionaryUprades.Instance.SaveInfoLevelHeroUpgrade(PlayerPrefs.GetString("NowHero"));

        Destroy(gameObject, 0.1f);
    }
    public void ShowCoin()
    {
        coin.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    
    
    
}
