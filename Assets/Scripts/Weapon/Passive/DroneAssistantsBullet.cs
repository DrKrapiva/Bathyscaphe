using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAssistantsBullet : MonoBehaviour
{
    private DroneAssistants _droneAssistants;
    public void FillInfo(Vector3 vectorFly, DroneAssistants droneAssistants, int distance)
    {
        _droneAssistants = droneAssistants;

        
        //двигаться
        GetComponent<Rigidbody>().velocity = vectorFly * _droneAssistants.BulletSpeed;
        // Поворот ракеты в направлении движения
        if (vectorFly != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(vectorFly);
        }

        Destroy(gameObject, distance + 2);
    }
    //если касается врага - урон, дестрой
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent != null &&  other.transform.parent.GetComponent<EnemyActions>() != null )
        {
            other.transform.parent.GetComponent<EnemyActions>().TakeDamage((int)_droneAssistants.Damage);
            Debug.Log("пуля дрона касается врага");
            Destroy(gameObject);
        }
    }
    
}
