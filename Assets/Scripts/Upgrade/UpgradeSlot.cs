using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _upName;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private TextMeshProUGUI _level;
    private Upgrades upgrade;
    [SerializeField] private Button buttonAdd;
    [SerializeField] private GameObject coinImage;

    private void Start()
    {
        FillInfo();
    }
    public void FillInfo()
    {
        upgrade = DictionaryUprades.Instance.DictUpgrade[gameObject.name];

        CheckInterectbleButtonAdd();

        ShowParam();
    }
    private void ShowParam()
    {
        if (!DictionaryUprades.Instance.IsMaxLevel(gameObject.name))
        {
            _description.text = $"+ {DictionaryUprades.Instance.DictUpgrade[gameObject.name].HowMuchAdd * (DictionaryUprades.Instance.DictUpgrade[gameObject.name].Level + 1)} {upgrade.Description}";
            _cost.text = DictionaryUprades.Instance.CountNowCostOfUpgrade(gameObject.name).ToString();
        }
        else
        {
            _description.text = $"+ {DictionaryUprades.Instance.DictUpgrade[gameObject.name].HowMuchAdd * (DictionaryUprades.Instance.DictUpgrade[gameObject.name].Level)} {upgrade.Description}";
            _cost.text = "";
            coinImage.SetActive(false);
        }

        _upName.text = upgrade.Name;
        
        _level.text = upgrade.Level + "/" + upgrade.MaxLevel;
    }
    
    public void ButtonAdd()
    {
        GameController.Instance.ChangeCoin(-DictionaryUprades.Instance.CountNowCostOfUpgrade(gameObject.name));
        GetUpgrade(gameObject.name);
    }
    public void GetUpgrade(string key)
    {
        DictionaryUprades.Instance.DictUpgrade[key].Level++;
        DictionaryUprades.Instance.SaveInfoLevelHeroUpgrade(PlayerPrefs.GetString("NowHero"));
        ReloadSlot();
        CheckInterectbleButtonAdd();

        UpgradePanel.Instance.ShowCoin();
    }
    private void ReloadSlot()
    {
        ShowParam();
    }
    private void CheckInterectbleButtonAdd()
    {
        if (PlayerPrefs.GetInt("Coin") >= DictionaryUprades.Instance.CountNowCostOfUpgrade(gameObject.name) && upgrade.Level < upgrade.MaxLevel)
        {
            buttonAdd.interactable = true;
        }
        else buttonAdd.interactable = false;
    }
}
