using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionDestroySpecificEnemy : MonoBehaviour
{
    private string _enemyName = "ordinaryFish";
    private int amountEnemy = 2;
    private int amountEnemyCount =0;
    private TextMeshProUGUI timeText;
    void OnEnable()
    {
        EnemyActions.OnEnemyDeathWithName += EnemyCounter;
    }

    void OnDisable()
    {
        EnemyActions.OnEnemyDeathWithName -= EnemyCounter;
    }
    private void Start()
    {
        GameObject timerObject = GameObject.Find("Timer");
        if (timerObject != null)
        {
            timeText = timerObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Timer GameObject not found");
        }
    }
    void EnemyCounter(string enemyName)
    {
        Debug.Log("Enemy died: " + enemyName);
        // Additional logic to handle enemy death

        if(_enemyName == enemyName)
        {
            amountEnemyCount++;
            CheckWin();
        }
    }
    private void CheckWin()
    {
        if(amountEnemyCount >= amountEnemy)
        {
            Debug.Log("WIN");
            timeText.color = Color.red;
            timeText.text = "WIN";
        }
    }
}
