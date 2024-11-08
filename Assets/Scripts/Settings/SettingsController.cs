using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private Transform content;

    public void Settings()
    {
        panelSettings.SetActive(true);
    }
   
    public void ExitPanel()
    {
        
        panelSettings.SetActive(false);
    }
}
