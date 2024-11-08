using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] private Image heroImg;
    public static HUD Instance
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
    static private HUD _instance;

    private void Start()
    {

        GameController.Instance.OnCoinChange.AddListener(ShowCoin);
        GameController.Instance.OnCoinChange.Invoke();
    }
    public void ShowCoin()
    {
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    public void ShowHero()
    {
        heroImg.sprite = Resources.Load<Sprite>("Bats/" + PlayerPrefs.GetString("NowHero"));
    }
}
