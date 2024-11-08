using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Компонент AudioSource для воспроизведения звука
    public AudioClip buttonSound;   // Звуковой эффект для кнопки

    // Метод, который будет вызываться при нажатии кнопки
    public void PlayButtonSound()
    {
        // Проверяем через SoundManager, включен ли звук
        if (!MusicController.Instance.IsMuted() && audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }
}
