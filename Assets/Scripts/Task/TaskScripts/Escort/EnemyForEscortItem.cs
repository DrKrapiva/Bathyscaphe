using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyForEscortItem : EnemyMove
{
    protected override void Start()
    {
        base.Start();
        hero = EscortItem.Instance.Target().transform;


    }
    
    public override void Move()
    {
        if (hero != null)
        {
            Vector3 destination = hero.position;
            Vector3 direction = destination - transform.position;
            // �������� ����������, �������������� ������ � ����������� destination
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ��������� ������� � �������
            transform.rotation = targetRotation;

            destination.y += 5f; // ��������, ���� ����� ��������� �� 5 ������ ���� �� ��� Y
            agent.SetDestination(destination);
            // ����������� � ����
            /*Vector3 direction = (hero.position - transform.position).normalized;

            // ����������� � ����������� ����
            transform.position += direction * _enemy.speed * Time.deltaTime;

            // �������� � ���� (���� �����)*/
            //transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    /*[SerializeField] private Slider sliderHP;
    private float hp = 20;
    private float attack = 2;
    private float attackSpeed = 1;
    private bool isAttacking = false;
    private bool canAttack = true;
    private Coroutine coroutineTakeDamage;
    private NavMeshAgent agent;
    private GameObject _target;
    protected void OnTriggerEnter(Collider other)
    {
        //Debug.Log("������� ");
        if (other.gameObject.GetComponent<EscortItem>() != null)
        {
            Debug.Log(gameObject.name + " ������� ����� � ������");
            StartAttacking();

        }
        if (other.gameObject.GetComponent<BulletTouch>() != null)
        {
            Debug.Log("������� ����");
            TakeDamage(other.gameObject.GetComponent<BulletTouch>().Actions());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EscortItem>() != null)
        {
            StopAttacking();
        }
    }
    public void FillInfo(GameObject target)
    {
        _target = target;

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.speed = 20; // ��������� �������� ������������ ��� NavMeshAgent

        sliderHP.maxValue = hp;
        sliderHP.value = hp;
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        //�������� ������������� 
        if (_target != null)
        {

            Vector3 destination = _target.transform.position;
            Vector3 direction = destination - transform.position;
            // �������� ����������, �������������� ������ � ����������� destination
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ��������� ������� � �������
            transform.rotation = targetRotation;

            destination.y += 5f; // ��������, ���� ����� ��������� �� 5 ������ ���� �� ��� Y
            agent.SetDestination(destination);
        }
        
    }
    private void StartAttacking()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(ContinuousAttack());
        }
    }

    private  void StopAttacking()
    {
        isAttacking = false;
        StopCoroutine(ContinuousAttack());
    }
    // �������� ��� ����������� �����
    private IEnumerator ContinuousAttack()
    {
        while (isAttacking)
        {
            //HeroHPController.Instance.TakeHit(Attack()); �������� �� ����� �� �����
            //Attack();
            yield return new WaitForSeconds(1f);
        }
    }
    private  void TakeDamage(float damage)
    {
        hp -= damage;
        sliderHP.value = hp;
        if (hp <= 0)
        {
            hp = 0;
            EnemyDeath();
        }
    }
    public void CheckIsTakeDamage(int damage, int seconds, bool isDamage)//��������� �� ���������� ������. ��. EnemyActions
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
    public  void EnemyDeath()
    {
        Destroy(gameObject);
    }
    private float Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(CoolDownAttack());
            return attack;
        }
        else return 0;
    }

    private IEnumerator CoolDownAttack()
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }*/
}
