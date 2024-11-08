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
        Debug.Log("монеты +" + coinToAdd);
        ShellClasses.Instance.ShowShellBonusText("монеты +" + coinToAdd);
    }
}
public class ShellHpScore : IBaseClasses
{
    public void Action()
    {
        
        int scoreHealHp = Random.Range(1, 6);
        HeroHPController.Instance.HealHp(scoreHealHp);
        Debug.Log("здоровье +" + scoreHealHp);
        ShellClasses.Instance.ShowShellBonusText("здоровье +" + scoreHealHp);
    }
}
public class ShellHpMax : IBaseClasses
{
    public void Action()
    {
        
        int scoreMaxHp = Random.Range(5, 21);
        HeroHPController.Instance.ChangeMaxHp(scoreMaxHp);
        Debug.Log("макс.здоровье +" + scoreMaxHp);
        ShellClasses.Instance.ShowShellBonusText("макс.здоровье +" + scoreMaxHp);
    }
}
public class ShellArmorScore: IBaseClasses
{
    public void Action()
    {
        
        int scoreHealArmor = Random.Range(1, 6);
        HeroHPController.Instance.HealArmor(scoreHealArmor);
        Debug.Log("броня +" + scoreHealArmor);
        ShellClasses.Instance.ShowShellBonusText("броня +" + scoreHealArmor);
    }
}
public class ShellMaxArmor : IBaseClasses
{
    //определяем событие с делегатом

    public delegate void ActionTriggered(int maxArmor);

    public static event ActionTriggered OnMaxArmor;

    public void Action()
    {
        //Debug.Log("ShellMaxArmor");
        int scoreMaxArmor = Random.Range(5, 21);
        HeroHPController.Instance.ChangeMaxArmor(scoreMaxArmor);
        Debug.Log("макс. броня +" + scoreMaxArmor);
        ShellClasses.Instance.ShowShellBonusText("макс. броня +" + scoreMaxArmor);
        //вызываем invoke отдаем значение макс брони
        OnMaxArmor?.Invoke(scoreMaxArmor);
    }
}
public class ShellSpeedIncrease: IBaseClasses
{
    //определяем событие
    public static event Action OnSpeedIncrease;
    public void Action()
    {
        
        CharacterLocomotion.Instance.StartCoroutineChangeWalkSpeed(0.5f, 5);
        Debug.Log("скорость +50% на 5с.");
        ShellClasses.Instance.ShowShellBonusText("скорость +50% на 5с.");
        //вызываем invoke
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
