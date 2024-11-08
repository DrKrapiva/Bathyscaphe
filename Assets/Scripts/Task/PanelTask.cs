using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelTask : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageNumber;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private TextMeshProUGUI wordText;
    private void Start()
    {
        
    }
    public void FillInfo(int stage, int result, int amontFromDictionary, string wordBefor, string wordAfter)
    {
        stageNumber.text = stage.ToString();
        resultsText.text = $"{result} / {amontFromDictionary}";
        wordText.text = wordBefor;
        if(result == amontFromDictionary)
        {
            gameObject.GetComponent<Image>().color = Color.green;
            wordText.text = wordAfter;
        }
    }
    /*private void FillInfo()
    {
        //textTask.text = TaskController.Instance.GetTaskTextByKey(gameObject.name);
        textTask.text = TaskController.Instance.GetTaskTextByKey("MissionRescue");

        int results = TaskController.Instance.GetResultsByKey("MissionRescue");
        //int results = TaskController.Instance.GetResultsByKey(gameObject.name);
        //int amount = TaskController.Instance.GetAmountByKey(gameObject.name);
        int amount = TaskController.Instance.GetAmountByKey("MissionRescue");
        resultsText.text = $"{results} / {amount}";

        //if (TaskController.Instance.IsTaskCompleted(gameObject.name))
        if (TaskController.Instance.IsTaskCompleted("MissionRescue"))
        {
            imageDone.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/CheckMark");
        }
    }*/
}
