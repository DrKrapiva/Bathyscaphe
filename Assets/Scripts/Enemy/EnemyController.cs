using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : EnemyDictionary
{
    public static EnemyController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    static private EnemyController _instance;

    public Enemy EnemyInfo(string keyName)
    {
        /* Enemy enemy = DicEnemy[keyName];
         enemy.scoresAfterDeath = GetExp(enemy.difficultLevel);
         return enemy;*/
        Enemy originalEnemy = DicEnemy[keyName];
        Enemy copyEnemy = new Enemy(); // Создаем новый экземпляр
                                       // Теперь копируем данные из originalEnemy в copyEnemy
                                       // ...
       
        copyEnemy.scoresAfterDeath = GetExp(originalEnemy.difficultLevel);
        copyEnemy.nameKey = originalEnemy.nameKey;
        copyEnemy.name = originalEnemy.name;
        copyEnemy.description = originalEnemy.description;
        copyEnemy.moveDescription = originalEnemy.moveDescription;
        copyEnemy.meleeAttack = originalEnemy.meleeAttack;
        copyEnemy.hp = originalEnemy.hp;
        copyEnemy.difficultLevel = originalEnemy.difficultLevel;
        copyEnemy.speed = originalEnemy.speed;
        copyEnemy.attackSpeed = originalEnemy.attackSpeed;

        return copyEnemy;

    }


}
