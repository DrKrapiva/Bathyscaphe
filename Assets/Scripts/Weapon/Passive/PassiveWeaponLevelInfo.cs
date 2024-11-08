using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveWeaponLevel
{
    public int LevelOnLevel;
}
public class PassiveWeaponLevelInfo : MonoBehaviour
{
    public static PassiveWeaponLevelInfo Instance
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
    static private PassiveWeaponLevelInfo _instance;

    private void OnEnable()
    {
        AccurateShellingFromAShipController.OnLevelUp += ChangeLevelOnlevel;
        CloudyWaterController.OnLevelUp += ChangeLevelOnlevel;
        DeepSeaBombsFromAShipController.OnLevelUp += ChangeLevelOnlevel;
        DischargeElectricityController.OnLevelUp += ChangeLevelOnlevel;
        DroneAssistantsController.OnLevelUp += ChangeLevelOnlevel;
        ForceFieldController.OnLevelUp += ChangeLevelOnlevel;
        MagnetController.OnLevelUp += ChangeLevelOnlevel;
        MinesController.OnLevelUp += ChangeLevelOnlevel;
        RegenerationController.OnLevelUp += ChangeLevelOnlevel;
    }
    private void OnDisable()
    {
        AccurateShellingFromAShipController.OnLevelUp -= ChangeLevelOnlevel;
        CloudyWaterController.OnLevelUp -= ChangeLevelOnlevel;
        DeepSeaBombsFromAShipController.OnLevelUp -= ChangeLevelOnlevel;
        DischargeElectricityController.OnLevelUp -= ChangeLevelOnlevel;
        DroneAssistantsController.OnLevelUp -= ChangeLevelOnlevel;
        ForceFieldController.OnLevelUp -= ChangeLevelOnlevel;
        MagnetController.OnLevelUp -= ChangeLevelOnlevel;
        MinesController.OnLevelUp -= ChangeLevelOnlevel;
        RegenerationController.OnLevelUp -= ChangeLevelOnlevel;
    }
    public static Dictionary<string, PassiveWeaponLevel> DictPassWeaponLevelOnlevel()
    {
        Dictionary<string, PassiveWeaponLevel> dict = new Dictionary<string, PassiveWeaponLevel>();
        dict.Add("ForceField", new PassiveWeaponLevel() { LevelOnLevel = 0 });
        dict.Add("DeepSeaBombsFromAShip", new PassiveWeaponLevel() { LevelOnLevel = 0 });
        dict.Add("AccurateShellingFromAShip", new PassiveWeaponLevel() {LevelOnLevel = 0 });
        dict.Add("DroneAssistants", new PassiveWeaponLevel() {LevelOnLevel = 0 });
        dict.Add("DischargeElectricity", new PassiveWeaponLevel() {LevelOnLevel = 0 });
        dict.Add("Magnet", new PassiveWeaponLevel() {LevelOnLevel = 0 });
        dict.Add("CloudyWater", new PassiveWeaponLevel() {LevelOnLevel = 0 });
        dict.Add("Regeneration", new PassiveWeaponLevel() {LevelOnLevel = 0 });
        dict.Add("Mines", new PassiveWeaponLevel() {LevelOnLevel = 0 });
        return dict;
    }
    public Dictionary<string, PassiveWeaponLevel> DicPassWeaponLevelOnlevel = DictPassWeaponLevelOnlevel();

    public void ChangeLevelOnlevel(string key)
    {
        if (DicPassWeaponLevelOnlevel.ContainsKey(key))
        {
            DicPassWeaponLevelOnlevel[key].LevelOnLevel++;
        }
    }

    public int GetLevelOnlevel(string key)
    {
        if (DicPassWeaponLevelOnlevel.ContainsKey(key))
            return DicPassWeaponLevelOnlevel[key].LevelOnLevel;
        return 0;
    }
}
