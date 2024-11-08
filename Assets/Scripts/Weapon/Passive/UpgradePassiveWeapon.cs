using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePassiveWeapon : MonoBehaviour
{
    private List<string> availableWeapons = new List<string>();
    private List<string> addedWeapons = new List<string>();
    private List<string> maxUpgradedWeapons = new List<string>();
    private List<string> usedWeapons = new List<string>();
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject prefabPanel;
    public static UpgradePassiveWeapon Instance
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
    static private UpgradePassiveWeapon _instance;
    private void Start()//было в Awake, но не успевал выполнить, появлялась ошибка
    {
        availableWeapons = DictionaryPassiveWeapon.Instance.GetKeysFromDictionary();
    }

    public void ReachingMaxLevelWeapon(string weaponName)
    {
        // Убираем оружие из списка доступных оружий, если оно там есть
        if (availableWeapons.Contains(weaponName))
        {
            availableWeapons.Remove(weaponName);
        }

        // Убираем оружие из списка добавленных оружий, если оно там есть
        if (addedWeapons.Contains(weaponName))
        {
            addedWeapons.Remove(weaponName);
        }

        // Добавляем оружие в список оружий максимального уровня, если его там еще нет
        if (!maxUpgradedWeapons.Contains(weaponName))
        {
            maxUpgradedWeapons.Add(weaponName);
        }
    }
    public List<string> RandomSelectWeaponsWithBias()
    {
        List<string> selectedWeapons = new List<string>();
        Dictionary<string, int> weaponWeights = new Dictionary<string, int>();

        // Назначаем базовый вес всем доступным оружиям
        foreach (string weapon in availableWeapons)
        {
            weaponWeights[weapon] = 1; // Базовый вес
        }

        // Увеличиваем вес для добавленных оружий
        foreach (string addedWeapon in addedWeapons)
        {
            if (weaponWeights.ContainsKey(addedWeapon))
            {
                weaponWeights[addedWeapon] += 2; // Увеличиваем шанс выбора, повышая вес
            }
        }

        // Преобразуем взвешенный список в список, который можно использовать для случайного выбора
        List<string> weightedList = new List<string>();
        foreach (KeyValuePair<string, int> entry in weaponWeights)
        {
            for (int i = 0; i < entry.Value; i++) // Добавляем оружие в список несколько раз в зависимости от его веса
            {
                weightedList.Add(entry.Key);
            }
        }

        // Случайно выбираем три уникальных оружия из взвешенного списка
        while (selectedWeapons.Count < 3 && weightedList.Count > 0)
        {
            int randomIndex = Random.Range(0, weightedList.Count);
            string selectedWeapon = weightedList[randomIndex];
            if (!selectedWeapons.Contains(selectedWeapon))
            {
                selectedWeapons.Add(selectedWeapon);
            }

        }

        return selectedWeapons;
    }

    public void ShowUpgradeWeaponPanel()
    {
        //GameObject panelClone = Instantiate(prefabPanel, canvas, false);
        prefabPanel.SetActive(true);
        UpgradePassiveWeaponPanel.Instance.ShowUpgradeWeaponPanel();
        Time.timeScale = 0f;
    }
    public void ChooseWeapon(string weaponName)
    {
        if (!usedWeapons.Contains(weaponName))
        {
            usedWeapons.Add(weaponName);
        }
        if (!addedWeapons.Contains(weaponName))
        {
            addedWeapons.Add(weaponName);
            PassiveWeaponClasses.Instance.CreateNewController(weaponName);
        }
        else
        {
            switch (weaponName)
            {
                case "ForceField":
                    ForceFieldController.Instance.ChangeParamPassiveWeapon();
                    break;
                case "DeepSeaBombsFromAShip":
                    DeepSeaBombsFromAShipController.Instance.ChangeParamPassiveWeapon();
                    break;
                case "AccurateShellingFromAShip":
                    AccurateShellingFromAShipController.Instance.ChangeParamPassiveWeapon();
                    break;
                case "DroneAssistants":
                    DroneAssistantsController.Instance.ChangeParamPassiveWeapon();
                    break;
                case "DischargeElectricity":
                    DischargeElectricityController.Instance.ChangeParamPassiveWeapon();
                    break;
                case "Magnet":
                    MagnetController.Instance.ChangeParamPassiveWeapon();
                    break;
                case "CloudyWater":
                    CloudyWaterController.Instance.ChangeParamPassiveWeapon();
                    break;
                case "Regeneration":
                    RegenerationController.Instance.ChangeParamPassiveWeapon();
                    break;
                case "Mines":
                    MinesController.Instance.ChangeParamPassiveWeapon();
                    break;
                default:
                    break;

            }
        }
        HeroHPController.Instance.CheckHpAfterLevelingUp();
    }
    public bool CheckChosenWeapon(string weaponName)
    {
        if (!addedWeapons.Contains(weaponName))
        {
            return true;
        }
        else return false;
    }
    public List<string> GetUsedWeapons()
    {
        return usedWeapons;
    }
}
