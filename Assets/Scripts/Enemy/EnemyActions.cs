using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyActions : EnemyCreature
{
    private Enemy _enemy;
    protected bool canAttack = true;
    private Coroutine coroutineTakeDamage;
    [SerializeField] private Slider sliderHP;
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    private static int chestCounter = 0;

    public static event Action OnEnemyDeath;
    public delegate void EnemyDeathHandler(string enemyName);
    public static event EnemyDeathHandler OnEnemyDeathWithName;

    private void Start()
    {
        _enemy = EnemyController.Instance.EnemyInfo(gameObject.name);
        FillInfo();
        sliderHP.maxValue = _enemy.hp;
        sliderHP.value = _enemy.hp;

    }
    protected override void FillInfo()
    {
        

        if (gameObject.GetComponent<EnemyForEscortItem>() != null)
        {
            hero = EscortItem.Instance.Target().transform;
            //Debug.Log("FillInfo �������");
        }
        else
        {
            base.FillInfo();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        
        if (gameObject.GetComponent<EnemyForEscortItem>() != null)
        {
            Debug.Log("��������� ����");
            //Debug.Log("EscortItem" + other.gameObject.GetComponent<EscortItem>());

            // ������������� ������ ��� ������ � ����������� EnemyForEscortItem
            if (other.gameObject.GetComponent<EscortItem>() != null)
            {
                Debug.Log(gameObject.name + " ������� ����� � ������ (����������� ������)");
                // �������������� ������ ��� ��������
                StartAttacking();
            }
            if (other.gameObject.GetComponent<BulletTouch>() != null)
            {
                Debug.Log("������� ����");
                TakeDamage(other.gameObject.GetComponent<BulletTouch>().Actions());
            }
            if (other.gameObject.GetComponent<Explosion>() != null)
            {
                Debug.Log("������� �����");
                TakeDamage(other.gameObject.GetComponent<Explosion>().Actions());
            }
        }
        else
        {
            //Debug.Log("����� ������� ������ ��� ��������� �������");
            // ����� ������� ������ ��� ��������� �������
            base.OnTriggerEnter(other);
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        

        if (gameObject.GetComponent<EnemyForEscortItem>() != null)
        {
            if (other.gameObject.GetComponent<EscortItem>() != null)
            {
                StopAttacking();
            }
        }
        else
        {
            base.OnTriggerExit(other);
        }
    }
    protected override IEnumerator ContinuousAttack()
    {
        
        if (gameObject.GetComponent<EnemyForEscortItem>() != null)
        {
            Debug.Log("ContinuousAttack ��� EnemyForEscortItem");
            while (isAttacking)
            {
                EscortItem.Instance.TakeHit(Attack());
                //Attack();
                yield return new WaitForSeconds(1f);
            }
        }
        else
        {
            Debug.Log("ContinuousAttack ����� ������� ������ ��� ��������� �������");
            yield return StartCoroutine(base.ContinuousAttack());
            //base.ContinuousAttack();
        }
    }
    public override void TakeDamage(float damage)
    {
        _enemy.hp -= damage;
        sliderHP.value = _enemy.hp;
        if (_enemy.hp <= 0)
        {
            _enemy.hp = 0;
            EnemyDeath();
        }
    }
    public void CheckIsTakeDamage(int damage, int seconds, bool isDamage)
    {

        if (isDamage)
            coroutineTakeDamage = StartCoroutine(StartTakeDamage(damage, seconds));
        else
        {
            StopCoroutine(coroutineTakeDamage);
            Debug.Log("StopCoroutine");
        }
    }
    IEnumerator StartTakeDamage(int damage, int seconds)
    {

        for (; ; )
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(seconds);
        }
    }
    public override void EnemyDeath()
    {
        OnEnemyDeathWithName?.Invoke(gameObject.name);
        OnEnemyDeath?.Invoke();

        //������� ����
        GameObject prefab = Resources.Load<GameObject>("Prefab/PointExp/" + "PointExp" + _enemy.difficultLevel);
        GameObject bonus = Instantiate(prefab, transform.position, Quaternion.identity);
        bonus.GetComponent<PointExp>().FillInfo(_enemy.scoresAfterDeath);

        //������� ������
        if (GetComponent<Boss>() != null)
        {
            GameObject prefabChest = Resources.Load<GameObject>("Prefab/Chest/Chest");
            GameObject chest = Instantiate(prefabChest, transform.position, Quaternion.identity);
            //��������� �������� ��� ���������
            chest.name = "Chest_" + chestCounter++;
            Debug.Log("������ ������: " + chest.gameObject.name);

            Sprite arrowSprite = Resources.Load<Sprite>("UI/Arrows/Chest");
            ArrowPointer.Instance.StartArrowCoroutine(chest, arrowSprite);
        }

        Death();
    }

    public override float Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(CoolDownAttack());
            return _enemy.meleeAttack;
        }
        else return 0;
    }

    public override IEnumerator CoolDownAttack()
    {
        yield return new WaitForSeconds(_enemy.attackSpeed);
        canAttack = true;
    }
    public void StartCoroutineFrozenEffect(float time, float newSpeed)
    {
        if(skinnedMesh != null)
            StartCoroutine(FrozenEffect(time, newSpeed));
    }
    IEnumerator FrozenEffect(float time, float newSpeed)
    {
        //FrozenEffectSkin("Frozen");
        // ��������� ������ ���������
        SetEnemyColor(Color.cyan);
        //����� �������� �����
        gameObject.GetComponent<EnemyMove>().StartChangeSpeed(newSpeed, time);
        isFrozen = true; 

        yield return new WaitForSeconds(time);

        //FrozenEffectSkin("");
        // ������� ������ ���������, ���������� �������� ����
        SetEnemyColor(Color.white);
        isFrozen = false;
    }
    private void SetEnemyColor(Color color)
    {
        if (skinnedMesh != null)
        {
            skinnedMesh.material.color = color;
        }
    }
    private void FrozenEffectSkin(string effect)
    {
        //Debug.Log("Prefab/Material/" + _enemy.nameKey + effect);
        skinnedMesh.material = Resources.Load<Material>("Prefab/Material/" + _enemy.nameKey + effect);
    }
}
