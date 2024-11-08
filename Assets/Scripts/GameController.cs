using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject hero;

    public UnityEvent OnCoinChange;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        PlayerPrefs.SetString("NowHero", "bat1");
        LoadNowHero();
    }
    static private GameController _instance;

    public void ChangeCoin(int coin)
    {

        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + coin);
        OnCoinChange.Invoke();
        //HUD.Instance.ShowCoin();
    }
    private void LoadNowHero()
    {
        HUD.Instance.ShowHero();
    }
}
