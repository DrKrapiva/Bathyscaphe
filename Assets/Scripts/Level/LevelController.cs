using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private float pointExp = 0;
    private int coin = 0;
    private float levelScore = 20;//������ 10 ������, ���������
    private int levelNumber = 0;
    private float levelIncreasePersent = 0.2f;
    private float elapsedTime = 0f; // �����, ��������� � ������� ������� �����������
    private string lastPassiveWeaponName;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelEndGame;
    [SerializeField] private GameObject prefabIconPassiveWeapon;
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform canvasForIconPassiveWeapon;
    [SerializeField] private TextMeshProUGUI textCoins;
    //[SerializeField] private TextMeshProUGUI stopwatch;
    [SerializeField] private Slider levelSlider;
    [SerializeField] private TextMeshProUGUI levelNumberText;

    public static event Action<int> OnTimePassed;
    public static event Action<int> OnCoinsCollected;
    public static event Action OnHeroDied;

    //private List<string> taskKeys = new List<string>() { "task1", "task2", "task3", "task4", "task5", "task6" };//����� ������ � ��������� �� ������

    public static LevelController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        textCoins.text = coin.ToString();
        levelSlider.maxValue = levelScore;
        levelSlider.value = pointExp;
        levelNumberText.text = levelNumber.ToString();
    }
    static private LevelController _instance;
    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(Stopwatch());
    }
    
    IEnumerator Stopwatch()
    {
        while (true)
        {
            elapsedTime += Time.deltaTime;
           // stopwatch.text = elapsedTime.ToString("F0");
            yield return null;
        }
    }
    
    public float GetElapsedTime()//��� ������� ������� � ����� ������ ��� �� �����
    {
        return elapsedTime;
    }

    public void CountPointExp(int addPointExp)
    {
        pointExp +=addPointExp;
        levelSlider.value = pointExp;
        StartCoroutine(LevelUpProcess());
    }
    private IEnumerator LevelUpProcess()
    {
        while (pointExp >= levelScore)
        {
            pointExp -= levelScore;
            levelSlider.value = pointExp;
            UpLevel();
            Debug.Log("��������� ������");
            // ����, ���� ������ ��������� ���������� ������ �� ����� �������
            yield return new WaitUntil(() => !UpgradePassiveWeaponPanel.Instance.IsUpgradePanelActive());
            
        }
    }
    private void UpLevel()
    {
        levelScore = Mathf.Round(levelScore + (levelScore * levelIncreasePersent));
        Debug.Log("����� ���� " + levelScore);

        levelNumber++;
        levelNumberText.text = levelNumber.ToString();

        // ��������� ������������ �������� ��������
        levelSlider.maxValue = levelScore;

        //������ ��������� ������
        UpgradePassiveWeapon.Instance.ShowUpgradeWeaponPanel();
    }
    public void CountCoins(int addCoins)
    {
        coin += addCoins;
        textCoins.text = coin.ToString();
    }

    public void PauseGame()
    {
        TaskController.Instance.TriggerTaskProgressUpdate();
        Debug.Log("PauseGame save task results");

        Time.timeScale = 0f;
        var pausePanel = Instantiate(panelPause, canvas);
        pausePanel.GetComponent<PanelPause>().FillInfo(coin, (int)GetElapsedTime(), UpgradePassiveWeapon.Instance.GetUsedWeapons());
    }
    public void LeaveLevel()
    {
        OnCoinsCollected?.Invoke(coin);
        OnTimePassed?.Invoke((int)GetElapsedTime());
    }
    public void EndGame()
    {
        TaskController.Instance.TriggerTaskProgressUpdate();
        Debug.Log("EndGame save task results");

        OnCoinsCollected?.Invoke(coin);
        OnTimePassed?.Invoke((int)GetElapsedTime());
        OnHeroDied?.Invoke();

        Time.timeScale = 0f;
        var endPanel = Instantiate(panelEndGame, canvas);
        endPanel.GetComponent<PanelEndGame>().FillInfo(coin, (int)GetElapsedTime(), UpgradePassiveWeapon.Instance.GetUsedWeapons());
    }
    public void PauseOff()
    {
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene("Level");
    }
    public void LoadSceneByIndex(int sceneIndex)//������������ ��� ������������ �����. ����� �������� ��� ������ ��� ����������� � taskControllera
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void RenameLastWeaponName(string newPassiveWeaponName)
    {
        lastPassiveWeaponName = newPassiveWeaponName;
        //Debug.Log(lastPassiveWeaponName + " lastPassiveWeaponName LevelController");
    }
    public GameObject GetPassiveWeaponIcon()
    {
        var iconPassiveWeapon = Instantiate(prefabIconPassiveWeapon, canvasForIconPassiveWeapon, false);
        //iconPassiveWeapon.name = lastPassiveWeaponName;
       // Debug.Log("Icon Passive Weapon created with name: " + iconPassiveWeapon.name); //  ��� ��� �������� �����

        // ��������, ��� ��� ��������� ����� ������� FillInfo
        var passiveWeaponSlot = iconPassiveWeapon.GetComponent<PassiveWeaponSlot>();
        if (passiveWeaponSlot != null)
        {
            passiveWeaponSlot.SetWeaponName(lastPassiveWeaponName); // �������� ���
            passiveWeaponSlot.FillInfo(); // ��������� ���������� ����� ��������� �����
        }

        return iconPassiveWeapon;
    }
}
