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
        // �������� ���������� ��������� �� ����������
        //DictionaryAchievementProgress.Instance.DictionarySum();
        // ��������� ������ ��� ��������� ����������
        GenerateAchievementSlot(AchievementController.Instance.DicAchievementsSum, "Sum");
        // �������� ��������� �������� ������ �� ���������� (���� ����������)
        //DictionaryAchievementProgress.Instance.DictionaryOnLevel();
        // ��������� ������ ��� ���������� �� ������
        GenerateAchievementSlot(AchievementController.Instance.DicAchievementsMaxOnLevel, "MaxOnLevel");
    }

    private void GenerateAchievementSlot(Dictionary<string, Achievement> achievementsDict, string keySuffix)
    {
        foreach (var achievement in achievementsDict)
        {
            
                var slotInstance = Instantiate(prefabAchievementSlot, content);
                var slotScript = slotInstance.GetComponent<AcheivementSlotShow>();
                //string iconName = achievement.Key + keySuffix; // ������������ ����� ��� ������

                // �������� ������� �������� (youHavePoints) ��� ������� ����������
                //int youHavePoints = DictionaryAchievementProgress.Instance.GetAchievementsProgress(achievement.Value.Parameter, "OnLevel");

                // ���������� ������� ��� ����������� ����� � ����������� �����
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
