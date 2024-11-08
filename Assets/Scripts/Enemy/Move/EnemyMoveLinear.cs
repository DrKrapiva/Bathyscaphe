using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveLinear : EnemyMove
{
    private Vector3 initialPosition;
    private bool movingForward = true;
    private float extraDistance = 5f; // Дополнительная дистанция после касания игрока
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
                currentDirection = direction; // Сохраняем направление движения
                transform.position += direction * _enemy.speed * Time.deltaTime;

                float distanceToHero = Vector3.Distance(transform.position, hero.position);

                if (distanceToHero <= 1f) // Если враг коснулся игрока
                {
                    movingForward = false;
                }
            }
            else
            {
                transform.position += currentDirection * _enemy.speed * Time.deltaTime;

                float extraTraveled = Vector3.Distance(transform.position, initialPosition + currentDirection * extraDistance);

                if (extraTraveled >= extraDistance) // Если враг прошел дополнительную дистанцию
                {
                    movingForward = true;
                    initialPosition = transform.position;
                }
            }

            // Получаем кватернион, поворачивающий объект в направлении движения
            Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
            transform.rotation = targetRotation;
        }
    }
}
