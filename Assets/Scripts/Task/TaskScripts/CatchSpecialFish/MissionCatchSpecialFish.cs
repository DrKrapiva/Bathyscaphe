using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionCatchSpecialFish : MonoBehaviour
{
    private float startTime = 40f; // ����� � �������� (��������, 5 �����)
    private float timeRemaining;
    private bool timerIsRunning = false;
    private TextMeshProUGUI timeText;
    private int countdownTime = 3; // ����� ��� ��������� ������� ����� �������

    private int caughtFish = 0;
    private int amountFish = 3;
    private int spawnRadius = 60;
    [SerializeField] private GameObject prefabFish;
    public static MissionCatchSpecialFish Instance
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
    static private MissionCatchSpecialFish _instance;
    void Start()
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
        StartCoroutine(CountdownToStart());
        CreateFish();
    }
    private void CreateFish()
    {
        for (int i = 0; i < amountFish; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
            randomPosition.y = 1; // ������������� ������ �� ������ �����
            GameObject fish = Instantiate(prefabFish, randomPosition, Quaternion.identity);

            Sprite arrowSprite = Resources.Load<Sprite>("UI/Arrows/pickupItem");
            ArrowPointer.Instance.StartArrowCoroutine(fish, arrowSprite);
        }
    }
    public void FishCounter()
    {
        caughtFish++;

        CheackWin();
    }
    private void CheackWin()
    {
        if(caughtFish >= amountFish && timeRemaining > 0)
        {
            timeText.text = "������!";
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

        //CreateMines();
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
