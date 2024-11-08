using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    
    public void StartNewGame()
    {
        SaveGame.Instance.NewGame();
        SceneManager.LoadScene("Menu");
        Debug.Log("New Game");
    }
}
