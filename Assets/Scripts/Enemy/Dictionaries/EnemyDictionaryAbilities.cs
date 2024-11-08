using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilities
{
    public string abilityName;
    public float attackDistance;
    public float retreatDistance;
    public float attackCooldown;
    public float slowAmount;
    public float slowDuration;
}

public class EnemyDictionaryAbilities : MonoBehaviour
{
    public static Dictionary<string, EnemyAbilities> DictEnemyAbilities()
    {
        Dictionary<string, EnemyAbilities> DictEnemyAbilities = new Dictionary<string, EnemyAbilities>();
        DictEnemyAbilities.Add("octopus", new EnemyAbilities()
        {
            abilityName = "heroSlowdown",
            attackDistance = 5.0f,
            retreatDistance = 20.0f,
            attackCooldown = 1.0f,
            slowAmount = 0.5f,
            slowDuration = 5f
        });
        // Добавьте другие способности врагов здесь
        return DictEnemyAbilities;
    }

    public Dictionary<string, EnemyAbilities> DicEnemyAbilities = DictEnemyAbilities();
}
