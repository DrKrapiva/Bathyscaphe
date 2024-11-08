using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelPause : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Transform contentPassiveWeapon;
    [SerializeField] private GameObject passiveWeaponSlotPrefab;
    [SerializeField] private Transform contentTaskLast;
    [SerializeField] private Transform contentTaskBest;
    [SerializeField] private GameObject taskPanelPrefab;

    public static event Action OnLeaveLevel;
    public void FillInfo(int coin, int time, List<string> passiveWeaponNames)
    {
        coinText.text = coin.ToString();
        timeText.text = time.ToString();

        foreach(string weaponName in passiveWeaponNames)
        {
            var weponSlot = Instantiate(passiveWeaponSlotPrefab, contentPassiveWeapon, false);
            weponSlot.name = weaponName;

            // Убедимся, что имя присвоено перед вызовом FillInfo
            var passiveWeponSlot = weponSlot.GetComponent<PassiveWeaponSlot>();
            if (passiveWeponSlot != null)
            {
                passiveWeponSlot.SetWeaponName(weaponName); // Передаем имя
                passiveWeponSlot.FillInfo(); // Заполняем информацию после установки имени
            }
        }
        //заполняю панели task
        string missionName = TaskController.Instance.MissionName();
        Debug.Log("PanelPause " + missionName);

        string wordBefor = TaskController.Instance.GetTaskParamByKey(missionName).taskTextBeforMission;
        string wordAfter = TaskController.Instance.GetTaskParamByKey(missionName).taskTextAfterMission;
        List<int> amountItem = TaskController.Instance.GetTaskParamByKey(missionName).AmountItem;
        List<int> amountItemLast = SaveGame.Instance.LoadAmountItemLast(missionName);
        List<int> amountItemBest = SaveGame.Instance.LoadAmountItemBest(missionName);

        for(int i = 0; i < amountItem.Count; i++)
        {
            var slotTask = Instantiate(taskPanelPrefab, contentTaskLast, false);
            slotTask.GetComponent<PanelTask>().FillInfo(i + 1, amountItemLast[i], amountItem[i], wordBefor, wordAfter);
            Debug.Log("amountItemLast " + amountItemLast[i]);
        }
        for(int i = 0; i < amountItem.Count; i++)
        {
            var slotTask = Instantiate(taskPanelPrefab, contentTaskBest, false);
            slotTask.GetComponent<PanelTask>().FillInfo(i + 1, amountItemBest[i], amountItem[i], wordBefor, wordAfter);
        }

    }
    
    public void LoadMenu()
    {
        LevelController.Instance.LeaveLevel();
        OnLeaveLevel?.Invoke();
        LevelController.Instance.LoadMenu();
    }
    public void ReturnToGame()
    {
        LevelController.Instance.PauseOff();
        Destroy(gameObject, 0.1f);
    }
}
