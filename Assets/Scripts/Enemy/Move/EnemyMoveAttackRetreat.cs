using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAttackRetreat : EnemyMove
{
    private bool movingToPlayer = true;
    private bool attacking = false;
    private bool retreating = false;
    [SerializeField] private GameObject slowCloudPrefab; // ������ ������ ����������
    private GameObject currentSlowCloud;
    private EnemyAbilities enemyAbilities;

    protected override void Start()
    {
        base.Start();
        // �������� ����������� ����� �� �������
        string enemyName = gameObject.name; // ��������������, ��� ��� ������� ��������� � ������ �������
        if (EnemyDictionaryAbilities.DictEnemyAbilities().TryGetValue(enemyName, out enemyAbilities))
        {
            // ��������� ������� ��������� �� �������
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
                // �������� � ������
                Vector3 directionToPlayer = (hero.position - transform.position).normalized;
                Vector3 targetPosition = hero.position - directionToPlayer * enemyAbilities.attackDistance;
                MoveTowards(targetPosition);

                if (Vector3.Distance(transform.position, hero.position) <= enemyAbilities.attackDistance)
                {
                    // �������� �����
                    attacking = true;
                    movingToPlayer = false;
                    StartCoroutine(AttackCooldown());

                    // ������� ������ ����������
                    if (currentSlowCloud == null)
                    {
                        currentSlowCloud = Instantiate(slowCloudPrefab, hero.position, Quaternion.identity);
                        currentSlowCloud.GetComponent<SlowCloud>().Initialize(-enemyAbilities.slowAmount, enemyAbilities.slowDuration); // ���������� 
                    }
                }
            }
            else if (attacking)
            {
                // ������ �����
                // ����� �������� �������������� �������� �����
            }
            else if (retreating)
            {
                // ����������� �� ������
                Vector3 directionFromPlayer = (transform.position - hero.position).normalized;
                Vector3 retreatPosition = transform.position + directionFromPlayer * enemyAbilities.retreatDistance;

                // ������� ����� ���������
                RotateTowards(directionFromPlayer);

                // ��������� ����� ������ ����� ��������
                MoveTowards(retreatPosition);

                if (Vector3.Distance(transform.position, hero.position) >= enemyAbilities.retreatDistance)
                {
                    // ������������ � �������� � ������
                    retreating = false;
                    movingToPlayer = true;
                    // ������� ������ ����������
                    if (currentSlowCloud != null)
                    {
                        Destroy(currentSlowCloud);
                    }
                }
            }

            // ������������ ����� � ������� ��������
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
