using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Button muteButton;

    private void Start()
    {
        // ��������� ������� �������
        muteButton.onClick.AddListener(ToggleSound);

        // ��������� ������� ��������� ����� � ��������� ���� ������ ��� ������
        UpdateButtonColor();
    }

    // ����� ��� ���������/���������� �����
    private void ToggleSound()
    {
        // ����������� ��������� ����� � MusicController
        MusicController.Instance.ToggleSound();

        // ��������� ���� ������
        UpdateButtonColor();
    }

    // ����� ��� ���������� ����� ������ � ����������� �� ��������� �����
    private void UpdateButtonColor()
    {
        if (MusicController.Instance.IsMuted())
        {
            muteButton.image.color = muteButton.colors.pressedColor;
            // ���� ���� ��������, ������ ���� �� "pressedColor"
            Debug.Log("UpdateButtonColor pressedColor ");
        }
        else
        {
            // ���� ���� �������, ������ ���� �� "normalColor"
            muteButton.image.color = muteButton.colors.normalColor;
            Debug.Log("UpdateButtonColor normalColor ");
        }
    }
}
