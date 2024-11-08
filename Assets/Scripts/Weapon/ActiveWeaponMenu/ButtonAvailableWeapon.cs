using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAvailableWeapon : MonoBehaviour
{
    private string weaponName;

    public void FillInfo(string weaponName)
    {
        this.weaponName = weaponName;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(weaponName);
        gameObject.name = weaponName;
    }

    public void SelectWeapon()
    {
        WeaponSelectionPanel.Instance.SelectWeapon(weaponName);
    }
}
