using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
        {
            Debug.Log("касаюсь хиро");

            MissionRescue.Instance.CreateTargetPointAndArrow();
            ArrowPointer.Instance.StopArrowCoroutine(gameObject);

            Destroy(gameObject);
        }
    }
}
