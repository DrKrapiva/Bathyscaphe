using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActWeapon
{
    public List<Vector3> vectors;///�������� � �������
    public int countBullet;
    public float speed;
    public float cooldawn;
    public float repulsion;
    public float attack;
    public int destroy;
    public string direction;
    public string name;
    public int maxTouchEnemy;// ��������
    public int radius;//
    public bool autoFire;
}
public class DictionaryActivWeapon : MonoBehaviour
{
    public static Dictionary<string, ActWeapon> DictActWeapon()
    {
        Dictionary<string, ActWeapon> dict = new Dictionary<string, ActWeapon>();
        dict.Add("RocketMiniRegular", new ActWeapon() { name = "������ ���������", attack = 4, countBullet = 1, speed = 20, cooldawn = 1, repulsion = 0.5f, destroy = 5, direction = "\u2191", maxTouchEnemy = 1, vectors = new List<Vector3>() { Vector3.forward }, autoFire = true });
        dict.Add("RocketMiniRare", new ActWeapon() { name = "������ ���������", attack = 6, countBullet = 1, speed = 23, cooldawn = 1, repulsion = 1.5f, destroy = 6, direction = "\u2191 \u2193", maxTouchEnemy = 2, vectors = new List<Vector3>() { Vector3.forward, Vector3.back }, autoFire = true });
        dict.Add("RocketMiniLegendary", new ActWeapon() { name = "������ ���������", attack = 8, countBullet = 1, speed = 24, cooldawn = 1, repulsion = 2f, destroy = 7, direction = "\u2191 \u2190 \u2192", maxTouchEnemy = 3, vectors = new List<Vector3>() { Vector3.forward, Vector3.right, Vector3.left }, autoFire = true });
        dict.Add("RocketMiniEpic", new ActWeapon() { name = "������ ���������", attack = 10, countBullet = 1, speed = 25, cooldawn = 1, repulsion = 2.5f, destroy = 8, direction = "\u2191 \u2193 \u2190 \u2192", maxTouchEnemy = 4, vectors = new List<Vector3>() { Vector3.forward, Vector3.right, Vector3.left, Vector3.back }, autoFire = true });

        dict.Add("RocketRegular", new ActWeapon() { name = "������ �������", attack = 15, countBullet = 1, speed = 22, cooldawn = 0.5f, repulsion = 4f, destroy = 5, direction = "����������", maxTouchEnemy = 1, autoFire = false });
        dict.Add("RocketRare", new ActWeapon() { name = "������ �������", attack = 18, countBullet = 2, speed = 23, cooldawn = 0.5f, repulsion = 5f, destroy = 6, direction = "����������", maxTouchEnemy = 1, autoFire = false });
        dict.Add("RocketLegendary", new ActWeapon() { name = "������ �������", attack = 21, countBullet = 3, speed = 24, cooldawn = 0.5f, repulsion = 6f, destroy = 7, direction = "����������", maxTouchEnemy =1, autoFire = false });
        dict.Add("RocketEpic", new ActWeapon() { name = "������ �������", attack = 24, countBullet = 4, speed = 25, cooldawn = 0.5f, repulsion = 7f, destroy = 8, direction = "����������", maxTouchEnemy =1, autoFire = false });

        dict.Add("ExplosionRegular", new ActWeapon() { name = "�����", attack = 10, countBullet = 1, speed = 0, cooldawn = 2f, repulsion = 8f, destroy = 1, direction = "������", maxTouchEnemy = 1, radius = 5, autoFire = false });
        dict.Add("ExplosionRare", new ActWeapon() { name = "�����", attack = 12, countBullet = 1, speed = 0, cooldawn = 2f, repulsion = 10f, destroy = 1, direction = "������", maxTouchEnemy = 1, radius = 7, autoFire = false });
        dict.Add("ExplosionLegendary", new ActWeapon() { name = "�����", attack = 14, countBullet = 1, speed = 0, cooldawn = 2f, repulsion = 12f, destroy = 1, direction = "������", maxTouchEnemy = 1, radius = 9, autoFire = false });
        dict.Add("ExplosionEpic", new ActWeapon() { name = "�����", attack = 16, countBullet = 1, speed = 0, cooldawn = 2f, repulsion =14f, destroy = 1, direction = "������", maxTouchEnemy = 1 , radius = 11, autoFire = false });

        dict.Add("HarpoonRegular", new ActWeapon() { name = "������", attack = 10, countBullet = 1, speed = 20, cooldawn = 1, repulsion = 8f, destroy = 2, direction = "�����", maxTouchEnemy = 1, autoFire = false });
        dict.Add("HarpoonRare", new ActWeapon() { name = "������", attack = 12, countBullet = 1, speed = 20, cooldawn = 1, repulsion = 10f, destroy = 2, direction = "�����", maxTouchEnemy = 1, autoFire = false });
        dict.Add("HarpoonLegendary", new ActWeapon() { name = "������", attack = 14, countBullet = 1, speed = 20, cooldawn = 1, repulsion = 12f, destroy = 2, direction = "�����", maxTouchEnemy = 1, autoFire = false });
        dict.Add("HarpoonEpic", new ActWeapon() { name = "������", attack = 16, countBullet = 1, speed = 20, cooldawn = 1, repulsion =14f, destroy = 2, direction = "�����", maxTouchEnemy = 1 , autoFire = false });

        dict.Add("NetRegular", new ActWeapon() { name = "�������", attack = 0, countBullet = 1, speed = 0, cooldawn = 0, repulsion = 0f, destroy = 20000, direction = "", maxTouchEnemy = 1, autoFire = false });
        dict.Add("NetRare", new ActWeapon() { name = "�������", attack = 0, countBullet = 2, speed = 0, cooldawn = 0, repulsion = 0f, destroy = 20000, direction = "", maxTouchEnemy = 1, autoFire = false });
        dict.Add("NetLegendary", new ActWeapon() { name = "�������", attack = 0, countBullet = 3, speed = 0, cooldawn = 0, repulsion = 0f, destroy = 20000, direction = "", maxTouchEnemy = 1, autoFire = false });
        dict.Add("NetEpic", new ActWeapon() { name = "�������", attack = 0, countBullet = 4, speed = 0, cooldawn = 0, repulsion =0f, destroy = 20000, direction = "", maxTouchEnemy = 1 , autoFire = false });
        return dict;
    }
    public Dictionary<string, ActWeapon> DicActWeapon = DictActWeapon();

    public ActWeapon GetActiveWeaponInfo(string keyName)
    {
        return DicActWeapon[keyName];
    }
     
}
