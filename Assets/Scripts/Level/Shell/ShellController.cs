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
        while (true) // ����������� ����
        {
            yield return new WaitForSeconds(5); 

            // ����������� ��������� ������� ������ ������ �� �������� �������
            Vector3 spawnPosition = player.transform.position + Random.onUnitSphere * spawnRadius;
            //spawnPosition.y = player.transform.position.y - 2; // ��������� ������ ������ ������ ������ ������
            spawnPosition.y = player.transform.position.y - 1f; // ��������� ������ ������ ������ ������ ������

            // �������� ������� �� ����������� �������
            Instantiate(prefabShell, spawnPosition, prefabShell.transform.rotation);
        }
    }
}
