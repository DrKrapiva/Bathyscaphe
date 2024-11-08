using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementControllerMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelAchievement;
    [SerializeField] private Transform canvas;
    

    public void CreatePanel()
    {
        var panel = Instantiate(panelAchievement, canvas);
    }
    
}
