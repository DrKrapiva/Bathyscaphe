using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotGarage : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI nameParam;
    [SerializeField] private TextMeshProUGUI _paramNow;
    [SerializeField] private TextMeshProUGUI _paramNext;
    

    public void FillInfo(GarageWeaponParam weaponParam)
    {
        nameParam.text = weaponParam.paramName;

        //  определяем, какой тип данных использовать
        if (weaponParam.isStringParam)
        {
            _paramNow.text = weaponParam.paramString;
            _paramNext.text = weaponParam.nextParamString;
        }
        else
        {
            _paramNow.text = weaponParam.param.ToString();
            _paramNext.text = weaponParam.nextParam.ToString();
        }
    }
    /*public void FillInfo()
    {
        nameSlot = gameObject.name;
        
        ShowParam();
    }
    private void ShowParam()
    {
        _background = Garage.Instance.Background(nameSlot);
        _weaponImage.sprite = Resources.Load<Sprite>(nameSlot);
        _backgroundImage.sprite = Resources.Load<Sprite>("BackgraundForWeapon/" + _background);
        nameWeapon.text = nameSlot + " " + _background;

        realCost = Garage.Instance.RealCostOfWeapon(nameSlot);
        cost.text = realCost.ToString();

        CheckCanBuy(realCost);
    }
    private void CheckCanBuy(int costBuy)
    {
        if(PlayerPrefs.GetInt("Coin") >= costBuy)
            buy.interactable = true;
        else buy.interactable = false;
    }
    public void Buy()
    {
        
        GameController.Instance.ChangeCoin(-realCost);

        Garage.Instance.UpdateParamDictionary(nameSlot);

        GaragePanel.Instance.ShowCoin();

        ShowParam();
    }*/
}
