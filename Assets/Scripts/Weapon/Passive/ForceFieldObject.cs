using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldObject : MonoBehaviour
{
    private ForceField _forceField;
    private bool isLive = true;
    private float hp;
    public void FillInfo(ForceField forceField)
    {
        _forceField = forceField;
        hp = _forceField.Hp;

        transform.localScale = new Vector3(_forceField.Size, _forceField.Size, _forceField.Size);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent != null && other.transform.parent.CompareTag("Enemy") && isLive)
        {
            //написать импульс
            Rigidbody enemyRigidbody = other.transform.parent.GetComponent<Rigidbody>();

            if (enemyRigidbody != null)
            {
                PushEnemy(enemyRigidbody);
                other.transform.parent.GetComponent<EnemyMove>().StartCorutineGetImpulseMove(_forceField.EnemyRepulsionForceTimer);
                HPController();
            }
        }
    }

    private void PushEnemy(Rigidbody enemyRigidbody)
    {
        // Задайте направление отталкивания (например, от игрока).
        Vector3 pushDirection = (transform.position - enemyRigidbody.transform.position).normalized;

        //  сила отталкивания из класса
        float pushForce = _forceField.EnemyRepulsionForce;

        // Примените силу к Rigidbody врага.
        enemyRigidbody.AddForce(-pushDirection * pushForce, ForceMode.Impulse);
    }

    private void HPController()
    {
        hp -= 1;

        if(hp <= 0)
        {
            isLive = false;
            Destroy();
        }
        //Debug.Log(hp);
    }

    private void Destroy()
    {
        //запустить заново корутину из ForceFieldController
        ForceFieldController.Instance.StartCoroutineGenerateForceField();
        //уничтожиться
        Destroy(gameObject);
    }
}
