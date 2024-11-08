using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyMove : MonoBehaviour
{
    protected Transform hero; // ������ ������
    protected NavMeshAgent agent;
    protected Enemy _enemy;
    protected Rigidbody rb;
    protected virtual void Start()
    {
        _enemy = EnemyController.Instance.EnemyInfo(gameObject.name);
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        if(agent != null)
            agent.speed = _enemy.speed; // ��������� �������� ������������ ��� NavMeshAgent
        hero = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        Move();
    }
    public virtual void Move()
    {
        //�������� ������������� 
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
        }
    }

    // �� �������� ����� ������ ��������� ������ ����. ������ ������������� �������� = 0. ������ ���� - ������� ��������.
    public void StartChangeSpeed(float percent, float timer)
    {
        StartCoroutine(ChangeSpeed(percent, timer));
    }
    IEnumerator ChangeSpeed(float percent, float timer)
    {
        float changedSpeed = (agent.speed * percent / 100);

        //�������� �������� 
        if (agent.speed < changedSpeed)
        {
            changedSpeed = agent.speed;
            agent.speed = 0f;
        }
        else
        {
            agent.speed -= changedSpeed;
        }

        yield return new WaitForSeconds(timer);

        agent.speed = _enemy.speed;
    }
    public void StartCorutineGetImpulseMove(float time)
    {
        StartCoroutine(GetImpulseMove(time));
    }
    private IEnumerator GetImpulseMove(float time)
    {
        yield return new WaitForSeconds(time); // ��������� 0.1 �������
        rb.velocity = Vector3.zero; // �������� �������� ����� ������������
    }
}
