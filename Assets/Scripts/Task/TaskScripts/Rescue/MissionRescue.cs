using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class MissionRescue : MonoBehaviour
{
    private GameObject pickupItem;
    private GameObject targetPoint;
    private GameObject player;
    private string keyName = "MissionRescue";
    private float spawnRadius;
    private int sceneNumber;
    private int stage = 1;
    private int maxStage = 5;
    private List<int> results = new List<int>() { 0,0,0,0,0};

    private float startTime ; // ����� � �������� (��������, 5 �����)
    private float timeRemaining;
    private bool timerIsRunning = false;
    private TextMeshProUGUI timeText;
    private int countdownTime = 3; // ����� ��� ��������� ������� ����� �������

    private Coroutine timerCoroutine;

    private Vector3 initialPlayerPosition;
    private float distanceToBoundary = 200f; // ���������� �� ������� �� ���������� ��������� ������

    private TaskParam taskParam; // ��������� ������ TaskParam ��� �������� ���������� ������

    public static MissionRescue Instance
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
    static private MissionRescue _instance;
    private void Start()
    {
        //�������� ���, ����� ������� �������� ���������� ������ ��� �������� ���������� TaskController
        if (TaskController.Instance != null)
        {
            TaskController.Instance.OnTaskProgressUpdated += HandleTaskProgressUpdate;
            Debug.Log("Successfully subscribed to OnTaskProgressUpdated in Start");
        }
        else
        {
            Debug.LogError("TaskController.Instance is still null in MissionRescue Start");
        }
        //Debug.Log($"results Last: {string.Join(", ", results)}");
        //Debug.Log(gameObject.name);
        TaskController.Instance.GetMissionName(keyName);

        taskParam = TaskController.Instance.GetTaskParamByKey(keyName);

        if (taskParam != null)
        {
            sceneNumber = taskParam.SceneNumber;
            spawnRadius = taskParam.SpawnRadius;
            startTime = taskParam.TimeForTask;
        }
        else
        {
            Debug.LogError($"Task parameters for {keyName} not found.");
        }

        // ������������� ������� ������
        player = GameObject.Find("Player");
        /*if (player != null)
        {
            initialPlayerPosition = player.transform.position; // ���������� ��������� ������� ������
            Debug.Log($"Initial Player Position set to: {initialPlayerPosition}");
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }*/

        GameObject timerObject = GameObject.Find("Timer");
        if (timerObject != null)
        {
            timeText = timerObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Timer GameObject not found");
        }

        pickupItem = Resources.Load<GameObject>("Prefab/Task/PickupItem");
        targetPoint = Resources.Load<GameObject>("Prefab/Task/TargetPoint");
        CreatePickupItemAndArrow();

        StartCoroutine(CountdownToStart());
    }
    

    private void OnDisable()
    {
        Debug.Log("MissionRescue OnDisable");
        // ������������ �� �������, ����� �������� ������ ������
        if (TaskController.Instance != null)
        {
            TaskController.Instance.OnTaskProgressUpdated -= HandleTaskProgressUpdate;
            Debug.Log("MissionRescue OnDisable �������");
        }
    }
    private void HandleTaskProgressUpdate()
    {
        // ����� ������� �����������, �������� �������� � TaskProgressDictionary
        TaskProgressDictionary.Instance.UpdateTaskProgress(keyName, stage, results);
        Debug.Log("UpdateTaskProgress MissionRescue");
    }
    private void CreatePickupItemAndArrow()
    {
        Vector3 spawnPosition = CalculateValidSpawnPosition();
        GameObject pickupItemClon = Instantiate(pickupItem, spawnPosition, Quaternion.identity);

        Sprite arrowSprite = Resources.Load<Sprite>("UI/Arrows/pickupItem");
        ArrowPointer.Instance.StartArrowCoroutine(pickupItemClon, arrowSprite);
    }

    public void CreateTargetPointAndArrow()
    {
        Vector3 spawnPosition = CalculateValidSpawnPosition();
        GameObject targetPointClon = Instantiate(targetPoint, spawnPosition, Quaternion.identity);

        Sprite arrowSprite = Resources.Load<Sprite>("UI/Arrows/targetPoint");
        ArrowPointer.Instance.StartArrowCoroutine(targetPointClon, arrowSprite);
    }

    private Vector3 CalculateValidSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero; // ������������� ����������

        bool isValidPosition = false;

        while (!isValidPosition)
        {
            Vector3 direction = Random.onUnitSphere;
            direction.y = 0; // ������� �� ��� �� ������
            //direction.y = player.transform.position.y; // ������� �� ��� �� ������
           // Debug.Log("spawnRadius * stage " + spawnRadius * stage);
            spawnPosition = player.transform.position + direction * spawnRadius * stage;

            // �������� ������ �� �������
            float distanceFromInitialX = Mathf.Abs(spawnPosition.x - initialPlayerPosition.x);
            float distanceFromInitialZ = Mathf.Abs(spawnPosition.z - initialPlayerPosition.z);

            if (distanceFromInitialX <= distanceToBoundary && distanceFromInitialZ <= distanceToBoundary)
            {
                isValidPosition = true;
            }
        }

        return spawnPosition;
    }
    public void StageIncrease()
    {
        results[stage - 1] = 1;
        stage++;
       // Debug.Log($"results Last: {string.Join(", ", results)}");
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        if (stage < maxStage)
        {
            TaskController.Instance.TriggerTaskProgressUpdate();
            Debug.Log("StageIncrease save task results");
            TaskController.Instance.CreatePanelStageComplete();
            

            CreatePickupItemAndArrow();

            
            StartCoroutine(CountdownToStart());
        }
        else
        {
            results[stage - 1] = 1;
            TaskController.Instance.TriggerTaskProgressUpdate();
            Debug.Log("StageIncrease last stage save task results");
            TaskController.Instance.CreatePanelTrialPassed();
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

        timeRemaining = startTime * stage;
        timerIsRunning = true;
        timerCoroutine = StartCoroutine(TimerCoroutine());
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
            TaskController.Instance.TriggerTaskProgressUpdate();
            Debug.Log("TaskFailed save task results");
            TaskController.Instance.SetSceneNumber(sceneNumber);
            TaskController.Instance.CreatePanelTaskFailed();
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

    public bool StopTimer()// �������� ��� �� ���� (������ �������� �� targetPoint)
    {
        timerIsRunning = false;

        if (timeRemaining > 0)
        {
            timeText.text = "������ ����������!";
            return true;
        }
        else
        {
            timeText.color = Color.red;
            timeText.text = "����� �����!";
            return false;
        }
    }
}
