using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAttackRetreat : EnemyMove
{
    private bool movingToPlayer = true;
    private bool attacking = false;
    private bool retreating = false;
    [SerializeField] private GameObject slowCloudPrefab; // Префаб облака замедления
    private GameObject currentSlowCloud;
    private EnemyAbilities enemyAbilities;

    protected override void Start()
    {
        base.Start();
        // Получаем способности врага из словаря
        string enemyName = gameObject.name; // Предполагается, что имя объекта совпадает с ключом словаря
        if (EnemyDictionaryAbilities.DictEnemyAbilities().TryGetValue(enemyName, out enemyAbilities))
        {
            // Параметры успешно загружены из словаря
        }
        else
        {
            Debug.LogError("Abilities for enemy " + enemyName + " not found in dictionary.");
        }
    }

    public override void Move()
    {
        if (hero != null)
        {
            if (movingToPlayer)
            {
                // Движение к игроку
                Vector3 directionToPlayer = (hero.position - transform.position).normalized;
                Vector3 targetPosition = hero.position - directionToPlayer * enemyAbilities.attackDistance;
                MoveTowards(targetPosition);

                if (Vector3.Distance(transform.position, hero.position) <= enemyAbilities.attackDistance)
                {
                    // Начинаем атаку
                    attacking = true;
                    movingToPlayer = false;
                    StartCoroutine(AttackCooldown());

                    // Создаем облако замедления
                    if (currentSlowCloud == null)
                    {
                        currentSlowCloud = Instantiate(slowCloudPrefab, hero.position, Quaternion.identity);
                        currentSlowCloud.GetComponent<SlowCloud>().Initialize(-enemyAbilities.slowAmount, enemyAbilities.slowDuration); // Замедление 
                    }
                }
            }
            else if (attacking)
            {
                // Логика атаки
                // Можно добавить дополнительные действия атаки
            }
            else if (retreating)
            {
                // Отступление от игрока
                Vector3 directionFromPlayer = (transform.position - hero.position).normalized;
                Vector3 retreatPosition = transform.position + directionFromPlayer * enemyAbilities.retreatDistance;

                // Поворот перед движением
                RotateTowards(directionFromPlayer);

                // Двигаемся назад только после поворота
                MoveTowards(retreatPosition);

                if (Vector3.Distance(transform.position, hero.position) >= enemyAbilities.retreatDistance)
                {
                    // Возвращаемся к движению к игроку
                    retreating = false;
                    movingToPlayer = true;
                    // Удаляем облако замедления
                    if (currentSlowCloud != null)
                    {
                        Destroy(currentSlowCloud);
                    }
                }
            }

            // Поворачиваем врага в сторону движения
            if (movingToPlayer || attacking)
            {
                Vector3 direction = (hero.position - transform.position).normalized;
                RotateTowards(direction);
            }
        }
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * _enemy.speed * Time.deltaTime;
    }

    private void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _enemy.speed);
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(enemyAbilities.attackCooldown);
        attacking = false;
        retreating = true;
    }
}
