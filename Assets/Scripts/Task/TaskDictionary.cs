using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskParam
{
    public string taskTextBeforMission;
    public string taskTextAfterMission;
    public int Amount;//сколько надо , не меняется
    public int Results;//сколько насобирал, менять во время прохождения уровня

    public List<int> AmountItem = new List<int>(5);
    public List<string> EnemyNames;
    public int SceneNumber;
    public float TimeForTask;
    public float DistanceToTarget;
    public float HpBoat;
    public float EnemyAttack;
    public float SpawnRadius;
    public float MineDefuseTime;
    

}
public class TaskDictionary : MonoBehaviour
{
    
    public static Dictionary<string, TaskParam> DictTask()
    {
        Dictionary<string, TaskParam> dict = new Dictionary<string, TaskParam>();
        dict.Add("MissionRescue", new TaskParam() { taskTextBeforMission = "спасти",taskTextAfterMission = "спасено", AmountItem = new List<int> { 1, 1,1,1,1 }, SceneNumber = 1, TimeForTask = 25, SpawnRadius = 60, });
        dict.Add("NoMission", new TaskParam() { taskTextBeforMission = "", SceneNumber = 2 });
        dict.Add("task3", new TaskParam() { taskTextBeforMission = "Never gonna run around and desert you", Amount = 1, Results = 0 });
        dict.Add("task4", new TaskParam() { taskTextBeforMission = "Never gonna make you cry", Amount = 1, Results = 0 });
        dict.Add("task5", new TaskParam() { taskTextBeforMission = "Never gonna say goodbye", Amount = 1 , Results = 0 });
        dict.Add("task6", new TaskParam() { taskTextBeforMission = "Never gonna tell a lie and hurt you", Amount = 1, Results = 0 });
        return dict;
    }
    public Dictionary<string, TaskParam> DicTask = DictTask();

    
    public TaskParam GetTaskParamByKey(string key)
    {
        if (DicTask.ContainsKey(key))
        {
            return DicTask[key];
        }
        else
        {
            Debug.LogError($"Task with key {key} not found.");
            return null;
        }
    }

    //убрать
    public string GetTaskTextByKey(string key)
    {
        if (DicTask.ContainsKey(key))
        {
            return DicTask[key].taskTextBeforMission;
        }
        else
        {
            return "";
        }
    }
    public int GetAmountByKey(string key)
    {
        if (DicTask.ContainsKey(key))
        {
            return DicTask[key].Amount;
        }
        else return 0;
    }
    public int GetResultsByKey(string key)
    {
        if (DicTask.ContainsKey(key))
        {
            return DicTask[key].Results;
        }
        else return 0;
    }

    public bool IsTaskCompleted(string key)
    {
        if (DicTask[key].Results >= DicTask[key].Amount)
        {
            return true;
        }
        else return false;
    }
}
