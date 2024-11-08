using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
        {
            
             Debug.Log("WIN");
            MissionRescue.Instance.StageIncrease();

            ArrowPointer.Instance.StopArrowCoroutine(gameObject);
            Destroy(gameObject);
        }

    }
}
