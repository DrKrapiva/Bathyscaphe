using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveWeaponSlot : MonoBehaviour
{
    [SerializeField] private Image imageLevel;
    private int maxLevel;
    private string weaponName;
    
    public void SetWeaponName(string name)
    {
        weaponName = name;
        gameObject.name = name; // Обновляем имя объекта
    }
    public void FillInfo()
    {
        //Debug.Log(gameObject.name = " Slot name");

        if (!string.IsNullOrEmpty(weaponName))
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Prefab/PassiveWeaponPictures/" + weaponName);
            maxLevel = PassiveWeaponClasses.Instance.MaxWeaponLevel;
            ShowLevel();
        }
        else
        {
            Debug.LogError("Weapon name is not set.");
        }
    }
    //сделать отдельную функцию на изменение уровня
    public void ShowLevel()
    {
        //Debug.Log("ShowLevel");
        int myLevel = PassiveWeaponLevelInfo.Instance.GetLevelOnlevel(gameObject.name);
        imageLevel.fillAmount = (float)myLevel / maxLevel;
    }
}
