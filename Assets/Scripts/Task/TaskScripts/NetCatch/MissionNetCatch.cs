using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionNetCatch : MonoBehaviour
{
    private TextMeshProUGUI timeText;

    private int caughtFish = 0;
    private int amountFish = 3;
    private int spawnRadius = 60;
    [SerializeField] private GameObject prefabFish;
    // Делегат и событие
    public delegate void NetPickedUpHandler();
    public static event NetPickedUpHandler OnNetPickedUp;
    public static MissionNetCatch Instance
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
    static private MissionNetCatch _instance;
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

        CreateFish();
    }
    public void FishCounter()
    {
        caughtFish++;

        CheackWin();
    }
    private void CheackWin()
    {
        if (caughtFish >= amountFish)
        {
            timeText.text = "Победа!";
        }
    }
    private void CreateFish()
    {
        for (int i = 0; i < amountFish; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
            randomPosition.y = 1; // Устанавливаем высоту на уровне земли
            GameObject fish = Instantiate(prefabFish, randomPosition, Quaternion.identity);

            Sprite arrowSprite = Resources.Load<Sprite>("UI/Arrows/pickupItem");
            ArrowPointer.Instance.StartArrowCoroutine(fish, arrowSprite);
        }
    }
    public void PickUpNet()
    {
        Debug.Log("PickUpNet");
        //FishCounter();
        // Вызов события
        OnNetPickedUp?.Invoke();
    }
}
