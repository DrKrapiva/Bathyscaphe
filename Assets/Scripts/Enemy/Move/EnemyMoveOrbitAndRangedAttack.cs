using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveOrbitAndRangedAttack : EnemyMove
{
    private RangedAttack rangedAttackInfo;
    private bool isAttacking = false;
    private float currentAngle = 0f;
    private float orbitSpeed = 1f; // Скорость орбитального движения в градусах в секунду
    private float attackCooldown;
    private float nextAttackTime;

    public GameObject projectilePrefab; // Префаб снаряда

    protected override void Start()
    {
        base.Start();

        // Получаем информацию о дальнем атакующем враге из словаря
        string enemyName = gameObject.name;
        if (!RangedAttackDictionary.DictRangedAttack().TryGetValue(enemyName, out rangedAttackInfo))
        {
            Debug.LogError("Ranged attack info not found in dictionary for: " + enemyName);
        }

        attackCooldown = rangedAttackInfo.attackCooldown;
        nextAttackTime = Time.time + attackCooldown;
    }

    public override void Move()
    {
        if (hero != null && !isAttacking)
        {
            // Обновляем угол для орбитального движения
            currentAngle += orbitSpeed * Time.deltaTime;
            if (currentAngle >= 360f)
            {
                currentAngle -= 360f;
            }

            // Вычисляем новую позицию врага по кругу вокруг игрока
            Vector3 offset = new Vector3(Mathf.Sin(currentAngle), 0, Mathf.Cos(currentAngle)) * rangedAttackInfo.radius;
            Vector3 targetPosition = hero.position + offset;

            // Перемещение врага к целевой позиции
            transform.position = Vector3.Lerp(transform.position, targetPosition, _enemy.speed * Time.deltaTime);
            RotateTowards(targetPosition - transform.position); // Поворачиваем врага по ходу движения

            // Проверка готовности к атаке
            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(RangedAttack());
            }
        }
    }

    private IEnumerator RangedAttack()
    {
        isAttacking = true;

        // Поворачиваемся к игроку перед атакой
        Vector3 directionToPlayer = (hero.position - transform.position).normalized;
        RotateTowardsInstantly(directionToPlayer);

        yield return new WaitForSeconds(0.5f); // Немного подождем, чтобы повернуться к игроку

        // Создаем снаряд
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<RangedAttackProjectile>().Initialize(hero.position, rangedAttackInfo.damage, rangedAttackInfo.projectileSpeed, rangedAttackInfo.projectileLifetime);
        }

        // Устанавливаем следующий момент для атаки
        nextAttackTime = Time.time + attackCooldown;

        yield return new WaitForSeconds(0.5f); // Немного подождем после атаки

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
    }
}
