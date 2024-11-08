using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void Start()
    {
        
    }
    //public void StartSpawnCoroutine(string name, int number, float coroutineTime, Enemy enemy)
    public void StartSpawnCoroutine(string name, int number, float coroutineTime)
    {
        //StartCoroutine(SpawnCoroutine( name,  number, coroutineTime, enemy));
        StartCoroutine(SpawnCoroutine( name,  number, coroutineTime));
    }
   //IEnumerator SpawnCoroutine(string name, int number, float coroutineTime, Enemy enemy)
    IEnumerator SpawnCoroutine(string name, int number, float coroutineTime)
    {
        for (; ; )
        {
            
            //Spawn(name, number, enemy);
            Spawn(name, number);
            yield return new WaitForSeconds(coroutineTime);
        }
        
    }
    //public void Spawn(string name, int number, Enemy enemy)
    public void Spawn(string name, int number)
    {
        for(int i = 0; i < number; i++)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefab/Enemy/" + name);

            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * 2.0f; // Радиус случайного смещения 2.0 единицы
            randomOffset.y = 0; // Сохраняем врагов на одном уровне по Y
            Vector3 spawnPosition = transform.position + randomOffset;

            GameObject enemyClon = Instantiate(prefab, spawnPosition, Quaternion.identity);
            enemyClon.name = name ;
            
            //enemyClon.GetComponent<EnemyActions>().FillInfo(enemy);
            //enemyClon.GetComponent<EnemyActions>().FillInfo();
            //enemyClon.GetComponent<EnemyMove>().FillInfo(ii);
            //ii++;
        }
    }
}
