using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using TMPro;

public interface IBaseClasses
{
    void Action();
}
public class ShellCoins: IBaseClasses
{
    public void Action()
    {
        
        int coinToAdd = Random.Range(1, 6);
        LevelController.Instance.CountCoins(coinToAdd);
        Debug.Log("������ +" + coinToAdd);
        ShellClasses.Instance.ShowShellBonusText("������ +" + coinToAdd);
    }
}
public class ShellHpScore : IBaseClasses
{
    public void Action()
    {
        
        int scoreHealHp = Random.Range(1, 6);
        HeroHPController.Instance.HealHp(scoreHealHp);
        Debug.Log("�������� +" + scoreHealHp);
        ShellClasses.Instance.ShowShellBonusText("�������� +" + scoreHealHp);
    }
}
public class ShellHpMax : IBaseClasses
{
    public void Action()
    {
        
        int scoreMaxHp = Random.Range(5, 21);
        HeroHPController.Instance.ChangeMaxHp(scoreMaxHp);
        Debug.Log("����.�������� +" + scoreMaxHp);
        ShellClasses.Instance.ShowShellBonusText("����.�������� +" + scoreMaxHp);
    }
}
public class ShellArmorScore: IBaseClasses
{
    public void Action()
    {
        
        int scoreHealArmor = Random.Range(1, 6);
        HeroHPController.Instance.HealArmor(scoreHealArmor);
        Debug.Log("����� +" + scoreHealArmor);
        ShellClasses.Instance.ShowShellBonusText("����� +" + scoreHealArmor);
    }
}
public class ShellMaxArmor : IBaseClasses
{
    //���������� ������� � ���������

    public delegate void ActionTriggered(int maxArmor);

    public static event ActionTriggered OnMaxArmor;

    public void Action()
    {
        //Debug.Log("ShellMaxArmor");
        int scoreMaxArmor = Random.Range(5, 21);
        HeroHPController.Instance.ChangeMaxArmor(scoreMaxArmor);
        Debug.Log("����. ����� +" + scoreMaxArmor);
        ShellClasses.Instance.ShowShellBonusText("����. ����� +" + scoreMaxArmor);
        //�������� invoke ������ �������� ���� �����
        OnMaxArmor?.Invoke(scoreMaxArmor);
    }
}
public class ShellSpeedIncrease: IBaseClasses
{
    //���������� �������
    public static event Action OnSpeedIncrease;
    public void Action()
    {
        
        CharacterLocomotion.Instance.StartCoroutineChangeWalkSpeed(0.5f, 5);
        Debug.Log("�������� +50% �� 5�.");
        ShellClasses.Instance.ShowShellBonusText("�������� +50% �� 5�.");
        //�������� invoke
        OnSpeedIncrease?.Invoke();
    }
}
public class ShellClasses : MonoBehaviour
{
    List<IBaseClasses> listShellClasses = new List<IBaseClasses>();
    [SerializeField] private GameObject bonusPanelPrefab;
    [SerializeField] private Transform bonusPanelParent;
    public static ShellClasses Instance
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
    static private ShellClasses _instance;
    private void Start()
    {
        listShellClasses.Add(new ShellCoins());
        listShellClasses.Add(new ShellHpScore());
        listShellClasses.Add(new ShellHpMax());
        listShellClasses.Add(new ShellArmorScore());
        listShellClasses.Add(new ShellMaxArmor());
        listShellClasses.Add(new ShellSpeedIncrease());
    }
    public void ShowShellBonusText(string text)
    {
        GameObject newPanel = Instantiate(bonusPanelPrefab, bonusPanelParent);
        TextMeshProUGUI panelText = newPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (panelText != null)
        {
            panelText.text = text;
        }
        StartCoroutine(RemovePanelAfterDelay(newPanel, 2));
    }
    private IEnumerator RemovePanelAfterDelay(GameObject panel, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(panel);
    }
    public int IndexFromListShellClasses()
    {
        return Random.Range(0, listShellClasses.Count);
    }

    public void GiveBonus(int index)
    {
        listShellClasses[index].Action();
    }
}
