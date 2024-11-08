using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAssistantsBullet : MonoBehaviour
{
    private DroneAssistants _droneAssistants;
    public void FillInfo(Vector3 vectorFly, DroneAssistants droneAssistants, int distance)
    {
        _droneAssistants = droneAssistants;

        
        //���������
        GetComponent<Rigidbody>().velocity = vectorFly * _droneAssistants.BulletSpeed;
        // ������� ������ � ����������� ��������
        if (vectorFly != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(vectorFly);
        }

        Destroy(gameObject, distance + 2);
    }
    //���� �������� ����� - ����, �������
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent != null &&  other.transform.parent.GetComponent<EnemyActions>() != null )
        {
            other.transform.parent.GetComponent<EnemyActions>().TakeDamage((int)_droneAssistants.Damage);
            Debug.Log("���� ����� �������� �����");
            Destroy(gameObject);
        }
    }
    
}
