using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveWeaponActions : MonoBehaviour
{
    protected ActWeapon actWeapon;
    private int touchEnemy;
    public virtual float Actions()
    {
        CountTouchEnemy();
        return actWeapon.attack;
    }
    
    public void FillInfo()
    {
        actWeapon = ActivWeapon.Instance.GetActiveWeaponInfo(transform.parent.gameObject.name);
        DestroyBullet(actWeapon.destroy);
    }
    private void DestroyBullet(int destroy)
    {
        Destroy(transform.parent.gameObject, destroy);
    }
    public void CountTouchEnemy()//�������� ��� ������� �����, ��� �������� ����� �������� ������ ����� ������ ���� ���� ������ ��� ��������
    {
        touchEnemy++;
        if(touchEnemy >= actWeapon.maxTouchEnemy)
        {
            DestroyBullet(0);
        }
    }
}
