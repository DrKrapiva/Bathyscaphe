using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetObject : MonoBehaviour
{
    private bool isCaught = false;
    private int TwoTought = 0;
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<SpecialFish>() != null)
        {
            FishCaught();


        }
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null && isCaught)
        {
            MissionNetCatch.Instance.FishCounter();
            PickUpNet();

        }
        else if  (other.gameObject.GetComponent<CharacterLocomotion>() != null )
        {
            TwoTought++;
            if(TwoTought == 2)
                PickUpNet();
            
        }
    }
    private void FishCaught()
    {
        isCaught = true;
        Debug.Log("FishCaught");

    }
    private void PickUpNet()
    {
        //Debug.Log("PickUpNet");

        MissionNetCatch.Instance.PickUpNet();
        ArrowPointer.Instance.StopArrowCoroutine(gameObject);
        Destroy(gameObject);
    }
}
