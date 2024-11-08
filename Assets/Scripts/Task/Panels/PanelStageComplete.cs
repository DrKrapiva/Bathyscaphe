using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelStageComplete : MonoBehaviour
{
    [SerializeField] private Transform contentTaskLast;
    [SerializeField] private Transform contentTaskBest;
    [SerializeField] private GameObject taskPanelPrefab;
    private void Start()
    {
        FillInfo();
    }
    private void FillInfo()
    {
        //заполн€ю панели task
        string missionName = TaskController.Instance.MissionName();
        Debug.Log("PanelPause " + missionName);

        string wordBefor = TaskController.Instance.GetTaskParamByKey(missionName).taskTextBeforMission;
        string wordAfter = TaskController.Instance.GetTaskParamByKey(missionName).taskTextAfterMission;
        List<int> amountItem = TaskController.Instance.GetTaskParamByKey(missionName).AmountItem;
        List<int> amountItemLast = SaveGame.Instance.LoadAmountItemLast(missionName);
        List<int> amountItemBest = SaveGame.Instance.LoadAmountItemBest(missionName);

        for (int i = 0; i < amountItem.Count; i++)
        {
            var slotTask = Instantiate(taskPanelPrefab, contentTaskLast, false);
            slotTask.GetComponent<PanelTask>().FillInfo(i + 1, amountItemLast[i], amountItem[i], wordBefor, wordAfter);
            Debug.Log("amountItemLast " + amountItemLast[i]);
        }
        for (int i = 0; i < amountItem.Count; i++)
        {
            var slotTask = Instantiate(taskPanelPrefab, contentTaskBest, false);
            slotTask.GetComponent<PanelTask>().FillInfo(i + 1, amountItemBest[i], amountItem[i], wordBefor, wordAfter);
        }
    }
    public void LoadMenu()
    {

        LevelController.Instance.LoadMenu();
    }
    public void ContinueGame()
    {
        LevelController.Instance.PauseOff();
        Destroy(gameObject, 0.1f);
    }
}
