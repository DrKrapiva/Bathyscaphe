using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudyWaterObject : MonoBehaviour
{
    private CloudyWater _cloudyWater;
    public void FillInfo(CloudyWater cloudyWater)
    {
        _cloudyWater = cloudyWater;


        Destroy(gameObject, _cloudyWater.Duration);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null &&  other.transform.parent.CompareTag("Enemy"))
        {
            other.transform.parent.gameObject.GetComponent<EnemyMove>().StartChangeSpeed(_cloudyWater.SlowingDownEnemies, _cloudyWater.Duration);
        }
    }
}
