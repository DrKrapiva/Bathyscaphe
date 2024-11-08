using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight : ARocketMove
{
    public override void GetInfo()
    {
        _vectorFly = GetComponent<RocketMove>().Vector();
        _speed = GetComponent<RocketMove>().Speed();
    }

    protected override void Move()
    {
        
        // Движение по прямой линии
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = _vectorFly * _speed;

        // Поворот ракеты в направлении движения
        if (_vectorFly != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_vectorFly);
        }
    }

}
