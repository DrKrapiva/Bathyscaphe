using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Button buttonBuy;
    [SerializeField] private Image iconImage;
    void Start()
    {
        ShowInfo();
    }

    private void ShowInfo()
    {
        nameText.text = ShopDictionary.Instance.DicShop[gameObject.name].Name.ToString();
        coinText.text = ShopDictionary.Instance.DicShop[gameObject.name].coin.ToString();
        moneyText.text = ShopDictionary.Instance.DicShop[gameObject.name].money.ToString();

        iconImage.sprite = Resources.Load<Sprite>("UI/Shop/" + gameObject.name);

        buttonBuy.interactable = !ShopDictionary.Instance.DicShop[gameObject.name].isBuy;
    }

    public void Buy()
    {
        //списывать деньги
        ShopDictionary.Instance.ChangeIsBuy(gameObject.name, true);

        GameController.Instance.ChangeCoin(ShopDictionary.Instance.DicShop[gameObject.name].coin);
        ShopPanel.Instance.ShowCoin();

        ShowInfo();
    }
}
