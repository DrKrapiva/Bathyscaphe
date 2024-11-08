using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareResource : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
        {
            Debug.Log("касаюсь хиро");

            MissionRareResource.Instance.ItemCounter();
            ArrowPointer.Instance.StopArrowCoroutine(gameObject);

            Destroy(gameObject);
        }
    }


}
