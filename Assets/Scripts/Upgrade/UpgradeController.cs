using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private GameObject panelUpgrade;
    [SerializeField] private Transform content;

    public void GenerationUpgradePanel()
    {
        GameObject slot = Instantiate(panelUpgrade, content, false);
    }


}
