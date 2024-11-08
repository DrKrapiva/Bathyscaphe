using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public string name;
    public string nameKey;
    public string description;
    public string moveDescription;
    public float hp;
    public int difficultLevel;
    public int scoresAfterDeath;
    public float speed;
    public float attackSpeed;
    public float meleeAttack;
    public float spawnInterval; // �������� ������
    public int minSpawnCount; // ����������� ���������� ������ �� ���
    public int maxSpawnCount; // ������������ ���������� ������ �� ���
    //public float rangedAttack;
}
public class EnemyDictionary : MonoBehaviour
{
    public static Dictionary<string, Enemy> DictEnemy()
    {
        Dictionary<string, Enemy> DictEnemy = new Dictionary<string, Enemy>();
        DictEnemy.Add("ordinaryFish", new Enemy() 
        {
            nameKey = "ordinaryFish",
            name = "���� �������", 
            description = "������ ����������",
            moveDescription = "�������� ����� �� ����",
            meleeAttack = 5,
            hp = 6,
            difficultLevel = 1,
            speed = 3,
            attackSpeed = 0.2f,
            spawnInterval = 6f,
            minSpawnCount = 1,
            maxSpawnCount = 3 });
        DictEnemy.Add("ordinaryFishBoss", new Enemy()
        { nameKey = "ordinaryFishBoss",
            name = "���� ������� ����",
            description = "������ ����������, �� ����",
            moveDescription = "�������� ����� �� ����",
            meleeAttack = 15,
            hp = 16,
            difficultLevel = 2,
            speed = 15,
            attackSpeed = 0.2f,
            spawnInterval = 12f,
            minSpawnCount = 1,
            maxSpawnCount = 1 });
        DictEnemy.Add("seaEel", new Enemy() 
        { nameKey = "seaEel",
            name = "������� �����",
            description = "���� ����� �� ���������� 3 ",
            moveDescription = "������ ������ ����",
            meleeAttack = 7.5f,
            hp = 20,
            difficultLevel = 1,
            speed = 1,
            attackSpeed = 0.2f,
            spawnInterval = 13f,
            minSpawnCount = 1,
            maxSpawnCount = 2 });
        DictEnemy.Add("seaEelBoss", new Enemy() 
        { nameKey = "seaEelBoss",
            name = "������� �����",
            description = "���� ����� �� ���������� 6 ",
            moveDescription = "������ ������ ����",
            meleeAttack = 15f, 
            hp = 30, 
            difficultLevel = 2, 
            speed = 2,
            attackSpeed = 0.2f, 
            spawnInterval = 22f,
            minSpawnCount = 1,
            maxSpawnCount = 1 });
        DictEnemy.Add("octopus", new Enemy() 
        { nameKey = "octopus",
            name = "��������",
            description = "���������� ����� ����, ��������� �������, ������� ��������� ���� ",
            moveDescription = "����� ����� �������� �� ����",
            meleeAttack = 15f,
            hp = 40,
            difficultLevel = 1,
            speed = 6,
            attackSpeed = 0.2f,
            spawnInterval = 9f,
            minSpawnCount = 1,
            maxSpawnCount = 1 });
        DictEnemy.Add("schoolingFish", new Enemy() 
        { nameKey = "schoolingFish",
            name = "���� �������",
            description = "������, �������� ������", 
            moveDescription = "������ ����� ����",
            meleeAttack = 3f,
            hp = 8,
            difficultLevel = 1, 
            speed = 10,
            attackSpeed = 0.2f, 
            spawnInterval = 8f, 
            minSpawnCount = 5,
            maxSpawnCount = 10 });
        DictEnemy.Add("coral", new Enemy() 
        { nameKey = "coral",
            name = "������", 
            description = "����� �� �����, ���� ��������� ������� ����",
            moveDescription = "�� ��������",
            meleeAttack = 10f, 
            hp = 18, 
            difficultLevel = 1,
            speed = 0,
            attackSpeed = 0.2f,
            spawnInterval = 11f, 
            minSpawnCount = 1,
            maxSpawnCount = 3 });
        DictEnemy.Add("shark", new Enemy() 
        { nameKey = "shark",
            name = "�����", 
            description = "������ ����",
            moveDescription = "����� � ����", 
            meleeAttack = 45f,
            hp = 240,
            difficultLevel = 3,
            speed = 8, 
            attackSpeed = 5.0f,
            spawnInterval = 40f,
            minSpawnCount = 1,
            maxSpawnCount = 1 });
        DictEnemy.Add("EnemyForEscortItem", new Enemy() 
        { nameKey = "EnemyForEscortItem",
            name = "���� �������������", 
            description = "���������� ����� �������������", 
            moveDescription = "����� � �����", 
            meleeAttack = 5f, 
            hp = 20,
            difficultLevel = 1,
            speed = 8, 
            attackSpeed = 0.2f, 
            spawnInterval = 6f, 
            minSpawnCount = 1,
            maxSpawnCount = 1 });
        return DictEnemy;
    }
    public Dictionary<string, Enemy> DicEnemy = DictEnemy();

    private List<int> pointExp = new List<int>() { 1, 3, 7 };
    
    protected int GetExp(int i)
    {
       
        return pointExp[i - 1];
    }
}
