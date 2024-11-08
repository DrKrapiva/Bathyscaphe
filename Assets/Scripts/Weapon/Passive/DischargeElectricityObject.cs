using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DischargeElectricityObject : MonoBehaviour
{
    private DischargeElectricity _dischargeElectricity;
    public void FillInfo(DischargeElectricity dischargeElectricity)
    {
        _dischargeElectricity = dischargeElectricity;


        Destroy(gameObject, _dischargeElectricity.Duration);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.CompareTag("Enemy"))
        {
            other.transform.parent.gameObject.GetComponent<EnemyActions>().StartCoroutineFrozenEffect(_dischargeElectricity.Duration, _dischargeElectricity.SlowingDownEnemiesSpeed);
        }
    }
    
}
