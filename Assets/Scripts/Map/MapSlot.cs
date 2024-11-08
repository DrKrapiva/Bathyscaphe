using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSlot : MonoBehaviour
{
    [SerializeField] private Image mapImg;
    [SerializeField] private TextMeshProUGUI mapName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private GameObject panelTaskPrefab;
    [SerializeField] private GameObject panelAdditionalTaskPrefab;

    [SerializeField] private Transform contentMainTask;
    [SerializeField] private Transform contentAddittionalTask;
    [SerializeField] private Button buttonChoice;
    private bool isToggled = false;
    private Map map;
    
    private void Start()
    {
        ShowInfo();
        //CheckInitialToggleState();
        //buttonChoice.onClick.AddListener(ToggleAndChooseMap);
    }
    
    private void ShowInfo()
    {
        //map = MapController.Instance.GetMapExample(gameObject.name);
        
        mapName.text = map.Name;
        description.text = map.Description;
        mapImg.sprite = Resources.Load<Sprite>("MapsPic/" + gameObject.name);
        //GeneratePanelMainTask();
        GeneratePanelAdditionalTask();
    }
    /*private void GeneratePanelMainTask()
    {
        List<string> taskNames = MapController.Instance.DicMaps[gameObject.name].MissionName;
        foreach (var task in taskNames)
        {
            GameObject panelTask = Instantiate(panelTaskPrefab, contentMainTask, false);
            panelTask.name = task;
        }
    }*/
    private void GeneratePanelAdditionalTask()
    {
        List<string> addTaskNames = MapController.Instance.DicMaps[gameObject.name].AdditionalTask;
        foreach(var addTask in addTaskNames)
        {
            GameObject panelAddTask = Instantiate(panelAdditionalTaskPrefab, contentAddittionalTask, false);
            panelAddTask.name = addTask;
        }
    }

    /*private void CheckInitialToggleState()
    {
        // Устанавливаем isToggled в зависимости от текущего выбранного состояния в MapPanel
        isToggled = MapPanel.Instance.ChoosenMapName() == gameObject.name;
        UpdateButtonSprite();
    }

    private void ToggleAndChooseMap()
    {
        // Переключаем isToggled на противоположное значение
        isToggled = !isToggled;
        UpdateButtonSprite();

        // Отправляем информацию о текущем состоянии в MapPanel
        MapPanel.Instance.CheckCanPlay(isToggled, isToggled ? gameObject.name : "UnselectedMap");
    }

    private void UpdateButtonSprite()
    {
        // Обновляем спрайт кнопки в зависимости от состояния isToggled
        buttonChoice.image.sprite = isToggled ? buttonChoice.spriteState.pressedSprite : buttonChoice.spriteState.disabledSprite;
    }*/

}
