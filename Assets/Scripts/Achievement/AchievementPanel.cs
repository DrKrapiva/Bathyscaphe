using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementPanel : MonoBehaviour
{
    [SerializeField] private GameObject prefabAchievementSlot;
    [SerializeField] private Transform content;
    [SerializeField] private TextMeshProUGUI coin;

    private void Start()
    {
        GenerateAchievementSlots();
        ShowCoin();
    }

    private void GenerateAchievementSlots()
    {
        DictionaryAchievementProgress.Instance.LoadSaveListAchievementProgress();
        // Загрузка суммарного прогресса из сохранения
        //DictionaryAchievementProgress.Instance.DictionarySum();
        // Генерация слотов для суммарных достижений
        GenerateAchievementSlot(AchievementController.Instance.DicAchievementsSum, "Sum");
        // Загрузка прогресса текущего уровня из сохранения (если необходимо)
        //DictionaryAchievementProgress.Instance.DictionaryOnLevel();
        // Генерация слотов для достижений на уровне
        GenerateAchievementSlot(AchievementController.Instance.DicAchievementsMaxOnLevel, "MaxOnLevel");
    }

    private void GenerateAchievementSlot(Dictionary<string, Achievement> achievementsDict, string keySuffix)
    {
        foreach (var achievement in achievementsDict)
        {
            
                var slotInstance = Instantiate(prefabAchievementSlot, content);
                var slotScript = slotInstance.GetComponent<AcheivementSlotShow>();
                //string iconName = achievement.Key + keySuffix; // Формирование ключа для иконки

                // Получаем текущий прогресс (youHavePoints) для данного достижения
                //int youHavePoints = DictionaryAchievementProgress.Instance.GetAchievementsProgress(achievement.Value.Parameter, "OnLevel");

                // Используем функции для определения монет и необходимых очков
                //int coins = AchievementController.Instance.CoinsForAchievement(achievement.Key, keySuffix == "Sum");
                //int pointsNeeded = AchievementController.Instance.ValueForCheck(achievement.Key, keySuffix == "Sum");

                slotScript.FillInfo(
                    achievementsDict,
                    achievement.Key,
                    keySuffix,
                    AchievementController.Instance.MaxLevel
                );
            
        }
    }
    private void ShowCoin()
    {
        coin.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    public void ExitPanel()
    {
        Destroy(gameObject, 0.1f);

    }
}
