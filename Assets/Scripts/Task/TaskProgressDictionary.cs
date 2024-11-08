using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskProgressParam
{
    public int stageNumber;
    public List<int> amountItemLast = new List<int>(5);
    public List<int> amountItemBest = new List<int>(5);
}
public class TaskProgressDictionary : MonoBehaviour
{
    public static TaskProgressDictionary Instance
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
    private void Start()
    {
        LoadAllTaskProgress();
    }
    static private TaskProgressDictionary _instance;
    public static Dictionary<string, TaskProgressParam> DictTask()
    {
        Dictionary<string, TaskProgressParam> dict = new Dictionary<string, TaskProgressParam>();
        dict.Add("MissionRescue", new TaskProgressParam() { stageNumber = 0, amountItemBest = new List<int> { 1, 0, 0, 0, 0 }, amountItemLast = new List<int> { 0, 0, 0, 0, 0 } });
        dict.Add("NoMission", new TaskProgressParam() { stageNumber = 0, amountItemBest = new List<int> { 1, 0, 0, 0, 0 }, amountItemLast = new List<int> { 0, 0, 0, 0, 0 } });
        
        return dict;
    }
    public Dictionary<string, TaskProgressParam> DicTask = DictTask();

    public int NumberKeysFromDictionary()
    {
        return DicTask.Keys.Count;
    }

    public void UpdateTaskProgress(string taskName, int currentStage, List<int> newResults)
    {
        if (DicTask.TryGetValue(taskName, out TaskProgressParam taskProgress))
        {
            // Обновляем stageNumber
            taskProgress.stageNumber = currentStage;
            SaveGame.Instance.SaveStageNumber(taskName, taskProgress.stageNumber); // Сохраняем stageNumber

            // Записываем новые результаты в amountItemLast
            taskProgress.amountItemLast = new List<int>(newResults);
            SaveGame.Instance.SaveAmountItemLast(taskName, taskProgress.amountItemLast);// Сохраняем amountItemLast

            // Сравниваем и обновляем amountItemBest
            bool isBestUpdated = false;
            for (int i = 0; i < taskProgress.amountItemLast.Count; i++)
            {
                if (taskProgress.amountItemLast[i] > taskProgress.amountItemBest[i])
                {
                    taskProgress.amountItemBest[i] = taskProgress.amountItemLast[i];
                    isBestUpdated = true; // Отмечаем, что обновили amountItemBest
                }
            }
            if (isBestUpdated)
            {
                SaveGame.Instance.SaveAmountItemBest(taskName, taskProgress.amountItemBest); // Сохраняем amountItemBest, если он был обновлен
            }
        }
        else
        {
            Debug.LogError($"Задача с именем {taskName} не найдена в словаре.");
        }
    }
    private void LoadAllTaskProgress()
    {
        // Проходим по всем ключам в словаре DicTask
        foreach (var task in DicTask)
        {
            string taskName = task.Key;

            // Загружаем данные для каждого ключа и обновляем TaskProgressParam
            task.Value.stageNumber = SaveGame.Instance.LoadStageNumber(taskName);
            task.Value.amountItemBest = SaveGame.Instance.LoadAmountItemBest(taskName);
            task.Value.amountItemLast = SaveGame.Instance.LoadAmountItemLast(taskName);
            // Выводим загруженные данные для проверки
            Debug.Log($"Task: {taskName}, stageNumber: {task.Value.stageNumber}, " +
                      $"amountItemBest: {string.Join(", ", task.Value.amountItemBest)}, " +
                      $"amountItemLast: {string.Join(", ", task.Value.amountItemLast)}");

        }
    }
    
}
