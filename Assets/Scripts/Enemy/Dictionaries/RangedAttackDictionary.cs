using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack
{
    public float radius;
    public float attackCooldown;
    public float damage;
    public float projectileSpeed;
    public float projectileLifetime;
}

public class RangedAttackDictionary : MonoBehaviour
{
    public static Dictionary<string, RangedAttack> DictRangedAttack()
    {
        Dictionary<string, RangedAttack> DictRangedAttack = new Dictionary<string, RangedAttack>();
        DictRangedAttack.Add("seaEel", new RangedAttack() { radius = 15, attackCooldown = 3.0f, damage = 5f, projectileSpeed = 10f, projectileLifetime = 5f });
        DictRangedAttack.Add("seaEelBoss", new RangedAttack() { radius = 15, attackCooldown = 2.0f, damage = 10f, projectileSpeed = 10f, projectileLifetime = 5f });
        return DictRangedAttack;
    }

    public Dictionary<string, RangedAttack> DicRangedAttack = DictRangedAttack();
}
