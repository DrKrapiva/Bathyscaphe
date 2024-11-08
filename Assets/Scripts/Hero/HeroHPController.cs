using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHPController : MonoBehaviour
{
    private float _hp;
    private float _hpMax;
    private float _armor;
    private float _armorMax;
    private float _armorHp;
    private float recovery = 0.3f;//��� � ������� ��������?
    private float timerRegeniration = 2;//���� �� �����
    [SerializeField] private Slider sliderHP;
    [SerializeField] private Slider sliderArmor;
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    private Coroutine coroutineRegeniration;
    public static HeroHPController Instance
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
    static private HeroHPController _instance;
    private void Start()
    {
        Hp = DictionaryHero.Instance.Hp(PlayerPrefs.GetString("NowHero"));
        //Debug.Log(Hp);
        _armor = DictionaryHero.Instance.Armor(PlayerPrefs.GetString("NowHero"));
        //Debug.Log(_armor);
        _armorHp = _armor;
        _armorMax = _armor;
        _hpMax = Hp;

        sliderHP.maxValue = _hpMax;
        sliderHP.value = _hpMax;
        sliderArmor.maxValue = _armorHp;
        sliderArmor.value = _armorHp;

        if (DictionaryUprades.Instance.DictUpgrade["resurrection"].Level > 0)
        {
            StartRegeniration(DictionaryUprades.Instance.HowMuchAdd("resurrection"));
        }
    }
    public float Hp
    {
        get { return _hp; }
        set { _hp = value; }
    }
    public void TakeHit(float attack)
    {
        Debug.Log("���� �������� ����");

        // ���������, �������������� �� �������� �� ����������, � ��������� �
        if (Application.isMobilePlatform)
        {
            Handheld.Vibrate();
        }

        StartCoroutine(HitEffect(0.1f));
        Hp -= Defense(attack);
        sliderHP.value = Hp;
        sliderArmor.value = _armorHp;
        if (Hp <= 0)
            Death();
    }
    IEnumerator HitEffect(float time)
    {

        SetHeroColor(Color.red);
        

        yield return new WaitForSeconds(time);


        SetHeroColor(Color.white);
        
    }
    private void SetHeroColor(Color color)
    {
        if (skinnedMesh != null)
        {
            skinnedMesh.material.color = color;
        }
    }
    private float Defense(float attack)
    {
        if (_armorHp > 0)
        {
            // �������� 20% �� ����� �� _armorHp ���������� �� �������� �����
            _armorHp -= attack * 0.2f;
            if (_armorHp < 0)
            {
                _armorHp = 0; // ������������, ��� _armorHp �� ����� � ������������� ��������
            }

            if (attack <= _armor)
            {
                // ���� ���� ������ ��� ����� _armor, ���� �����������, �� _armorHp �����������
                return 0;
            }
            else
            {
                // ���� ���� ������ _armor, �� ������� ����� ������ � _armor �������� �� ��������
                return attack - _armor;
            }
        }
        else
        {
            // ���� _armorHp ����� 0 ��� ������, ���� ���� ���� �� ��������
            return attack;
        }
    }
    private void Death()
    {
        LevelController.Instance.EndGame();
        Debug.Log("Death");
    }
    public void StartRegeniration(float heroRegen)
    {
        if(coroutineRegeniration != null)
            StopCoroutine(coroutineRegeniration);
        coroutineRegeniration = StartCoroutine(Regeniration(heroRegen));
    }
    IEnumerator Regeniration(float heroRegen)
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timerRegeniration);

            Hp += heroRegen;
            sliderHP.value = Hp;
            if (Hp > _hpMax)
                Hp = _hpMax;
        }
    }
    public void CheckHpAfterLevelingUp()
    {
        // ��������� recovery �� ������������� ��������
        float hpThreshold = _hpMax * recovery;
        // ��������� recovery �� ������������ �����
        float armorHpThreshold = _armorMax * recovery;

        // ���������, ���� �� ������� �������� 30% �� ���������
        if (Hp < hpThreshold)
        {
            Hp = hpThreshold; // ��������������� �������� �� 30% �� ������������� ��������
            sliderHP.value = Hp;
            Debug.Log("�������� ������������� �� 30% �� ���������.");
        }

        // ���������, ���� �� ������� ����� 30% �� ���������
        if (_armorHp < armorHpThreshold)
        {
            _armorHp = armorHpThreshold; // ��������������� ����� �� 30% �� ������������� ��������
            sliderArmor.value = _armorHp;
            Debug.Log("����� ������������� �� 30% �� ���������.");
        }
    }
    public void HealHp(int addScoreHp)
    {   
        Hp += addScoreHp;
        if (Hp > _hpMax)
            Hp = _hpMax;
        

        sliderHP.value = Hp;
    }
    public void ChangeMaxHp(int maxHpScore)
    {
        _hpMax += maxHpScore;
        Hp += maxHpScore;
        sliderHP.maxValue = _hpMax;
    }
    public void HealArmor(int addScoreArmor)
    {
        _armorHp += addScoreArmor;
        if(_armorHp > _armorMax)
            _armorHp = _armorMax;

        sliderArmor.value = _armorHp;
    }
    public void ChangeMaxArmor(int addArmorScore)
    {
        _armor += addArmorScore;
        _armorMax += addArmorScore;
        sliderArmor.maxValue = _armorMax;
    }
}
