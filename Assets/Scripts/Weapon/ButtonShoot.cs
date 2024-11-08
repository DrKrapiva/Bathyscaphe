using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShoot : MonoBehaviour
{
    private float cooldown;
    private string weaponNameShort;
    private string longWeaponName;
    public Image cooldownImage;
    private bool isAutoFire;

    private int _countBullet;
    private int createdBullet = 0;

    public delegate void ActionTriggered(string weaponName);

    public static event ActionTriggered OnShoot;

    public AudioSource audioSource;  // Добавляем AudioSource
    private AudioClip fireSound;     // Добавляем AudioClip для звука
    private void OnEnable()
    {
        // Подписка на событие
        MissionNetCatch.OnNetPickedUp += PickUpNet;
    }

    private void OnDisable()
    {
        // Отписка от события
        MissionNetCatch.OnNetPickedUp -= PickUpNet;
    }
    public void FillInfo(string name, float cd, int countBullet)
    {
        cooldown = cd;
        weaponNameShort = name;
        _countBullet = countBullet;
        //Debug.Log(weaponName);
        GetComponent<Image>().sprite = Resources.Load<Sprite>(weaponNameShort);
        gameObject.GetComponent<Button>().interactable = true;

        // Получение правильного ключа с редкостью
        longWeaponName = SaveWeaponUpgrade.Instance.GetWeaponLevelRarity(name);
        //Debug.Log(weaponKey);
        // Проверка параметра автострельбы
        ActWeapon actWeapon = ActivWeapon.Instance.GetActiveWeaponInfo(longWeaponName);
        isAutoFire = actWeapon.autoFire;
        if (isAutoFire)
        {
            gameObject.GetComponent<Button>().interactable = false; // Отключение кнопки

            StartAutoFire();
        }

        // Загрузка звука выстрела
        fireSound = Resources.Load<AudioClip>("Music/" + weaponNameShort);

        CheckCanUseNet();
    }
    private void CheckCanUseNet()
    {
        if(weaponNameShort == "Net" )
        {
            gameObject.GetComponent<Button>().interactable = _countBullet > createdBullet; 
        }
    }
    public void Fire()
    {
        // Проигрываем звук выстрела
        PlayFireSound();

        OnShoot?.Invoke(weaponNameShort);
        StartCoroutine(Cooldown());

        if (weaponNameShort == "Net")
        {
            createdBullet++;
            CheckCanUseNet();
            
        }
    }
    private void PlayFireSound()
    {
        // Проверяем, включен ли звук, и существует ли аудиоклип
        if (!MusicController.Instance.IsMuted() && audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
    IEnumerator Cooldown()
    {
        gameObject.GetComponent<Button>().interactable = false;

        float timeElapsed = 0;

        while (timeElapsed < cooldown)
        {
            timeElapsed += Time.deltaTime;
            cooldownImage.fillAmount = 1 - (timeElapsed / cooldown);
            yield return null;
        }

        cooldownImage.fillAmount = 0;// ???
        gameObject.GetComponent<Button>().interactable = true;
    }
    // Метод для автоматической стрельбы
    private void StartAutoFire()
    {
        StartCoroutine(AutoFireCoroutine());
    }

    IEnumerator AutoFireCoroutine()
    {
        while (true)
        {
            //Fire();
            ActivWeapon.Instance.CreateWeapon(Resources.Load<GameObject>("Prefab/Bullet/" + longWeaponName), weaponNameShort);
            PlayFireSound();
            //ActivWeapon.Instance.CreateWeapon(Resources.Load<GameObject>("Prefab/Bullet/" + weaponName));
            yield return new WaitForSeconds(cooldown);
        }
    }
    //изменение cooldown
    public void ChangeCoolDown(int decreaseCooldownPercent)
    {
        float decreaseAmount = cooldown * (decreaseCooldownPercent / 100f);

        cooldown -= decreaseAmount;

        if (cooldown < 0)
        {
            cooldown = 0;
        }
    }
    private void PickUpNet()
    {
        createdBullet--;
        CheckCanUseNet();
    }
}
