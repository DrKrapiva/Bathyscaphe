using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelAdditionalTask : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTask;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private Image imageDone;
    private void Start()
    {
        FillInfo();
    }
    private void FillInfo()
    {
        textTask.text = AdditionalTaskDictionary.Instance.GetTaskTextByKey(gameObject.name);

        int results = AdditionalTaskDictionary.Instance.GetResultsByKey(gameObject.name);
        int amount = AdditionalTaskDictionary.Instance.GetAmountByKey(gameObject.name);
        resultsText.text = $"{results} / {amount}";

        if (AdditionalTaskDictionary.Instance.IsTaskCompleted(gameObject.name))
        {
            imageDone.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/CheckMark");
        }
    }
}
