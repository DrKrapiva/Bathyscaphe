using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAssistantsObject : MonoBehaviour
{
    private DroneAssistants _droneAssistants;
    private GameObject player;
    private GameObject currentTarget = null;
    private List<GameObject> allDrones;
    private Vector3 velocity = Vector3.zero; // �������� ��������� ������� ��� SmoothDamp
    private float stopDistanceToEnemy = 4.0f;
    private float minimumDistanceBetweenDrones = 2f; // ���������� ���������� ���������� ����� �������
    private float smoothTime = 0.3f; // ����� ���������� ��� SmoothDamp
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

        // ����� � ������ �����
        if (currentTarget == null || currentTarget.Equals(null))
        {
            // ��������� ���������� �� ������
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // ���� ����� ��������� ����� maxDistanceToHero, ���� �����
            if (distanceToPlayer <= maxDistanceToHero)
            {
                GameObject newTarget = FindClosestEnemyInRange(player.transform.position, _droneAssistants.SizeZone);
                // ��������� ������� ���� ������ ���� ����� ���� �������
                if (newTarget != null)
                {
                    currentTarget = newTarget;
                }
            }
            else
            {
                // ���� ����� ������, ������� �� �������
                currentTarget = null;
                FollowPlayer();
                return; // ������� �� Update, ����� �������� ��������� ��������� ��������� �������
            }
        }
        // ���������� ����������������, ���� � ��� ���� ����
        if (currentTarget != null && !currentTarget.Equals(null))
        {
            AutoAim(currentTarget);
            
        }
        else
        {
            // ���� ��� ����, ������� �� �������
            FollowPlayer();
        }
        
        
    }
    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position + new Vector3(0, 4, 0);// ���� �� 4 � 2
            Vector3 targetPosition = playerPosition;

            //�� ��� ������ ���������� ���� � ������
            foreach (GameObject drone in allDrones)
            {
                if (drone != this.gameObject) // ���������� ����
                {
                    float distance = Vector3.Distance(transform.position, drone.transform.position);
                    if (distance < minimumDistanceBetweenDrones)
                    {
                        Vector3 directionAway = transform.position - drone.transform.position;
                        targetPosition += directionAway.normalized * (minimumDistanceBetweenDrones - distance);
                    }
                }
            }

            // ������ ���������� ���� � ������� ������� � �������������� SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, _droneAssistants.Speed);

            //����������� ���� � ������� ������
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
            // ������������ ����������� � �����
            Vector3 directionToEnemy = enemy.transform.position - transform.position;

            // ������������ ����� � �����
            if (directionToEnemy != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _droneAssistants.RotationSpeed);
            }

            // ������������ � �����, �� ��������������� �� stopDistance
            // ��������� ������� ������� � ������ stopDistance
            float distanceToEnemy = directionToEnemy.magnitude;
            Vector3 targetPosition = enemy.transform.position - directionToEnemy.normalized * stopDistanceToEnemy;
            targetPosition.y = Mathf.Max(targetPosition.y, 1.0f);

            // ������������� �������, ����� ������������ ����������� ���������� ����� �������
            foreach (GameObject drone in allDrones)
            {
                if (drone != this.gameObject) // ���������� ����
                {
                    float distance = Vector3.Distance(transform.position, drone.transform.position);
                    if (distance < minimumDistanceBetweenDrones)
                    {
                        Vector3 directionAway = transform.position - drone.transform.position;
                        targetPosition += directionAway.normalized * (minimumDistanceBetweenDrones - distance);
                    }
                }
            }
            // ���� ��������� �� ����� ������ ��� stopDistance, ������������
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
