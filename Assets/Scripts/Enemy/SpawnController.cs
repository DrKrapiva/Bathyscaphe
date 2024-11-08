using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;

    private float spawnTimer = 0f;
    private int currentDifficulty = 1;
    private int maxDifficulty = 5;

    
    void Start()
    {
        //StartSpawn("ordinaryFish", 2, 3);
        //StartSpawn("coral", 2, 3);
        //StartSpawn("seaEel",1 , 13);
        //StartSpawn("shark", 1 , 40);
        //StartSpawn("octopus", 1, 15);
        //StartSpawn("schoolingFish", 10, 3);//нужно получать им€, сколько энеми, врем€ спавна наверное из левел контроллера


        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        
        while (true)
        {
            StartSpawn("ordinaryFish", 3, 3);
            yield return new WaitForSeconds(25);
            StartSpawn("seaEel", 1, 35);
            yield return new WaitForSeconds(15);
            StartSpawn("octopus", 1, 50);
            yield return new WaitForSeconds(20);
            StartSpawn("schoolingFish", 6, 25);
            yield return new WaitForSeconds(25);
            StartSpawn("coral", 2, 17);
            yield return new WaitForSeconds(40);
            StartSpawn("shark", 1, 340);
            yield return new WaitForSeconds(35);
        }
    }
    public void StartSpawn(string keyName, int numberOfEnemy, int spawnTime)
    {

        Enemy enemyToSpawn = EnemyController.Instance.EnemyInfo(keyName);
        ///start coroutine
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        if (spawnPoints.Length > 0)
        {
            // ѕолучаем случайный индекс дл€ массива spawnPoints
            int randomIndex = Random.Range(0, spawnPoints.Length);
            // ¬ызываем метод дл€ случайной точки спавна
            spawnPoints[randomIndex].GetComponent<SpawnPoint>().StartSpawnCoroutine(keyName, numberOfEnemy, spawnTime);
        }
        /*for (int i = 0; i < spawnPoints.Length; i++)
        {
            //Debug.Log(spawnPoints[i]);
            spawnPoints[i].GetComponent<SpawnPoint>().StartSpawnCoroutine(keyName, numberOfEnemy, spawnTime);
        }*/
    }

}
