using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveLinear : EnemyMove
{
    private Vector3 initialPosition;
    private bool movingForward = true;
    private float extraDistance = 5f; // �������������� ��������� ����� ������� ������
    private Vector3 currentDirection;

    protected override void Start()
    {
        initialPosition = transform.position;
        base.Start();
    }

    public override void Move()
    {
        if (hero != null)
        {
            if (movingForward)
            {
                Vector3 destination = hero.position;
                Vector3 direction = (destination - initialPosition).normalized;
                currentDirection = direction; // ��������� ����������� ��������
                transform.position += direction * _enemy.speed * Time.deltaTime;

                float distanceToHero = Vector3.Distance(transform.position, hero.position);

                if (distanceToHero <= 1f) // ���� ���� �������� ������
                {
                    movingForward = false;
                }
            }
            else
            {
                transform.position += currentDirection * _enemy.speed * Time.deltaTime;

                float extraTraveled = Vector3.Distance(transform.position, initialPosition + currentDirection * extraDistance);

                if (extraTraveled >= extraDistance) // ���� ���� ������ �������������� ���������
                {
                    movingForward = true;
                    initialPosition = transform.position;
                }
            }

            // �������� ����������, �������������� ������ � ����������� ��������
            Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
            transform.rotation = targetRotation;
        }
    }
}
