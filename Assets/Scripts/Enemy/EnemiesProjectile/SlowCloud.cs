using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCloud : MonoBehaviour
{
    private float slowAmount;
    private float duration;
    private bool playerInCloud = false;
    private CharacterLocomotion player;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTrigger");
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
        {
            //Debug.Log("OnTriggerEnter");
            player = other.gameObject.GetComponent<CharacterLocomotion>();
            player.StartCoroutineChangeWalkSpeed(slowAmount, duration);
            playerInCloud = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null && playerInCloud)
        {
            //Debug.Log("Exit");
            playerInCloud = false;
            player.StopCoroutineChangeWalkSpeed();
        }
    }

    public void Initialize(float slowAmount, float duration)
    {
        this.slowAmount = slowAmount;
        this.duration = duration;
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
