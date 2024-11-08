using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBaseClass
{
    void Action();
}
public class A: IBaseClass
{
    public void Action()
    {
        //Debug.Log("A");
    }
}
public class B: IBaseClass
{
    public void Action()
    {
        //Debug.Log("B");
    }
}
public class Test : MonoBehaviour
{

    List<IBaseClass> listAB = new List<IBaseClass>();

    /*private void Start()
    {
        listAB.Add(new A());
        listAB.Add(new B());
        int i = Random.Range(0, listAB.Count);
        // Вызываем метод, который определен в интерфейсе IBaseClass
        listAB[i].Action();
       //listAB[1].Action();
    }*/
    public void AddCoin()
    {
        LevelController.Instance.CountCoins(10);
        //HeroHPController.Instance.HealHp(1);
        //HeroHPController.Instance.ChangeMaxHp(5);
        //HeroHPController.Instance.HealArmor(1);
        //HeroHPController.Instance.ChangeMaxArmor(2);
        //CharacterLocomotion.Instance.StartCoroutineChangeWalkSpeed(0.5f, 5);
    }
    public void CheckMaxLevel()
    {
        //Debug.Log( AchievementController.Instance.CheckMaxLevel("time"));
       // Debug.Log(AchievementController.Instance.ValueForCheck("time", true));
        //Debug.Log(AchievementController.Instance.ValueForCheck("time", false));
    }
}


