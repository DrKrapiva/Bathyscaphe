using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccurateShellingFromAShipObject : MonoBehaviour
{
    private AccurateShellingFromAShip _accurateShellingFromAShip;
    private List<GameObject> enemies = new List<GameObject>();

    public void FillInfo(AccurateShellingFromAShip accurateShellingFromAShip)
    {
        _accurateShellingFromAShip = accurateShellingFromAShip;

        StartCoroutine(Shelling());
        Destroy(gameObject, _accurateShellingFromAShip.DestroyTime);
    }
    private void OnTriggerEnter(Collider other)

    {
        if (other.transform.parent != null && other.transform.parent.CompareTag("Enemy"))
        {
            if (!enemies.Contains(other.transform.parent.gameObject))//записывать тех кого нет в списке
                enemies.Add(other.transform.parent.gameObject);

        }
    }
    IEnumerator Shelling()
    {

        yield return new WaitForSeconds(1.5f);

        EnemiesRundomAndDamage();
    }
    private void EnemiesRundomAndDamage()
    {
        if (enemies.Count != 0)
        {
            for (int i = 0; i < _accurateShellingFromAShip.Amount; i++)
            {
                int randomEnemyIndex = Random.Range(0, enemies.Count);

                if (enemies[randomEnemyIndex] != null)
                    enemies[randomEnemyIndex].GetComponent<EnemyActions>().TakeDamage((int)_accurateShellingFromAShip.Damage);

            }
        }
    }
}
