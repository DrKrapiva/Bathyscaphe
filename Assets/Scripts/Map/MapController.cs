using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : DictionaryMap
{
    
    [SerializeField] private Transform content;
    [SerializeField] private GameObject prefabMapPanel;

    public static MapController Instance
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
    static private MapController _instance;

    public void CreateMapPanel()
    {
        GameObject mapPanel = Instantiate(prefabMapPanel, content, false);
    }
    public Map GetMapExample(string mapKey)
    {
        return DicMaps[mapKey];
    }
    public List<string> SpecialWeapon(string mapKey)
    {
        return DicMaps[mapKey].SpecialWeapon;
    }
    /*public TaskParam GetTask(string taskKey)
    {
        return TaskDictionary.Instance.DicTask[taskKey];
    }*/

}
