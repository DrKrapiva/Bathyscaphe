using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AcheivementSlotShow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI achivName;
    [SerializeField] private TextMeshProUGUI achivDescription;
    [SerializeField] private TextMeshProUGUI _coin;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI textProgress;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private Button getReward;
    private Dictionary<string, Achievement> _achievementsDict;
    private string _key;
    private string _keySuffix;
    private int _levelReward;
    private int _maxLevel; 
    private int _youHavePoints;
    private int coins;
    
    public void FillInfo(Dictionary<string, Achievement> achievementsDict, string key, string keySuffix, int maxLevel)
    {
        _achievementsDict = achievementsDict;
        _key = key; 
        _keySuffix = keySuffix; 
        _maxLevel = maxLevel;
        _youHavePoints = DictionaryAchievementProgress.Instance.GetAchievementsProgress(_achievementsDict[_key].Parameter, keySuffix);
        //Debug.Log(_achievementsDict[_key].Name);
        //Debug.Log(_youHavePoints+ " _youHavePoints");
        string iconName = $"{key}{keySuffix}";
        icon.sprite = Resources.Load<Sprite>($"Prefab/AchievementPic/{iconName}");

        achivName.text = _achievementsDict[_key].Name;
        achivDescription.text = _achievementsDict[_key].Description;

        CheckParamToShow();
    }
    private void CheckParamToShow()
    {
        _levelReward = DictionaryAchievementRewards.Instance.GetAchievementsRewards(_achievementsDict[_key].Parameter, _keySuffix == "Sum");

        bool isSum = _keySuffix == "Sum";
        int rewardLevel = _levelReward;  // Предполагаемый уровень для расчета

        // Определяем, какие значения использовать для уровня и прогресса
        int currentLevel = (_achievementsDict[_key].Level > _levelReward) ? _levelReward : _achievementsDict[_key].Level;

        // Проверка достижения максимального уровня
        if (currentLevel == _maxLevel)
        {
            rewardLevel = _levelReward - 1; // Используем уровень на единицу меньше для расчета награды
        }

        // Расчёт монет и максимального значения на основании актуального уровня награды
        coins = AchievementController.Instance.CoinsForAchievement(_key, isSum, rewardLevel);
        int _maxValue = AchievementController.Instance.ValueForCheck(_key, isSum, rewardLevel);

        // Обновляем UI элементы
        _level.text = $" {currentLevel} / {_maxLevel}";
        _coin.text = coins.ToString();
        slider.maxValue = _maxValue;

        int currentProgress = (_achievementsDict[_key].Level > _levelReward) ? _maxValue : _youHavePoints;

        if (currentLevel == _maxLevel)
        {
            slider.value = _maxValue;
            //getReward.interactable = false;
            getReward.gameObject.SetActive(false);
            _coin.text = "";
            coinImage.SetActive(false);
        }
        else
        {
            slider.value = currentProgress;
            getReward.interactable = _achievementsDict[_key].Level > _levelReward;
        }

        textProgress.text = $" {slider.value} / {_maxValue}";
        
    }
    public void GetRewards()
    {
        DictionaryAchievementRewards.Instance.ChangeLevelAchievementRewards(_achievementsDict[_key].Parameter, _keySuffix == "Sum");
        GameController.Instance.ChangeCoin(coins);
        CheckParamToShow();
    }
}
