using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // ��������� AudioSource ��� ��������������� �����
    public AudioClip buttonSound;   // �������� ������ ��� ������

    // �����, ������� ����� ���������� ��� ������� ������
    public void PlayButtonSound()
    {
        // ��������� ����� SoundManager, ������� �� ����
        if (!MusicController.Instance.IsMuted() && audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }
}
