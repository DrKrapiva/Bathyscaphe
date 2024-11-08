using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionDefuseMines : MonoBehaviour
{
    private float startTime = 40f; // Время в секундах (например, 5 минут)
    private float timeRemaining;
    private bool timerIsRunning = false;
    private TextMeshProUGUI timeText;
    private int countdownTime = 3; // Время для обратного отсчета перед стартом

    private int defusedMines = 0;
    private int amountMines = 3;
    private int spawnRadius = 60;
    private bool isAllMinesDefused = false;
    [SerializeField] private GameObject prefabMine;
    // Список для хранения ссылок на мины
    private List<GameObject> mines = new List<GameObject>();
    public static MissionDefuseMines Instance
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
    static private MissionDefuseMines _instance;
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

        timeRemaining = startTime;
        timerIsRunning = true;
        StartCoroutine(TimerCoroutine());

        CreateMines();
    }
    private void CreateMines()
    {
        for (int i = 0; i < amountMines; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
            randomPosition.y = 0; // Устанавливаем высоту на уровне земли
            GameObject mine = Instantiate(prefabMine, randomPosition, Quaternion.identity);
            mines.Add(mine);

            Sprite arrowSprite = Resources.Load<Sprite>("UI/Arrows/pickupItem");
            ArrowPointer.Instance.StartArrowCoroutine(mine, arrowSprite);
        }
    }
    public void DefuseMine(GameObject mine)
    {
        if (mines.Contains(mine))
        {
            mines.Remove(mine);
            defusedMines++;
            //Destroy(mine); // Удаляем обезвреженную мину из сцены
            Debug.Log("Мина обезврежена! Обезврежено мин: " + defusedMines);

            // Проверяем, все ли мины обезврежены
            if (defusedMines >= amountMines && timeRemaining > 0)
            {
                isAllMinesDefused = true;
                Debug.Log("Все мины обезврежены!");
                // Дополнительная логика для завершения миссии при обезвреживании всех мин
            }
        }
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
            Debug.Log("Время вышло!");
            timerIsRunning = false;
            // Дополнительная логика завершения миссии или уровня
            timeText.color = Color.red;
            timeText.text = "Потрачено!";
            //взрываю не обезвреженные
            if (defusedMines < amountMines)
            {
                // Используйте копию списка для перебора
                List<GameObject> minesCopy = new List<GameObject>(mines);
                foreach (GameObject mine in minesCopy)
                {
                    if (mine != null)
                    {
                        // Вызов функции взрыва
                        mine.GetComponent<MinesForDefuse>().Explode();
                    }
                }
            }
            //стоп игра

        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(0, timeToDisplay);

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Изменяем цвет текста на красный, если осталось 5 секунд или меньше
        if (timeToDisplay <= 5)
        {
            timeText.color = Color.red;
        }
        else
        {
            timeText.color = Color.white; // Вернуть белый цвет текста
        }

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
