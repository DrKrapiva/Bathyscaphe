using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Button muteButton;

    private void Start()
    {
        // Назначаем событие нажатия
        muteButton.onClick.AddListener(ToggleSound);

        // Проверяем текущее состояние звука и обновляем цвет кнопки при старте
        UpdateButtonColor();
    }

    // Метод для включения/отключения звука
    private void ToggleSound()
    {
        // Переключаем состояние звука в MusicController
        MusicController.Instance.ToggleSound();

        // Обновляем цвет кнопки
        UpdateButtonColor();
    }

    // Метод для обновления цвета кнопки в зависимости от состояния звука
    private void UpdateButtonColor()
    {
        if (MusicController.Instance.IsMuted())
        {
            muteButton.image.color = muteButton.colors.pressedColor;
            // Если звук выключен, меняем цвет на "pressedColor"
            Debug.Log("UpdateButtonColor pressedColor ");
        }
        else
        {
            // Если звук включен, меняем цвет на "normalColor"
            muteButton.image.color = muteButton.colors.normalColor;
            Debug.Log("UpdateButtonColor normalColor ");
        }
    }
}
