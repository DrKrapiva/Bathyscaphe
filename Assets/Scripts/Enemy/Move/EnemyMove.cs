using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyMove : MonoBehaviour
{
    protected Transform hero; // объект игрока
    protected NavMeshAgent agent;
    protected Enemy _enemy;
    protected Rigidbody rb;
    protected virtual void Start()
    {
        _enemy = EnemyController.Instance.EnemyInfo(gameObject.name);
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        if(agent != null)
            agent.speed = _enemy.speed; // установка скорости передвижения для NavMeshAgent
        hero = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        Move();
    }
    public virtual void Move()
    {
        //движение преследования 
        if (hero != null)
        {

            Vector3 destination = hero.position;
            Vector3 direction = destination - transform.position;
            // Получаем кватернион, поворачивающий объект в направлении destination
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Применяем поворот к объекту
            transform.rotation = targetRotation;

            destination.y += 5f; // Например, враг будет следовать на 5 единиц выше по оси Y
            agent.SetDestination(destination);
        }
    }

    // на скорость может влиять пассивное оружие хиро. разряд электричества скорость = 0. мутные воды - снижают скорость.
    public void StartChangeSpeed(float percent, float timer)
    {
        StartCoroutine(ChangeSpeed(percent, timer));
    }
    IEnumerator ChangeSpeed(float percent, float timer)
    {
        float changedSpeed = (agent.speed * percent / 100);

        //изменяем скорость 
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
        yield return new WaitForSeconds(time); // Подождать 0.1 секунды
        rb.velocity = Vector3.zero; // Обнулить скорость после отталкивания
    }
}
