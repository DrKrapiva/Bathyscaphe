using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAssistantsObject : MonoBehaviour
{
    private DroneAssistants _droneAssistants;
    private GameObject player;
    private GameObject currentTarget = null;
    private List<GameObject> allDrones;
    private Vector3 velocity = Vector3.zero; // —корость изменени€ позиции дл€ SmoothDamp
    private float stopDistanceToEnemy = 4.0f;
    private float minimumDistanceBetweenDrones = 2f; // ћинимально допустимое рассто€ние между дронами
    private float smoothTime = 0.3f; // ¬рем€ замедлени€ дл€ SmoothDamp
    private int maxDistanceToHero = 15;
    

    [SerializeField] private Transform drone;
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject prefabBullet;
    public void FillInfo(DroneAssistants droneAssistants, List<GameObject> drones)
    {
        _droneAssistants = droneAssistants;
        allDrones = drones;

        player = GameObject.Find("Player");
        
        StartCoroutineAttack();
    }
    void Update()
    {

        // ѕоиск и захват врага
        if (currentTarget == null || currentTarget.Equals(null))
        {
            // ѕровер€ем рассто€ние до игрока
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // ≈сли игрок находитс€ ближе maxDistanceToHero, ищем врага
            if (distanceToPlayer <= maxDistanceToHero)
            {
                GameObject newTarget = FindClosestEnemyInRange(player.transform.position, _droneAssistants.SizeZone);
                // ќбновл€ем текущую цель только если нова€ цель найдена
                if (newTarget != null)
                {
                    currentTarget = newTarget;
                }
            }
            else
            {
                // ≈сли игрок далеко, следуем за игроком
                currentTarget = null;
                FollowPlayer();
                return; // ¬ыходим из Update, чтобы избежать повторной обработки следующих условий
            }
        }
        // ѕродолжаем автоприцеливание, если у нас есть цель
        if (currentTarget != null && !currentTarget.Equals(null))
        {
            AutoAim(currentTarget);
            
        }
        else
        {
            // ≈сли нет цели, следуем за игроком
            FollowPlayer();
        }
        
        
    }
    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position + new Vector3(0, 4, 0);// было не 4 а 2
            Vector3 targetPosition = playerPosition;

            //не даю дронам сближатьс€ друг с другом
            foreach (GameObject drone in allDrones)
            {
                if (drone != this.gameObject) // ѕропускаем себ€
                {
                    float distance = Vector3.Distance(transform.position, drone.transform.position);
                    if (distance < minimumDistanceBetweenDrones)
                    {
                        Vector3 directionAway = transform.position - drone.transform.position;
                        targetPosition += directionAway.normalized * (minimumDistanceBetweenDrones - distance);
                    }
                }
            }

            // ѕлавно перемещаем дрон к целевой позиции с использованием SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, _droneAssistants.Speed);

            //поворачиваю дрон в сторону игрока
            if ((targetPosition - transform.position).sqrMagnitude > 0.01f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _droneAssistants.RotationSpeed);
            }
        }
    }
    
    GameObject FindClosestEnemyInRange(Vector3 center, float range)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, range);
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {

            EnemyActions enemy = hitCollider.GetComponentInParent<EnemyActions>();
            //EnemyActions enemy = hitCollider.GetComponent<EnemyActions>();
            if (enemy != null)
            {
                float distance = (hitCollider.transform.position - center).sqrMagnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hitCollider.gameObject;
                }
            }
        }

        return closestEnemy;
    }
    private void AutoAim(GameObject enemy)
    {
        //Debug.Log("aouto aim " + enemy.transform.parent.name);
        if (enemy != null)
        {
            // –ассчитываем направление к врагу
            Vector3 directionToEnemy = enemy.transform.position - transform.position;

            // ѕоворачиваем дрона к врагу
            if (directionToEnemy != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _droneAssistants.RotationSpeed);
            }

            // ѕриближаемс€ к врагу, но останавливаемс€ на stopDistance
            // ¬ычисл€ем целевую позицию с учетом stopDistance
            float distanceToEnemy = directionToEnemy.magnitude;
            Vector3 targetPosition = enemy.transform.position - directionToEnemy.normalized * stopDistanceToEnemy;
            targetPosition.y = Mathf.Max(targetPosition.y, 1.0f);

            //  орректировка позиции, чтобы поддерживать минимальное рассто€ние между дронами
            foreach (GameObject drone in allDrones)
            {
                if (drone != this.gameObject) // ѕропускаем себ€
                {
                    float distance = Vector3.Distance(transform.position, drone.transform.position);
                    if (distance < minimumDistanceBetweenDrones)
                    {
                        Vector3 directionAway = transform.position - drone.transform.position;
                        targetPosition += directionAway.normalized * (minimumDistanceBetweenDrones - distance);
                    }
                }
            }
            // ≈сли дистанци€ до врага больше чем stopDistance, приближаемс€
            if (distanceToEnemy > stopDistanceToEnemy)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _droneAssistants.Speed * Time.deltaTime);
            }
        }
    }
    private void StartCoroutineAttack()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        //Debug.Log("attack");
        for (; ; )
        {
            if (currentTarget != null)
            {
                Vector3 flyBullet = (gun.position - drone.position).normalized;

                GameObject bulletClone = Instantiate(prefabBullet, gun.transform.position, Quaternion.identity);

                bulletClone.GetComponent<DroneAssistantsBullet>().FillInfo(flyBullet, _droneAssistants, (int)stopDistanceToEnemy);

            }
            yield return new WaitForSeconds(_droneAssistants.CoolDown);
        }
    }
}
