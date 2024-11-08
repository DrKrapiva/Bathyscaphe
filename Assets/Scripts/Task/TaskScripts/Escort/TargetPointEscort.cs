using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointEscort : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EscortItem>() != null)
        {

            Debug.Log("WIN");

        }

    }
}
