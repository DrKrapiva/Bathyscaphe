using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointExp : MonoBehaviour
{
    private int _scoresAfterDeath;
    private float speed = 30;
    private Transform hero;
    private float radiusTakeExp = 1.3f;//   ������ �����
    private void Start()
    {
        hero = GameObject.Find("Player").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Triggered with: " + other.name);
        if (other.gameObject.GetComponentInParent<CharacterLocomotion>() != null)
        {
            //Debug.Log("Experience added");
            AddPointExp();
        }
        if(other.gameObject.GetComponent<EnemyActions>() != null)
        {
            Debug.Log("Enemy Touched");
        }
        // ���� ���� �������� �����, ���� �� ������ ������ �������� �����
        /*if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Collider experienceCollider = GetComponent<Collider>();
            if (experienceCollider != null)
            {
                Physics.IgnoreCollision(experienceCollider, other, true);
                Debug.Log("Ignoring collision with: " + other.name);
            }
            else
            {
                Debug.LogWarning("No Collider attached to the Experience object.");
            }
        }*/
    }
    public void StartCoroutineMoveToObjectAndDestroy()// ��������� �� �������
    {
        StopCoroutine(MoveToObjectAndDestroy());
        StartCoroutine(MoveToObjectAndDestroy());
        
    }
    IEnumerator MoveToObjectAndDestroy()
    {
        while (Vector3.Distance(transform.position, hero.position) > radiusTakeExp)
        {
           
            transform.position = Vector3.MoveTowards(transform.position, hero.position, speed * Time.deltaTime);
            yield return null; 
        }
        //AddPointExp();

    }
    public void FillInfo(int scoresAfterDeath)
    {
        _scoresAfterDeath = scoresAfterDeath;
    }
    private void AddPointExp()
    {
        LevelController.Instance.CountPointExp(_scoresAfterDeath);
        Destroy(gameObject);
    }

}
