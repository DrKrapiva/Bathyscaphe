using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionEscort : MonoBehaviour
{
    private GameObject escortItem;
    private GameObject targetPoint;
    private GameObject player;
    private float spawnRadiusForTarget = 100;
    private float spawnRadiusForEscortItem = 3;
    private int countdownTime = 3; // Время для обратного отсчета перед стартом
    private TextMeshProUGUI timeText;
    private void Start()
    {
        player = GameObject.Find("Player");
        GameObject timerObject = GameObject.Find("Timer");
        if (timerObject != null)
        {
            timeText = timerObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Timer GameObject not found");
        }

        escortItem = Resources.Load<GameObject>("Prefab/Task/EscortItem");
        targetPoint = Resources.Load<GameObject>("Prefab/Task/TargetPointEscort");

        //CreateEscortItemAndTarget();
        StartCoroutine(CountdownToStart());
    }

    private void CreateEscortItemAndTarget()
    {
        Vector3 spawnPositionForEscortItem = player.transform.position + Random.onUnitSphere * spawnRadiusForEscortItem;
        //spawnPositionForEscortItem.y = player.transform.position.y;
        spawnPositionForEscortItem.y = 0;
        GameObject pickupEscortItem = Instantiate(escortItem, spawnPositionForEscortItem, Quaternion.identity);

        Sprite arrowSpriteItem = Resources.Load<Sprite>("UI/Arrows/pickupItem");
        ArrowPointer.Instance.StartArrowCoroutine(pickupEscortItem, arrowSpriteItem);

        //Create Target

        Vector3 spawnPosition = player.transform.position + Random.onUnitSphere * spawnRadiusForTarget;
        //spawnPosition.y = player.transform.position.y;
        spawnPosition.y = 0;
        GameObject targetPointClon = Instantiate(targetPoint, spawnPosition, Quaternion.identity);

        Sprite arrowSpriteTarget = Resources.Load<Sprite>("UI/Arrows/targetPoint");
        ArrowPointer.Instance.StartArrowCoroutine(targetPointClon, arrowSpriteTarget);

        //отдаем цель
        pickupEscortItem.GetComponent<EscortItem>().FillInfo(targetPointClon);
    }

    private IEnumerator CountdownToStart()
    {
        for (int i = countdownTime; i > 0; i--)
        {
            timeText.text = "Старт через " + i;
            yield return new WaitForSeconds(1f);
        }

        timeText.text = "Поехали!";
        yield return new WaitForSeconds(1f);
        timeText.text = "";
        CreateEscortItemAndTarget();
    }
}
