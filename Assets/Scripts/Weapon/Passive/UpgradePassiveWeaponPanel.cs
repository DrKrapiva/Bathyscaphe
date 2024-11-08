using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePassiveWeaponPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonList;
    [SerializeField] private GameObject buttonChoice;
    private List<string> weaponNames = new List<string>(); 
    private List<GameObject> passiveWeaponIcons = new List<GameObject>();
    private string weaponNameChoice;
    private bool isPanelActive = false;
    public static UpgradePassiveWeaponPanel Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        
        buttonChoice.GetComponent<Button>().interactable = false;
    }
    static private UpgradePassiveWeaponPanel _instance;

    private void AddPassiveWeaponIcons()
    {
        PassiveWeaponSlot[] weaponSlots = FindObjectsOfType<PassiveWeaponSlot>();
        foreach (var slot in weaponSlots)
        {
            passiveWeaponIcons.Add(slot.gameObject);
            Debug.Log("Found PassiveWeaponSlot: " + slot.gameObject.name);
        }
        
        
    }
    private void ShowWeapon()
    {
        weaponNames = UpgradePassiveWeapon.Instance.RandomSelectWeaponsWithBias();

        for (int i = 0; i < buttonList.Count; i++)
        {
            if (i < weaponNames.Count)
            {
                
                buttonList[i].name = weaponNames[i];
                
                buttonList[i].GetComponent<UpgradePassiveWeaponButton>().FillInfo("Prefab/PassiveWeaponPictures/" + weaponNames[i]);
            }
        }
    }
    public void ChoiceWeapon(string buttonName)
    {
        weaponNameChoice = buttonName;

        buttonChoice.GetComponent<Button>().interactable = true;
    }
    public void ClosePanel()
    {
        //отравляю имя выбранного оружия
        if (weaponNameChoice != null)
        {
            LevelController.Instance.RenameLastWeaponName(weaponNameChoice);


            if (UpgradePassiveWeapon.Instance.CheckChosenWeapon(weaponNameChoice))
                ReplaceImage(weaponNameChoice);
            UpgradePassiveWeapon.Instance.ChooseWeapon(weaponNameChoice);

            Time.timeScale = 1f;
            
            isPanelActive = false;
            //gameObject.SetActive(false); // Скрываем панель
            
            StartCoroutine(WaitForSoundAndHidePanel(0.1f));
        }
    }
    private IEnumerator WaitForSoundAndHidePanel(float delay)
    {
        // Ждем до завершения воспроизведения звука
        yield return new WaitForSeconds(delay);

        // Скрываем панель после завершения звука
        gameObject.SetActive(false);
    }
    private void ReplaceImage(string weaponName)
    {
        Debug.Log("ReplaceImage");

        // Найти объект с именем weaponName в списке passiveWeaponIcons
        GameObject iconToReplace = passiveWeaponIcons.Find(icon => icon.name == weaponName);

        if (iconToReplace != null)
        {
            // Получить компонент PassiveWeaponSlot
            PassiveWeaponSlot slot = iconToReplace.GetComponent<PassiveWeaponSlot>();

            if (slot != null)
            {
                // Вызвать метод ShowLevel
                slot.ShowLevel();
                Debug.Log("ShowLevel вызван для: " + weaponName);
            }
            else
            {
                Debug.LogError("Компонент PassiveWeaponSlot не найден на объекте: " + weaponName);
            }
        }
        else
        {
            Debug.Log("Объект с именем " + weaponName + " не найден в списке passiveWeaponIcons.");
        }
        
    }
    public void ShowUpgradeWeaponPanel()
    {
        ShowWeapon(); // Заполняем панель перед показом
        AddPassiveWeaponIcons(); // Добавляем иконки перед показом
        
        isPanelActive = true;
        Time.timeScale = 0f; // Ставим игру на паузу
    }

    public bool IsUpgradePanelActive()
    {
        return isPanelActive;
    }
}
