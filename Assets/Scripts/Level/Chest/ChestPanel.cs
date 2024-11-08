using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestPanel : MonoBehaviour
{
    [SerializeField] Image weapon;
    [SerializeField] TextMeshProUGUI weaponText;
    [SerializeField] TextMeshProUGUI coinText;
    public void FillInfo(int coin, string weaponName, int coolDown)
    {
        weapon.sprite = Resources.Load<Sprite>(weaponName);
        LevelController.Instance.CountCoins(coin);
        MessageWeapon(coolDown);
        MessageCoin(coin);
    }
    private void MessageWeapon(int coolDown)
    {
        weaponText.text = "Перезарядка снижена на " +coolDown+ " %";
    }
    private void MessageCoin(int coin)
    {
        coinText.text = coin + " монет получено";
    }
    public void ClosePanel()
    {
        //gameObject.SetActive(false);
        Time.timeScale = 1f;

        Destroy(gameObject, 0.1f);
    }

}
