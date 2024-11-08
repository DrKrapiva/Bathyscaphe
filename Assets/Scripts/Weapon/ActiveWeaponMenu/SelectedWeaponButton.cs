using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedWeaponButton : MonoBehaviour
{
    public int buttonIndex;

    public void OnClick()
    {
        WeaponSelectionPanel.Instance.OpenPanelAvailableWeapon(buttonIndex);
    }
}
