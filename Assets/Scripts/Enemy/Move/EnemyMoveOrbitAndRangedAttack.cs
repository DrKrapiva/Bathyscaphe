using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveOrbitAndRangedAttack : EnemyMove
{
    private RangedAttack rangedAttackInfo;
    private bool isAttacking = false;
    private float currentAngle = 0f;
    private float orbitSpeed = 1f; // �������� ������������ �������� � �������� � �������
    private float attackCooldown;
    private float nextAttackTime;

    public GameObject projectilePrefab; // ������ �������

    protected override void Start()
    {
        base.Start();

        // �������� ���������� � ������� ��������� ����� �� �������
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
            // ��������� ���� ��� ������������ ��������
            currentAngle += orbitSpeed * Time.deltaTime;
            if (currentAngle >= 360f)
            {
                currentAngle -= 360f;
            }

            // ��������� ����� ������� ����� �� ����� ������ ������
            Vector3 offset = new Vector3(Mathf.Sin(currentAngle), 0, Mathf.Cos(currentAngle)) * rangedAttackInfo.radius;
            Vector3 targetPosition = hero.position + offset;

            // ����������� ����� � ������� �������
            transform.position = Vector3.Lerp(transform.position, targetPosition, _enemy.speed * Time.deltaTime);
            RotateTowards(targetPosition - transform.position); // ������������ ����� �� ���� ��������

            // �������� ���������� � �����
            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(RangedAttack());
            }
        }
    }

    private IEnumerator RangedAttack()
    {
        isAttacking = true;

        // �������������� � ������ ����� ������
        Vector3 directionToPlayer = (hero.position - transform.position).normalized;
        RotateTowardsInstantly(directionToPlayer);

        yield return new WaitForSeconds(0.5f); // ������� ��������, ����� ����������� � ������

        // ������� ������
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<RangedAttackProjectile>().Initialize(hero.position, rangedAttackInfo.damage, rangedAttackInfo.projectileSpeed, rangedAttackInfo.projectileLifetime);
        }

        // ������������� ��������� ������ ��� �����
        nextAttackTime = Time.time + attackCooldown;

        yield return new WaitForSeconds(0.5f); // ������� �������� ����� �����

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
