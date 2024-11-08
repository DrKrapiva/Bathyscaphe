using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShellObject : MonoBehaviour
{
    [SerializeField] private GameObject prefabShellBonus;
    private float shellHP = 2;
    private float destroyDistanse = 50;
    private Transform hero;
    private void Start()
    {
        hero = GameObject.Find("Player").transform;

        StartCoroutine(CheckDistanceToHero());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BulletTouch>() != null)
        {
            TakeDamage(other.gameObject.GetComponent<BulletTouch>().Actions());
        }
        if (other.gameObject.GetComponent<Explosion>() != null)
        {
            TakeDamage(other.gameObject.GetComponent<Explosion>().Actions());
        }
    }
    private IEnumerator CheckDistanceToHero()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(2);

            float dist = Vector3.Distance(transform.position, hero.position);
            if (dist > destroyDistanse)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
    private void TakeDamage(float damage)
    {
        shellHP -= damage;
        if(shellHP <= 0)
        {
            DestroyShell();
        }
    }
    private void DestroyShell()
    {
        Vector3 spawnPosition = gameObject.transform.position;
        //spawnPosition.y = gameObject.transform.position.y + 2;

        GameObject shellBonus = Instantiate(prefabShellBonus, spawnPosition, prefabShellBonus.transform.rotation);
        
        shellBonus.GetComponent<ShellBonus>().FillInfo(ShellClasses.Instance.IndexFromListShellClasses());

        Destroy(transform.parent.gameObject);
    }
}
