using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageWeaponButton : MonoBehaviour
{
    private void Start()
    {
        ShowInfo();
    }
    private void ShowInfo()
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(gameObject.name);
    }
    public void ShowParam()
    {
        GaragePanel.Instance.ShowInfo(gameObject.name);
    }
}
