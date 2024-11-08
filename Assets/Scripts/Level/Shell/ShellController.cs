using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    [SerializeField] private GameObject prefabShell;
    private GameObject player;
    private float spawnRadius = 50;
    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutineCreateShell();
    }
    private void StartCoroutineCreateShell()
    {
        StartCoroutine(CreateShell());
    }
    IEnumerator CreateShell()
    {
        while (true) // Бесконечный цикл
        {
            yield return new WaitForSeconds(5); 

            // Определение случайной позиции вокруг игрока на заданном радиусе
            Vector3 spawnPosition = player.transform.position + Random.onUnitSphere * spawnRadius;
            //spawnPosition.y = player.transform.position.y - 2; // Установка высоты спавна равной высоте игрока
            spawnPosition.y = player.transform.position.y - 1f; // Установка высоты спавна равной высоте игрока

            // Создание ракушки на вычисленной позиции
            Instantiate(prefabShell, spawnPosition, prefabShell.transform.rotation);
        }
    }
}
