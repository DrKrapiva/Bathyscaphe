using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBonus : MonoBehaviour
{
    private int _index;
    private float speed = 30;
    private Transform hero;
    private float radiusTakeExp = 1;//   радиус сбора
    
    public void FillInfo(int index)
    {
        _index = index;
        hero = GameObject.Find("Player").transform;
    }
    public void StartCoroutineMoveToObject()// запускать из магнита
    {
        StopCoroutine(MoveToObject());
        StartCoroutine(MoveToObject());

    }
    IEnumerator MoveToObject()
    {
        while (Vector3.Distance(transform.position, hero.position) > radiusTakeExp)
        {

            transform.position = Vector3.MoveTowards(transform.position, hero.position, speed * Time.deltaTime);
            yield return null;
        }
        
    }
    private void GiveBonus()
    {
        //
        ShellClasses.Instance.GiveBonus(_index);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
        {
            //Debug.Log("касаюсь хиро");
            GiveBonus();
        }
    }
}
