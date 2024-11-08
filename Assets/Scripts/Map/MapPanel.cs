using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;
    [SerializeField] private Image mapImg;
    [SerializeField] private TextMeshProUGUI mapName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI specialWeapon;
    [SerializeField] private GameObject panelTaskPrefab;
    [SerializeField] private GameObject panelAdditionalTaskPrefab;
    [SerializeField] private GameObject panelWeaponSelection;
    [SerializeField] private Transform contentForPanel;
    [SerializeField] private Transform contentTaskLast;
    [SerializeField] private Transform contentTaskBest;
    private Map map;
    private List<string> _mapNames;
    private List<string> specialWeapons;
    private int index = 0;
    
    public static MapPanel Instance
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
    static private MapPanel _instance;
    private void Start()
    {
        _mapNames = MapController.Instance.DicMaps.Keys.ToList();
        
        ShowInfo(_mapNames[index]);

        CheckInterectableButtonLeft();
        CheckInterectableButtonRight();
        //CheckCanPlay();/////// оставить для проверки особого условия
    }
    private void ShowInfo(string mapKey)
    {
        map = MapController.Instance.GetMapExample(mapKey);

        specialWeapons = map.SpecialWeapon;
        if (specialWeapons != null && specialWeapons.Count > 0)
        {
            specialWeapon.text = string.Join(", ", specialWeapons);
            //CheckCanPlay();
        }
        else
        {
            specialWeapon.text = "Нет особого оружия";
        }

        mapName.text = map.Name;
        description.text = map.Description;
        mapImg.sprite = Resources.Load<Sprite>("MapsPic/" + mapKey);
        GeneratePanelsTaskLastAndBest();
       //GeneratePanelMainTask();
       // GeneratePanelAdditionalTask();
    }
    public List<string> SpecialWeapon()
    {
        return MapController.Instance.SpecialWeapon(_mapNames[index]);
    }
    private void GeneratePanelsTaskLastAndBest()
    {
        foreach (Transform child in contentTaskLast)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in contentTaskBest)
        {
            Destroy(child.gameObject);
        }
        string missionName = MapController.Instance.DicMaps[_mapNames[index]].MissionName;

        string wordBefor = TaskController.Instance.GetTaskParamByKey(missionName).taskTextBeforMission;
        string wordAfter = TaskController.Instance.GetTaskParamByKey(missionName).taskTextAfterMission;
        List<int> amountItem = TaskController.Instance.GetTaskParamByKey(missionName).AmountItem;
        List<int> amountItemLast = SaveGame.Instance.LoadAmountItemLast(missionName);
        List<int> amountItemBest = SaveGame.Instance.LoadAmountItemBest(missionName);

        for (int i = 0; i < amountItem.Count; i++)
        {
            var slotTask = Instantiate(panelTaskPrefab, contentTaskLast, false);
            slotTask.GetComponent<PanelTask>().FillInfo(i + 1, amountItemLast[i], amountItem[i], wordBefor, wordAfter);
        }
        for (int i = 0; i < amountItem.Count; i++)
        {
            var slotTask = Instantiate(panelTaskPrefab, contentTaskBest, false);
            slotTask.GetComponent<PanelTask>().FillInfo(i + 1, amountItemBest[i], amountItem[i], wordBefor, wordAfter);
        }

    }
    public int SceneNumber()
    {
        string missionName = MapController.Instance.DicMaps[_mapNames[index]].MissionName;
        return TaskController.Instance.GetTaskParamByKey(missionName).SceneNumber;
    }

    private void CheckInterectableButtonLeft()
    {
        if (index == 0)
        {
            buttonLeft.interactable = false;
        }
        else buttonLeft.interactable = true;
    }
    private void CheckInterectableButtonRight()
    {
        if (index == _mapNames.Count - 1)
        {
            buttonRight.interactable = false;
        }
        else buttonRight.interactable = true;
    }
    public void SwithLeft()
    {
        index--;
        ShowInfo(_mapNames[index]);
        CheckInterectableButtonLeft();
        CheckInterectableButtonRight();
    }
    public void SwithRight()
    {
        index++;
        ShowInfo(_mapNames[index]);
        CheckInterectableButtonLeft();
        CheckInterectableButtonRight();
    }
    /*public void CheckCanPlay(bool canPlay, string mapName)/////// оставить для проверки особого условия
    {
        buttonPlay.interactable = canPlay;
        
    }*/
    
    public void Play()
    {
        //Debug.Log(_mapNames[index]);
        //SceneManager.LoadScene(_mapName); вызов панель выбор оружия
        //SceneManager.LoadScene("Level");
        GameObject panel = Instantiate(panelWeaponSelection, contentForPanel, false);
    }
    public void Exit()
    {
        Destroy(gameObject, 0.1f);
    }
}
