using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour
{
    private Vector3 vectorFly;
    private float _attack;
    private ActWeapon _actWeapon;

    public float Attack { get { return _attack; } set { _attack = value; } }
    
    public void FillInfo( Vector3 vector, ActWeapon activWeapon)
    {
        //_actWeapon = ActivWeapon.Instance.GetActiveWeaponInfo(gameObject.name);
        _actWeapon = activWeapon;
        _attack = _actWeapon.attack;//
        vectorFly = vector;
        
    }
    
    public Vector3 Vector()
    {
        return vectorFly;
    }
    public float Speed()
    {
        return _actWeapon.speed;
    }

    public GameObject Target()
    {

        return GameObject.FindGameObjectWithTag("Enemy");
    }
}
