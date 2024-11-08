using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AdditionalTaskParam
{
    public string taskText;
    public int Amount;//сколько надо , не меняется
    public int Results;//сколько насобирал, менять во время прохождения уровня

}
public class AdditionalTaskDictionary : MonoBehaviour
{
    public static AdditionalTaskDictionary Instance
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
    static private AdditionalTaskDictionary _instance;

    public static Dictionary<string, AdditionalTaskParam> DictAdditionalTask()
    {
        Dictionary<string, AdditionalTaskParam> dict = new Dictionary<string, AdditionalTaskParam>();
        dict.Add("task1", new AdditionalTaskParam() { taskText = "Never gonna give you up", Amount = 1, Results = 1 });
        dict.Add("task2", new AdditionalTaskParam() { taskText = "Never gonna let you down", Amount = 1, Results = 0 });
        dict.Add("task3", new AdditionalTaskParam() { taskText = "Never gonna run around and desert you", Amount = 1, Results = 0 });
        dict.Add("task4", new AdditionalTaskParam() { taskText = "Never gonna make you cry", Amount = 1, Results = 0 });
        dict.Add("task5", new AdditionalTaskParam() { taskText = "Never gonna say goodbye", Amount = 1, Results = 0 });
        dict.Add("task6", new AdditionalTaskParam() { taskText = "Never gonna tell a lie and hurt you", Amount = 1, Results = 0 });
        return dict;
    }
    public Dictionary<string, AdditionalTaskParam> DicAdditionalTask = DictAdditionalTask();

    public string GetTaskTextByKey(string key)
    {
        if (DicAdditionalTask.ContainsKey(key))
        {
            return DicAdditionalTask[key].taskText;
        }
        else
        {
            return "";
        }
    }
    public int GetAmountByKey(string key)
    {
        if (DicAdditionalTask.ContainsKey(key))
        {
            return DicAdditionalTask[key].Amount;
        }
        else return 0;
    }
    public int GetResultsByKey(string key)
    {
        if (DicAdditionalTask.ContainsKey(key))
        {
            return DicAdditionalTask[key].Results;
        }
        else return 0;
    }

    public bool IsTaskCompleted(string key)
    {
        if (DicAdditionalTask[key].Results >= DicAdditionalTask[key].Amount)
        {
            return true;
        }
        else return false;
    }
}
