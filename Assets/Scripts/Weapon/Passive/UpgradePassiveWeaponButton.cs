using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Diagnostics; // Добавить для доступа к Stopwatch
using TMPro;
using UnityEngine.UI;

public class UpgradePassiveWeaponButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image image;
    
    public void ChoiceWeapon()
    {
        UpgradePassiveWeaponPanel.Instance.ChoiceWeapon(gameObject.name);
    }
    public void FillInfo(string nameImg)
    {
        image.sprite = Resources.Load<Sprite>(nameImg);

        description.text = DictionaryPassiveWeapon.Instance.GetDescriptionByKey(gameObject.name);
        
    }
}
