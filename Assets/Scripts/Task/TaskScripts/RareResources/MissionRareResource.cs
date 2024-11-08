using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionRareResource : MonoBehaviour
{
    private GameObject pickupItem;
    private GameObject player;
    private float spawnRadiusMax = 60;
    private float spawnRadiusMin = 40;

    private float startTime = 20f; // ����� � �������� (��������, 5 �����)
    private float timeRemaining;
    private bool timerIsRunning = false;
    private TextMeshProUGUI timeText;
    private int countdownTime = 3; // ����� ��� ��������� ������� ����� �������

    private int amountPickupItem = 5;
    private int amountCollectedItem;
    public static MissionRareResource Instance
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
    static private MissionRareResource _instance;
    void Start()
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

        pickupItem = Resources.Load<GameObject>("Prefab/Task/RareResource");

        StartCoroutine(CountdownToStart());
        CreatePickupItemsAndArrow();
    }

    private void CreatePickupItemsAndArrow()
    {
        float angleStep = 360f / amountPickupItem; // ��� ���� ����� ����������
        float currentAngle = 0f; // ��������� ����

        for (int i = 0; i < amountPickupItem; i++)
        {
            // ������������ ��������� ���������� � �������� spawnRadiusMin � spawnRadiusMax
            float distance = Random.Range(spawnRadiusMin, spawnRadiusMax);

            // ���������� ������� �������� �� ���������� ������ ������
            float xPosition = player.transform.position.x + distance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float zPosition = player.transform.position.z + distance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            Vector3 spawnPosition = new Vector3(xPosition, player.transform.position.y, zPosition);

            // ������ �������
            GameObject pickupItemClon = Instantiate(pickupItem, spawnPosition, Quaternion.identity);

            // ��������� ������� � ��������
            Sprite arrowSprite = Resources.Load<Sprite>("UI/Arrows/pickupItem");
            ArrowPointer.Instance.StartArrowCoroutine(pickupItemClon, arrowSprite);

            // ����������� ������� ����
            currentAngle += angleStep;
        }
    }
    public void ItemCounter()
    {
        amountCollectedItem++;
        CheckWin();
    }
    private void CheckWin()
    {
        if(amountCollectedItem == amountPickupItem)
        {
            Debug.Log("WIN");
            timeText.text = "WIN";
        }
    }
    private IEnumerator CountdownToStart()
    {
        for (int i = countdownTime; i > 0; i--)
        {
            timeText.text = "����� ����� " + i;
            yield return new WaitForSeconds(1f);
        }

        timeText.text = "�������!";
        yield return new WaitForSeconds(1f);

        timeRemaining = startTime;
        timerIsRunning = true;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (timerIsRunning && timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
            DisplayTime(timeRemaining);
        }

        if (timeRemaining <= 0)
        {
            Debug.Log("����� �����!");
            timerIsRunning = false;
            // �������������� ������ ���������� ������ ��� ������
            timeText.color = Color.red;
            timeText.text = "���������!";
            //���� ����
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(0, timeToDisplay);

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // �������� ���� ������ �� �������, ���� �������� 5 ������ ��� ������
        if (timeToDisplay <= 5)
        {
            timeText.color = Color.red;
        }
        else
        {
            timeText.color = Color.white; // ������� ����� ���� ������
        }

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
