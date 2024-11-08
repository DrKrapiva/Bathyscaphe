using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
class SaveNeed
{
    public List<int> levelRarity;
}
class SaveListInt
{
    public List<int> listInt;
}
class SaveInt
{
    public int Int;
}
class SaveBoolean
{
    public List<bool> value;
}
class SaveBool
{
    public bool value;
}
class SaveList
{
    public List<string> selectedWeaponNames;
}
class SaveListPassiveWeaponLevel
{
    public List<int> levelsPassiveWeapon;
}
public class SaveGame : DictionaryCostOfActiveWeapon
{
    [SerializeField] private GameObject dict;
    private bool isFirstLaunch = true;
    public static SaveGame Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        
            _instance = this;
            

            // Проверяем, был ли это первый запуск
            isFirstLaunch = LoadIsFirstLaunch();
            Debug.Log("Первый запуск: " + isFirstLaunch);

            if (isFirstLaunch)
            {
                // Выполняем действия для первого запуска
                Debug.Log("Это первый запуск игры. Включаем музыку.");
                SaveIsMuted(false); // Включаем музыку по умолчанию
                SaveIsFirstLaunch(false); // Сохраняем, что первый запуск завершен
            }
        
    }
    static private SaveGame _instance;
    // Функция для сохранения флага первого запуска
    public void SaveIsFirstLaunch(bool isFirst)
    {
        SaveBool firstLaunch = new SaveBool { value = isFirst };
        SaveSystem.Set("IsFirstLaunch", firstLaunch);
    }

    // Функция для загрузки флага первого запуска
    public bool LoadIsFirstLaunch()
    {
        var firstLaunch = SaveSystem.Get<SaveBool>("IsFirstLaunch");
        if (firstLaunch != null)
        {
            return firstLaunch.value;
        }
        return true; // Если данных нет, это первый запуск
    }
    public void NewGame()
    {
        CreateRarityLevelActiveWeapon();
        CreateLevelHeroUpgrade(PlayerPrefs.GetString("NowHero"));
        CreateSelectedWeaponNames();
        PlayerPrefs.SetInt("Coin", 1000);
        CreateListPassiveWeaponLevels();

        CreateListProgressSumAchiv();
        CreateListProgressOnLevelAchiv();
        CreateListProgressMaxOnLevelAchiv();

        CreateListAchievementsSumLevels();
        CreateListAchievementsMaxOnLevelLevels();

        CreateListAchivRewardsSum();
        CreateListAchivRewardsOnLevel();

        CreateShopListIsBuy();

        CreateListAmountItemLast();
        CreateListAmountItemBest();
        CreateStageNumber();

        Debug.Log("isFirstLaunch " + isFirstLaunch);
        if (!isFirstLaunch)
        {
            Debug.Log("isFirstLaunch " + isFirstLaunch);
            SaveIsMuted(false);
            isFirstLaunch = true;
        }
    }

    public void CreateListPassiveWeaponLevels()
    {
        List<int> passiveWeaponLevels = new List<int>();
        int count = DictionaryPassiveWeapon.Instance.NumberKeysFromDictionary();
        for (int i = 0; i < count; i++)
        {
            passiveWeaponLevels.Add(0);
        }
        SaveListPassiveWeaponLevels(passiveWeaponLevels);
    }
    public void CreateListAchievementsSumLevels()
    {
        List<int> achievementLevels = new List<int>();
        int count = AchievementController.Instance.GetNumberOfKeysInDictionary(AchievementController.Instance.DicAchievementsSum);
        for (int i = 0; i < count; i++)
        {
            achievementLevels.Add(0);
        }
        SaveListAchievementsSumLevels(achievementLevels);
    }
    public void CreateListAchievementsMaxOnLevelLevels()
    {
        List<int> achievementLevels = new List<int>();
        int count = AchievementController.Instance.GetNumberOfKeysInDictionary(AchievementController.Instance.DicAchievementsMaxOnLevel);
        for (int i = 0; i < count; i++)
        {
            achievementLevels.Add(0);
        }
        SaveListAchievementsMaxOnLevelLevels(achievementLevels);
    }
    public void CreateListProgressOnLevelAchiv()
    {
        List<int> progress = new List<int>();
        int count = DictionaryAchievementProgress.Instance.NumberKeysFromDictionary();
        for (int i = 0; i < count; i++)
        {
            progress.Add(0);
        }
        SaveListProgressOnLevelAchiv(progress);
    }
    public void CreateListProgressMaxOnLevelAchiv()
    {
        List<int> progress = new List<int>();
        int count = DictionaryAchievementProgress.Instance.NumberKeysFromDictionary();
        for (int i = 0; i < count; i++)
        {
            progress.Add(0);
        }
        SaveListProgressMaxOnLevelAchiv(progress);
    }
    public void CreateListProgressSumAchiv()
    {
        List<int> progress = new List<int>();
        int count = DictionaryAchievementProgress.Instance.NumberKeysFromDictionary();
        for (int i = 0; i < count; i++)
        {
            progress.Add(0);
        }
        SaveListProgressSumAchiv(progress);
    }
    public void CreateListAchivRewardsSum()
    {
        List<int> rewards = new List<int>();
        int count = DictionaryAchievementRewards.Instance.NumberKeysFromDictionary(DictionaryAchievementRewards.Instance.DicAchievementsRewardsSum);
        for (int i = 0; i < count; i++)
        {
            rewards.Add(0);
        }
        SaveListAchivRewardsSum(rewards);
    }
    public void CreateListAchivRewardsOnLevel()
    {
        List<int> rewards = new List<int>();
        int count = DictionaryAchievementRewards.Instance.NumberKeysFromDictionary(DictionaryAchievementRewards.Instance.DicAchievementsRewardsOnLevel);
        for (int i = 0; i < count; i++)
        {
            rewards.Add(0);
        }
        SaveListAchivRewardsOnLevel(rewards);
    }
    public void CreateSelectedWeaponNames()
    {
        List<string> selectedWeaponNames = new List<string>() { "", "", ""};
        SaveSelectedWeaponNames(selectedWeaponNames);
    }
    public void CreateRarityLevelActiveWeapon()
    {
        List<int> levelRarity = new List<int>();
        foreach(var key in DicCostWeapon.Keys)
        {
            levelRarity.Add(0);
        }
        SaveRarityLevelActiveWeapon(levelRarity);
    }
    public void CreateLevelHeroUpgrade(string heroName)
    {
        List<int> levelUpdrade = new List<int>();
        
        int count = DictionaryUprades.Instance.NumberKeysFromDictionary();
        for (int i = 0; i < count; i++)
        {
            levelUpdrade.Add(0);
        }
        SaveLevelHeroUpgrade(levelUpdrade, heroName);
    }
    public void CreateShopListIsBuy()
    {
        List<bool> isBuy = new List<bool>();

        int count = ShopDictionary.Instance.NumberKeysFromDictionary();
        for(int i = 0; i < count; i++)
        {
            isBuy.Add(false);
        }
        SaveShopListIsBuy(isBuy);
    }
    public void CreateIsMusicPlay()
    {

    }
    public void CreateListAmountItemLast()
    {
        foreach (var task in TaskProgressDictionary.Instance.DicTask)
        {
            // Создаем новый список amountItemLast длиной 5 и заполняем его нулями
            List<int> amountItemLast = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                amountItemLast.Add(0);
            }
            //Debug.Log($"Task: {task.Key}, amountItemLast: {string.Join(", ", amountItemLast)}");
            SaveAmountItemLast(task.Key, amountItemLast);
        }
        
    }
    public void CreateListAmountItemBest()
    {
        foreach (var task in TaskProgressDictionary.Instance.DicTask)
        {
            // Создаем новый список amountItemLast длиной 5 и заполняем его нулями
            List<int> amountItemBest = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                amountItemBest.Add(0);
            }

            SaveAmountItemBest(task.Key, amountItemBest);
        }
    }
    public void CreateStageNumber()
    {
        foreach (var task in TaskProgressDictionary.Instance.DicTask)
        {
            int initialStageNumber = 0; // Начальное значение для stageNumber

            SaveStageNumber(task.Key, initialStageNumber);
        }
    }
    public void SaveListPassiveWeaponLevels(List<int> levels)
    {
        SaveListPassiveWeaponLevel saveListPassiveWeaponLevel = new SaveListPassiveWeaponLevel() { levelsPassiveWeapon = levels};
        SaveSystem.Set("ListPassiveWeaponLevels", saveListPassiveWeaponLevel);
    }
    public void SaveListAchievementsSumLevels(List<int> levels)
    {
        SaveListInt saveListAchievementsSumLevel = new SaveListInt() { listInt = levels};
        SaveSystem.Set("ListAchievementsSumLevels", saveListAchievementsSumLevel);
    }
    public void SaveListAchievementsMaxOnLevelLevels(List<int> levels)
    {
        SaveListInt saveListAchievementsMaxOnLevelLevel = new SaveListInt() { listInt = levels};
        SaveSystem.Set("ListAchievementsMaxOnLevelLevels", saveListAchievementsMaxOnLevelLevel);
    }
    public void SaveListProgressSumAchiv(List<int> progress)
    {
        SaveListInt saveListProgress = new SaveListInt() { listInt = progress};
        SaveSystem.Set("ListProgressSumAchiv", saveListProgress);
    }
    public void SaveListProgressMaxOnLevelAchiv(List<int> progress)
    {
        SaveListInt saveListProgress = new SaveListInt() { listInt = progress};
        SaveSystem.Set("ListProgressMaxOnLevelAchiv", saveListProgress);
    }
    public void SaveListProgressOnLevelAchiv(List<int> progress)
    {
        SaveListInt saveListProgress = new SaveListInt() { listInt = progress};
        SaveSystem.Set("ListProgressOnLevelAchiv", saveListProgress);
    }
    public void SaveListAchivRewardsSum(List<int> rewards)
    {
        SaveListInt saveListAchivRewards = new SaveListInt() { listInt = rewards };
        SaveSystem.Set("ListProgressAchivRewardsSum", saveListAchivRewards);
    }
    public void SaveListAchivRewardsOnLevel(List<int> rewards)
    {
        SaveListInt saveListAchivRewards = new SaveListInt() { listInt = rewards };
        SaveSystem.Set("ListProgressAchivRewardsOnLevel", saveListAchivRewards);
    }
    public void SaveSelectedWeaponNames(List<string> selectedWeaponName)
    {
        SaveList listSelectedWeapon = new SaveList() { selectedWeaponNames = selectedWeaponName};
        SaveSystem.Set("SelectedWeaponNames", listSelectedWeapon);
    }
    public void SaveRarityLevelActiveWeapon( List<int> levelRar)
    {
        SaveNeed lvl = new SaveNeed() {  levelRarity = levelRar };
        SaveSystem.Set("SaveLevelRarityWeapon", lvl);
    }
    
    public void SaveLevelHeroUpgrade( List<int> levelRar, string heroName)
    {
        SaveNeed lvl = new SaveNeed() { levelRarity = levelRar };
        string saveKey = "SaveLevelHeroUpgrade" + heroName;
        SaveSystem.Set(saveKey, lvl);
    }
    public void SaveShopListIsBuy(List<bool> isBuyList)
    {
        SaveBoolean isBuy = new SaveBoolean() { value = isBuyList };
        SaveSystem.Set("isBuyList", isBuy);
    }
    public void SaveIsMuted(bool isMuted)
    {
        SaveBool isMusic = new SaveBool (){ value = isMuted };
        SaveSystem.Set("isMuted", isMusic);
    }
    public void SaveAmountItemLast(string taskName, List<int> listAmountItemLast)
    {
        SaveListInt saveListInt = new SaveListInt() { listInt = listAmountItemLast};
        string saveKey = "SaveAmountItemLast" + taskName;
       // Debug.Log($"Saving amountItemLast with key: {saveKey}");
        SaveSystem.Set(saveKey, saveListInt);
    }
    public void SaveAmountItemBest(string taskName, List<int> listAmountItemBest)
    {
        SaveListInt saveListInt = new SaveListInt() { listInt = listAmountItemBest };
        string saveKey = "SaveAmountItemBest" + taskName;
        SaveSystem.Set(saveKey, saveListInt);
    }
    public void SaveStageNumber(string taskName, int stageNumber)
    {
        SaveInt saveInt = new SaveInt() { Int = stageNumber };
        string saveKey = "StageNumber_" + taskName;
        SaveSystem.Set(saveKey, saveInt);
    }
    public List<int> LoadListPassiveWeaponLevels()
    {
        var listLevels = SaveSystem.Get<SaveListPassiveWeaponLevel>("ListPassiveWeaponLevels");
        return listLevels.levelsPassiveWeapon;
    }
    public List<int> LoadListAchievementsSumLevels()
    {
        var listLevels = SaveSystem.Get<SaveListInt>("ListAchievementsSumLevels");
        return listLevels.listInt;
    }
    public List<int> LoadListAchievementsMaxOnLevelLevels()
    {
        var listLevels = SaveSystem.Get<SaveListInt>("ListAchievementsMaxOnLevelLevels");
        return listLevels.listInt;
    }
    public List<int> LoadListProgressOnLevelAchiv()
    {
        var listInt = SaveSystem.Get<SaveListInt>("ListProgressOnLevelAchiv");
        return listInt.listInt;
    }
    public List<int> LoadListProgressMaxOnLevelAchiv()
    {
        var listInt = SaveSystem.Get<SaveListInt>("ListProgressMaxOnLevelAchiv");
        return listInt.listInt;
    }
    public List<int> LoadListProgressSumAchiv()
    {
        var listInt = SaveSystem.Get<SaveListInt>("ListProgressSumAchiv");
        return listInt.listInt;
    }
    public List<int> LoadListAchivRewardsSum()
    {
        var listInt = SaveSystem.Get<SaveListInt>("ListProgressAchivRewardsSum");
        return listInt.listInt;
    }
    public List<int> LoadListAchivRewardsOnLevel()
    {
        var listInt = SaveSystem.Get<SaveListInt>("ListProgressAchivRewardsOnLevel");
        return listInt.listInt;
    }
    public List<string> LoadSelectedWeaponNames()
    {
        var listSelectedWeapon = SaveSystem.Get<SaveList>("SelectedWeaponNames");
        return listSelectedWeapon.selectedWeaponNames;
    }
    public List<int> LoadRarityLevelActiveWeapon()
    {
        var lvl = SaveSystem.Get<SaveNeed>("SaveLevelRarityWeapon");
        return lvl.levelRarity;
    }

    public List<int> LoadLevelHeroUpgrade(string heroName)
    {
        string saveKey = "SaveLevelHeroUpgrade" + heroName;
        var lvl = SaveSystem.Get<SaveNeed>(saveKey);
        return lvl.levelRarity;
    }
    public List<bool> LoadShopListIsBuy()
    {
        var isBuy = SaveSystem.Get<SaveBoolean>("isBuyList");
        return isBuy.value;
    }
    public bool LoadIsMuted()
    {
        var isMusic = SaveSystem.Get<SaveBool>("isMuted");
        return isMusic.value;
    }
    public List<int> LoadAmountItemLast(string taskName)
    {
        string saveKey = "SaveAmountItemLast" + taskName;
        var list = SaveSystem.Get<SaveListInt>(saveKey);
        Debug.Log($"Loading amountItemLast with key: {saveKey}");
        return list.listInt;

    }
    public List<int> LoadAmountItemBest(string taskName)
    {
        string saveKey = "SaveAmountItemBest" + taskName;
        var list = SaveSystem.Get<SaveListInt>(saveKey);
        return list.listInt;

    }
    public int LoadStageNumber(string taskName)
    {
        string saveKey = "StageNumber_" + taskName;
        var savedData = SaveSystem.Get<SaveInt>(saveKey);

        if (savedData != null)
        {
            return savedData.Int;
        }
        else
        {
            Debug.LogWarning($"Stage number for task {taskName} not found. Returning 0 as default.");
            return 0; // Возвращаем 0 по умолчанию, если данных нет
        }
    }
}

