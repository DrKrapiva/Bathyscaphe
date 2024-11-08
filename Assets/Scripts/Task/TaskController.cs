using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

public class TaskController : TaskDictionary
{
    [SerializeField] private GameObject panelTaskFailed;
    [SerializeField] private GameObject panelStageComplete;
    [SerializeField] private GameObject panelTrialPassed;
    [SerializeField] private Transform canvas;
    private int sceneNumber;
    private string missionName = "NoMission";
    // Событие для обновления прогресса
    public event Action OnTaskProgressUpdated;
    
    public static TaskController Instance
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
    static private TaskController _instance;
    

    
    public void GetMissionName(string missionNamee)
    {
        missionName = missionNamee;
        Debug.Log("TaskController " + missionName);
    }
    public string MissionName()
    {
        return missionName;
    }
    // Метод для вызова события, чтобы миссии могли передать прогресс и сохранить
    public void TriggerTaskProgressUpdate()
    {
        OnTaskProgressUpdated?.Invoke();
        Debug.Log("TriggerTaskProgressUpdate TaskController");
    }
    public void CreatePanelTaskFailed()
    {
        GameObject panelClon = Instantiate(panelTaskFailed, canvas);
        Time.timeScale = 0f;
    }
    public void CreatePanelStageComplete()
    {
        GameObject panelClon = Instantiate(panelStageComplete, canvas);
        Time.timeScale = 0f;
    }
    public void CreatePanelTrialPassed()
    {
        GameObject panelClon = Instantiate(panelTrialPassed, canvas);
        Time.timeScale = 0f;
    }
    public void SetSceneNumber(int scene)
    {
        sceneNumber = scene;
    }
    public int GetSceneNumber()
    {
        return sceneNumber;
    }
    public void SaveTaskResults(string taskName, int currentStage, List<int> newResults)
    {
        TaskProgressDictionary.Instance.UpdateTaskProgress(taskName, currentStage, newResults);
        Debug.Log("SaveTaskResults TaskController");
    }
}
