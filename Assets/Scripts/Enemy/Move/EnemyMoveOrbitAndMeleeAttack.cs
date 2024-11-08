using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveOrbitAndMeleeAttack : EnemyMove
{
    private bool isAttacking = false;
    private float currentAngle = 0f;
    private float orbitSpeed = 1f; // Скорость орбитального движения в градусах в секунду
    private float attackCooldown;
    private float nextAttackTime;

    private float meleeAttackDistance = 3f; // Дистанция для ближней атаки
    private float orbitRadius = 9f; // Радиус орбиты

    private enum State
    {
        Orbiting,
        PreparingAttack,
        Attacking,
        ReturningToOrbit
    }

    private State currentState = State.Orbiting;

    private float stateTimer = 0f;
    private float additionalOrbitTime = 2f;
    protected override void Start()
    {
        base.Start();

        attackCooldown = _enemy.attackSpeed;
        nextAttackTime = Time.time + attackCooldown;

        additionalOrbitTime = _enemy.attackSpeed;
    }

    public override void Move()
    {
        if (hero != null)
        {
            switch (currentState)
            {
                case State.Orbiting:
                    OrbitMove();
                    break;
                case State.PreparingAttack:
                    PrepareAttack();
                    break;
                case State.Attacking:
                    AttackMove();
                    break;
                case State.ReturningToOrbit:
                    ReturnToOrbit();
                    break;
            }
        }
    }

    private void OrbitMove()
    {
        //Debug.Log("OrbitMove");
        // Обновляем угол для орбитального движения
        currentAngle += orbitSpeed * Time.deltaTime;
        if (currentAngle >= 360f)
        {
            currentAngle -= 360f;
        }

        // Вычисляем новую позицию врага по кругу вокруг игрока
        Vector3 offset = new Vector3(Mathf.Sin(currentAngle), 0, Mathf.Cos(currentAngle)) * orbitRadius;
        Vector3 targetPosition = hero.position + offset;

        // Перемещение врага к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, _enemy.speed * Time.deltaTime);

        // Поворачиваем врага по ходу движения
        RotateTowards(targetPosition - transform.position);

        // Обновляем таймер состояния
        stateTimer += Time.deltaTime;

        // Проверка готовности к атаке
        if (Time.time >= nextAttackTime && stateTimer >= additionalOrbitTime)
        {
            currentState = State.PreparingAttack;
            stateTimer = 0.5f; // Время на поворот к игроку
        }
    }

    private void PrepareAttack()
    {
        //Debug.Log("PrepareAttack");
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            currentState = State.Attacking;
            stateTimer = 1.5f; // Время на движение к игроку
        }

        // Поворачиваемся к игроку
        Vector3 directionToPlayer = (hero.position - transform.position).normalized;
        RotateTowardsInstantly(directionToPlayer);
    }

    private void AttackMove()
    {
        //Debug.Log("AttackMove");
        if (hero == null)
        {
            currentState = State.Orbiting;
            isAttacking = false;
            return;
        }
        stateTimer -= Time.deltaTime;
        float distanceToHero = Vector3.Distance(transform.position, hero.position);
        // Двигаемся к игроку только если расстояние больше чем meleeAttackDistance
        if (distanceToHero > meleeAttackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, hero.position, _enemy.speed * Time.deltaTime);
        }

        // Проверка дистанции для атаки
        if (Vector3.Distance(transform.position, hero.position) <= meleeAttackDistance)
        {
            //HeroHPController.Instance.TakeHit(_enemy.meleeAttack);
            nextAttackTime = Time.time + attackCooldown;
            currentState = State.ReturningToOrbit;
            stateTimer = 0.5f; // Время на поворот и возврат на орбиту
        }

        if (stateTimer <= 0)
        {
            currentState = State.ReturningToOrbit;
            stateTimer = 0.5f; // Время на поворот и возврат на орбиту
        }
    }

    private void ReturnToOrbit()
    {
        //Debug.Log("ReturnToOrbit");
        if (hero == null)
        {
            currentState = State.Orbiting;
            isAttacking = false;
            return;
        }
        stateTimer -= Time.deltaTime;

        Vector3 directionFromPlayer = (transform.position - hero.position).normalized;
        RotateTowardsInstantly(directionFromPlayer);

        Vector3 offset = new Vector3(Mathf.Sin(currentAngle), 0, Mathf.Cos(currentAngle)) * orbitRadius;
        Vector3 targetPosition = hero.position + offset;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _enemy.speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f || stateTimer <= 0)
        {
            currentState = State.Orbiting;
            stateTimer = 0f;
        }

        // Поворачиваемся по ходу орбиты
        Vector3 orbitDirection = (hero.position + offset - transform.position).normalized;
        RotateTowardsInstantly(orbitDirection);
    }

    private void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _enemy.speed);
    }

    private void RotateTowardsInstantly(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }
    /*private bool isAttacking = false;
    private float currentAngle = 0f;
    private float orbitSpeed = 1f; // Скорость орбитального движения в градусах в секунду
    private float attackCooldown;
    private float nextAttackTime;

    private float meleeAttackDistance = 1.0f; // Дистанция для ближней атаки
    private float orbitRadius = 9f; // Радиус орбиты

    protected override void Start()
    {
        base.Start();

        attackCooldown = _enemy.attackSpeed;
        nextAttackTime = Time.time + attackCooldown;
    }

    public override void Move()
    {
        if (hero != null)
        {
            if (!isAttacking)
            {
                // Обновляем угол для орбитального движения
                currentAngle += orbitSpeed * Time.deltaTime;
                if (currentAngle >= 360f)
                {
                    currentAngle -= 360f;
                }

                // Вычисляем новую позицию врага по кругу вокруг игрока
                Vector3 offset = new Vector3(Mathf.Sin(currentAngle), 0, Mathf.Cos(currentAngle)) * orbitRadius;
                Vector3 targetPosition = hero.position + offset;

                // Перемещение врага к целевой позиции
                transform.position = Vector3.Lerp(transform.position, targetPosition, _enemy.speed * Time.deltaTime);

                // Поворачиваем врага по ходу движения
                RotateTowards(targetPosition - transform.position);

                // Проверка готовности к атаке
                if (Time.time >= nextAttackTime)
                {
                    PerformAttack();
                }
            }
            else
            {
                AttackMove();
            }
        }
    }

    private void PerformAttack()
    {
        Debug.Log("PerformAttack");
        isAttacking = true;

        // Останавливаемся и поворачиваемся к игроку
        Vector3 directionToPlayer = (hero.position - transform.position).normalized;
        RotateTowardsInstantly(directionToPlayer);
        Invoke("AttackMove", 0.5f); // Немного подождем, чтобы повернуться к игроку
    }

    private void AttackMove()
    {
        Debug.Log("AttackMove");
        if (hero == null)
        {
            isAttacking = false;
            return;
        }

        // Двигаемся к игроку для атаки
        float startTime = Time.time;
        while (Vector3.Distance(transform.position, hero.position) > meleeAttackDistance && Time.time - startTime < 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, hero.position, _enemy.speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, hero.position) <= meleeAttackDistance)
            {
                HeroHPController.Instance.TakeHit(_enemy.meleeAttack);
                nextAttackTime = Time.time + attackCooldown;
                ReturnToOrbit();
                return;
            }
        }

        // Устанавливаем следующий момент для атаки
        nextAttackTime = Time.time + attackCooldown;
        ReturnToOrbit();
    }

    private void ReturnToOrbit()
    {
        Debug.Log("ReturnToOrbit");
        if (hero == null)
        {
            isAttacking = false;
            return;
        }

        Vector3 directionFromPlayer = (transform.position - hero.position).normalized;
        RotateTowardsInstantly(directionFromPlayer);

        Vector3 offset = new Vector3(Mathf.Sin(currentAngle), 0, Mathf.Cos(currentAngle)) * orbitRadius;
        Vector3 targetPosition = hero.position + offset;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _enemy.speed * Time.deltaTime);
        }

        Vector3 orbitDirection = (hero.position + offset - transform.position).normalized;
        RotateTowardsInstantly(orbitDirection);

        isAttacking = false;
    }
    private void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _enemy.speed);
    }
    private void RotateTowardsInstantly(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }*/
}
