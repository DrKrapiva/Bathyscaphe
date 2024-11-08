using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponSelectionPanel : MonoBehaviour
{
    private List<string> availableWeaponNames = new List<string>() { "RocketMini", "Rocket", "Explosion", "Harpoon", "Net" };
    private List<string> selectedWeaponNames = new List<string>() { "", "", "" };
    [SerializeField] private GameObject prefabButtonAvailableWeapon;
    [SerializeField] private GameObject panelAvailableWeapon;
    [SerializeField] private GameObject panelSpecialWeapon;
    [SerializeField] private TextMeshProUGUI specialWeapon;
    [SerializeField] private Transform contentForPanel;
    [SerializeField] private Transform contentForButtons;
    [SerializeField] private GameObject[] selectedWeaponButtons;
    [SerializeField] private Button playButton;
    private int currentSelectedIndex;
    private List<string> specialWeapons;
    public static WeaponSelectionPanel Instance
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

    static private WeaponSelectionPanel _instance;

    private void Start()
    {
        GetAvailableWeaponNames();
        LoadSelectedWeaponNames();

        specialWeapons = MapPanel.Instance.SpecialWeapon();
        //SpecialWeaponInfo();

        LoadImageToButton();
        UpdatePlayButtonState();
    }

    private void GetAvailableWeaponNames()
    {
        // Получение списка доступных оружий (пустой метод для примера)
    }
    
    private void CreateButtonAvailableWeapon()
    {
        foreach (Transform child in contentForButtons)
        {
            Destroy(child.gameObject);
        }

        foreach (var availableWeapon in availableWeaponNames)
        {
            GameObject buttonAvailableWeapon = Instantiate(prefabButtonAvailableWeapon, contentForButtons, false);
            buttonAvailableWeapon.GetComponent<ButtonAvailableWeapon>().FillInfo(availableWeapon);
        }
    }

    private void LoadImageToButton()
    {
        for (int i = 0; i < selectedWeaponNames.Count; i++)
        {
            if (selectedWeaponNames[i] == "")
            {
                selectedWeaponButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Question");
            }
            else
            {
                selectedWeaponButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(selectedWeaponNames[i]);
            }
        }
    }

    private void LoadSelectedWeaponNames()
    {
        selectedWeaponNames = SaveGame.Instance.LoadSelectedWeaponNames();
    }

    private void SaveSelectedWeaponNames()
    {
        SaveGame.Instance.SaveSelectedWeaponNames(selectedWeaponNames);
    }

    public void SelectWeapon(string selectedWeaponName)
    {
        AddSelectedWeaponToSelectedWeaponNamesList(currentSelectedIndex, selectedWeaponName);
        //StartCoroutine(ClosePanelWithDelay(0.1f));
        //Exit();
        //panelAvailableWeapon.SetActive(false);
        ClosePanelAvailableWeapon();
    }

    private IEnumerator ClosePanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Задержка на 0.1 сек
        //ClosePanelAvailableWeapon(); // Вызываем метод после задержки
    }

    private void AddSelectedWeaponToSelectedWeaponNamesList(int index, string selectedWeapon)
    {
        selectedWeaponNames[index] = selectedWeapon;
        LoadImageToButton();
        //SpecialWeaponInfo();
        UpdatePlayButtonState();
    }

    public void ClearSelectedWeaponName(int index)
    {
        selectedWeaponNames[index] = "";
        LoadImageToButton();
       // SpecialWeaponInfo();
        UpdatePlayButtonState();
    }

    public void Play()
    {
        SaveSelectedWeaponNames();
        //SceneManager.LoadScene(1);
        SceneManager.LoadScene(MapPanel.Instance.SceneNumber());
    }

    public void Exit()
    {
        Destroy(gameObject, 0.1f);
    }

    public void OpenPanelAvailableWeapon(int selectedIndex)
    {
        currentSelectedIndex = selectedIndex;
        panelAvailableWeapon.SetActive(true);
        CreateButtonAvailableWeapon();
    }

    public void ClosePanelAvailableWeapon()
    {
        Debug.Log("ClosePanelAvailableWeapon");
        StartCoroutine(ClosePanelAvailableWeaponWithDelay(0.1f));
    }

    private IEnumerator ClosePanelAvailableWeaponWithDelay(float delay)
    {
        //Debug.Log("IEnumerator ClosePanelAvailableWeaponWithDelay");
        //Debug.Log("delay " + delay);
        //Debug.Log("Panel is active before delay: " + panelAvailableWeapon.activeSelf);
        Debug.Log("Time scale: " + Time.timeScale);
        yield return new WaitForSeconds(delay); // Задержка на 0.1 сек
        //Debug.Log("Panel is active after  delay: " + panelAvailableWeapon.activeSelf);
        Debug.Log("ClosePanel");
        panelAvailableWeapon.SetActive(false); // Вызываем метод после задержки
        
    }

    public void ClearAll()
    {
        for (int i = 0; i < selectedWeaponNames.Count; i++)
        {
            selectedWeaponNames[i] = "";
        }
        LoadImageToButton();
        //SpecialWeaponInfo();
        UpdatePlayButtonState();
    }
    
    private void UpdatePlayButtonState()
    {
        if (specialWeapons.Count > 0)
        {
            bool allSpecialWeaponsSelected = true;

            foreach (var specialWeaponName in specialWeapons)
            {
                if (!selectedWeaponNames.Contains(specialWeaponName))
                {
                    allSpecialWeaponsSelected = false;
                    break;
                }
            }

            if (allSpecialWeaponsSelected)
            {
                panelSpecialWeapon.SetActive(false);
                playButton.interactable = true;
                //UpdatePlayButtonState();
            }
            else
            {
                playButton.interactable = false;
                panelSpecialWeapon.SetActive(true);
                specialWeapon.text = string.Join(", ", specialWeapons);
            }
        }
        else
        {
            panelSpecialWeapon.SetActive(false);
            //UpdatePlayButtonState();
            bool canPlay = false;
            if (specialWeapons.Count == 0)
            {
                foreach (var weaponName in selectedWeaponNames)
                {
                    if (weaponName != "")
                    {
                        canPlay = true;
                        break;
                    }
                }

                playButton.interactable = canPlay;
            }

    }
}}
