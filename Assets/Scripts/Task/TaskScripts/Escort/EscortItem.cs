using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EscortItem : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;
    [SerializeField] private GameObject[] spawnPoints;
    private float hp = 50;
    private float moveSpeed = 3;
    private int enemyAmountMax = 1;
    private int enemyAmountMin = 1;
    private int enemySpawnTime = 3;
    private string enemyName = "EnemyForEscortItem";
    private GameObject _target;
    private GameObject enemy;
    private GameObject chosenSpawnPoint;
    public static EscortItem Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

    }
    static private EscortItem _instance;
    public void FillInfo(GameObject target)
    {
       
        _target = target;
        enemy = Resources.Load<GameObject>("Prefab/Task/" + enemyName);

        sliderHP.maxValue = hp;
        sliderHP.value = hp;

        StartSpawn();

    }
    private void Update()
    {
        Move();
    }
    public virtual void Move()
    {
        //движение преследования 
        if (_target != null)
        {
            Vector3 direction = (_target.transform.position - transform.position).normalized;
            // Adjust the y-component to avoid floating up or down
            direction.y = 0;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Rotate to face the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);

            
        }
    }
    public void TakeHit(float damage)
    {
        Debug.Log("МЕНЯ КУСАЮТ");
        hp -= damage;
        sliderHP.value = hp;

        if(hp <= 0)
        {
            Debug.Log("Потрачено!");
            moveSpeed = 0;
           // agent.speed = 0;
        }
    }
    public void StartSpawn()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPointEnemyForEscortItem");

        if (spawnPoints.Length > 0)
        {
            // Выбираем случайную точку спавна и сохраняем ее
            int randomIndex = Random.Range(0, spawnPoints.Length);
            chosenSpawnPoint = spawnPoints[randomIndex];
            // Вызываем метод для случайной точки спавна
            StartCoroutine(SpawnCoroutine());
        }
        
    }
    IEnumerator SpawnCoroutine()
    {
        for (; ; )
        {
            Spawn();
            yield return new WaitForSeconds(enemySpawnTime);
        }

    }
    public void Spawn()
    {
        int randomEnemyAmount = Random.Range(enemyAmountMin, enemyAmountMax);
        for (int i = 0; i < randomEnemyAmount; i++)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * 2.0f; // Радиус случайного смещения 2.0 единицы
            randomOffset.y = 0; // Сохраняем врагов на одном уровне по Y
            Vector3 spawnPosition = chosenSpawnPoint.transform.position + randomOffset;

            GameObject enemyClon = Instantiate(enemy, spawnPosition, Quaternion.identity);
            enemyClon.name = enemyName;
            //enemyClon.GetComponent<EnemyForEscortItem>().FillInfo(gameObject);

        }
    }
    public GameObject Target()
    {
        return gameObject;
    }
}
