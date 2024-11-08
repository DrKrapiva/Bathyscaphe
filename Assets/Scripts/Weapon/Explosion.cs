using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : ActiveWeaponActions
{
    private void Start()
    {
        FillInfo();
    }
    public override float Actions()
    {
        return actWeapon.attack;
    }
}
