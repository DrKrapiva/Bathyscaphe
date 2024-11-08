using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARocketMove : MonoBehaviour
{
    protected float _speed;
    protected GameObject target;
    protected Vector3 _vectorFly;
    void Start()
    {
        GetInfo();
    }


    void Update()
    {
        Move();
    }
    public virtual void GetInfo()
    {
        _speed = GetComponent<RocketMove>().Speed();
        target = GetComponent<RocketMove>().Target();
        _vectorFly = GetComponent<RocketMove>().Vector();
    }
    protected abstract void Move();
    
}
