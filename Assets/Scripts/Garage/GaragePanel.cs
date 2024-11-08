using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GaragePanel : DictionaryGarageWeaponParameters
{
    [SerializeField] private GameObject prefabGarageSlot;
    [SerializeField] private Transform canvas;
    [SerializeField] private TextMeshProUGUI coin;

    [SerializeField] private GameObject prefabWeaponButtonGarage;
    [SerializeField] private Transform weaponButtonContent;
    [SerializeField] private Image weaponImage;
    [SerializeField] private List<Image> weaponRarity;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI costOfUpgrade;
    [SerializeField] private Button buy;
    private string shortName;
    private List<string> weaponList;
    public static GaragePanel Instance
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

    static private GaragePanel _instance;

    public void FillInfo(List<string> weapon)
    {
        ShowCoin();

        weaponList = weapon;

        CreateButtonWeapon();

        ShowInfo(weaponList[0]);
    }
    public void ShowCoin()
    {
        coin.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    public void CreateButtonWeapon()
    {
        foreach (var weapon in weaponList)
        {
            GameObject weaponButton = Instantiate(prefabWeaponButtonGarage, weaponButtonContent, false);
            weaponButton.name = weapon;
        }
    }
    public void ShowInfo(string shortWeaponName)
    {
        shortName = shortWeaponName;
        weaponImage.sprite = Resources.Load<Sprite>(shortWeaponName);
        if (weaponRarity.Count == Garage.Instance.GetListNameRarityCount())
        {
            for (int i = 0; i < weaponRarity.Count; i++)
            {
                weaponRarity[i].sprite = Resources.Load<Sprite>("UI/Gear");
            }

            for (int i = 0; i <= Garage.Instance.GetWeaponLevel(shortWeaponName); i++)
            {
                weaponRarity[i].sprite = Resources.Load<Sprite>("UI/GearGreen");
            }
        }

        string fullWeaponName = Garage.Instance.GetFullWeaponName(shortWeaponName);
        string fullNextLevelWeaponName = Garage.Instance.GetFullWeaponName(shortWeaponName, 1);

        weaponName.text = ActivWeapon.Instance.DicActWeapon[fullWeaponName].name;

        costOfUpgrade.text = Garage.Instance.RealCostOfWeapon(shortWeaponName).ToString();

        FillDictionary(fullWeaponName, fullNextLevelWeaponName);
        List<string> weaponGarageParam = DicGarageWeaponParam.Keys.ToList();
        foreach (Transform child in canvas.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var param in weaponGarageParam)
        {
            GameObject paramSlot = Instantiate(prefabGarageSlot, canvas, false);
            paramSlot.GetComponent<SlotGarage>().FillInfo(DicGarageWeaponParam[param]);
        }

        CheckCanBuy(Garage.Instance.RealCostOfWeapon(shortWeaponName));


    }
    private void CheckCanBuy(int costBuy)
    {
        if (PlayerPrefs.GetInt("Coin") >= costBuy && Garage.Instance.GetWeaponLevel(shortName) + 1 < Garage.Instance.GetListNameRarityCount())
            buy.interactable = true;
        else buy.interactable = false;
    }
    public void Buy()
    {

        GameController.Instance.ChangeCoin(-Garage.Instance.RealCostOfWeapon(shortName));

        Garage.Instance.UpdateParamDictionary(shortName);

        GaragePanel.Instance.ShowCoin();

        ShowInfo(shortName);
    }
    public void ExitPanel()
    {
        Destroy(gameObject, 0.1f);
    }

}
