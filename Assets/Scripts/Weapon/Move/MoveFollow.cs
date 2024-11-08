using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFollow : ARocketMove
{
    public override void GetInfo()
    {
        _speed = GetComponent<RocketMove>().Speed();
        target = GetComponent<RocketMove>().Target();
        _vectorFly = GetComponent<RocketMove>().Vector();
    }

    protected override void Move()
    {
        if (target != null)
        {
            // Движемся в сторону целевого объекта
            Vector3 targetDirection = target.transform.position - transform.position;
            float singleStep = _speed * Time.deltaTime;
            Vector3 newDirection = Vector3.MoveTowards(transform.forward, targetDirection, singleStep);
            transform.rotation = Quaternion.LookRotation(newDirection);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, singleStep);
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = _vectorFly * _speed;

            if (_vectorFly != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(_vectorFly);
            }
        }
    }
}
