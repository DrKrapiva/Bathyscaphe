using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepSeaBombsFromAShipObject : MonoBehaviour
{
    private DeepSeaBombFromAShip _deepSeaBombFromAShip;
    private List<GameObject> enemies = new List<GameObject>();
    private Coroutine coroutine;
    public void FillInfo(DeepSeaBombFromAShip deepSeaBombFromAShip)
    {
        _deepSeaBombFromAShip = deepSeaBombFromAShip;

        transform.localScale = new Vector3(_deepSeaBombFromAShip.Size, _deepSeaBombFromAShip.Size, _deepSeaBombFromAShip.Size);

        Die();
    }
    private void OnTriggerEnter(Collider other)

    {
        if (other.transform.parent != null && other.transform.parent.CompareTag("Enemy"))
        {
            //Debug.Log(other.transform.parent.name + " Enter");
            other.transform.parent.gameObject.GetComponent<EnemyActions>().CheckIsTakeDamage((int)_deepSeaBombFromAShip.Damage, (int)_deepSeaBombFromAShip.DamageSpeed, true);
            enemies.Add(other.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.CompareTag("Enemy"))
        {
            //Debug.Log(other.transform.parent.name + " Exit");
            other.transform.parent.gameObject.GetComponent<EnemyActions>().CheckIsTakeDamage((int)_deepSeaBombFromAShip.Damage, (int)_deepSeaBombFromAShip.DamageSpeed, false);
        }
    }
    IEnumerator DieWait()
    {
        yield return new WaitForSeconds(_deepSeaBombFromAShip.Duration);

        if (enemies.Count > 0)
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.GetComponent<EnemyActions>().CheckIsTakeDamage((int)_deepSeaBombFromAShip.Damage, (int)_deepSeaBombFromAShip.DamageSpeed, false);
                    Debug.Log(enemy.name + " Exit");
                }
            }
            enemies.Clear();
        }

        Destroy(gameObject);
    }
    private void Die()
    {
        coroutine = StartCoroutine(DieWait());
        //Destroy(gameObject, _deepSeaBombFromAShip.Duration);
    }
}
