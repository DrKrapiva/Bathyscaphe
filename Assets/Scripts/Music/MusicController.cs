using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    private  bool isMuted;

    public static MusicController Instance
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
    static private MusicController _instance;
    
    private void Start()
    {
        isMuted = SaveGame.Instance.LoadIsMuted();
        UpdateSoundState();
    }
    
    
    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    
    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // ����� ��� ���������/���������� �����
    public void ToggleSound()
    {
        
        isMuted = !isMuted;

        SaveGame.Instance.SaveIsMuted(isMuted);
        
        UpdateSoundState();
    }

    // ����� ��� ���������� ��������� ����� � ����������� �� isMuted
    private void UpdateSoundState()
    {
        
        if (isMuted)
        {
            StopMusic();
        }
        else
        {
            PlayMusic();
        }
    }

    // ����� ��� ��������, �������� �� ����
    public bool IsMuted()
    {
        isMuted = SaveGame.Instance.LoadIsMuted();
        return isMuted;
    }
    
}
