using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AchievementsCounter : MonoBehaviour
{
    private void OnEnable()
    {
        // ѕодписка на событи€
        TrySubscribeToEvent("Murderer",
                        () => EnemyActions.OnEnemyDeath += EnemyDied,
                        () => EnemyActions.OnEnemyDeath -= EnemyDied,
                        checkSum: true, checkMaxOnLevel: true);

        TrySubscribeToEvent("Flash",
                            () => ShellSpeedIncrease.OnSpeedIncrease += SpeedIncreased,
                            () => ShellSpeedIncrease.OnSpeedIncrease -= SpeedIncreased,
                            checkSum: true, checkMaxOnLevel: true);

        TrySubscribeToEvent("DieHard",
                            () => ShellMaxArmor.OnMaxArmor += GetMaxArmor,
                            () => ShellMaxArmor.OnMaxArmor -= GetMaxArmor,
                            checkSum: true, checkMaxOnLevel: true);

        TrySubscribeToEvent("LordOfTorpedoes1",
                            () => ButtonShoot.OnShoot += CountShoot,
                            () => ButtonShoot.OnShoot -= CountShoot,
                            checkSum: true, checkMaxOnLevel: true);

        TrySubscribeToEvent("YouDied",
                            () => LevelController.OnHeroDied += HeroDied,
                            () => LevelController.OnHeroDied -= HeroDied,
                            checkSum: true, checkMaxOnLevel: false);

        TrySubscribeToEvent("Treasurer",
                            () => LevelController.OnCoinsCollected += CountCoin,
                            () => LevelController.OnCoinsCollected -= CountCoin,
                            checkSum: true, checkMaxOnLevel: true);

        TrySubscribeToEvent("Longevity",
                            () => LevelController.OnTimePassed += CountTime,
                            () => LevelController.OnTimePassed -= CountTime,
                            checkSum: true, checkMaxOnLevel: true);


        PanelPause.OnLeaveLevel += LeaveLevel;
    }
    private void OnDisable()
    {
        EnemyActions.OnEnemyDeath -= EnemyDied;
        ShellSpeedIncrease.OnSpeedIncrease -= SpeedIncreased;
        ShellMaxArmor.OnMaxArmor -= GetMaxArmor;
        ButtonShoot.OnShoot -= CountShoot;//провер€ем троп1 2 и взрыв, если все 3 уровн€ то не подписываемс€
        LevelController.OnHeroDied -= HeroDied;
        LevelController.OnCoinsCollected -= CountCoin;
        LevelController.OnTimePassed -= CountTime;

        PanelPause.OnLeaveLevel -= LeaveLevel;  
    }
    private void TrySubscribeToEvent(string key, Action subscribeAction, Action unsubscribeAction, bool checkSum, bool checkMaxOnLevel)
    {
        bool isMaxLevelSum = checkSum && AchievementController.Instance.CheckMaxLevel(key, true);
        bool isMaxLevelOnLevel = checkMaxOnLevel && AchievementController.Instance.CheckMaxLevel(key, false);

        // ѕодписываемс€, если хот€ бы в одном из словарей достижение не достигло максимального уровн€
        if (!isMaxLevelSum || !isMaxLevelOnLevel)
        {
            unsubscribeAction();
            subscribeAction();
            Debug.Log($"Subscribed to event for {key}");
        }
    }
    private void EnemyDied()
    {
        DictionaryAchievementProgress.Instance.UpdateAchievementProgressAdd("deathEnemy", 1);
        //Debug.Log( " EnemyDied");

    }
    private void SpeedIncreased()
    {
        DictionaryAchievementProgress.Instance.UpdateAchievementProgressAdd("acceleration", 1);
        //Debug.Log(DictionaryAchievementProgress.Instance.GetAchievementsProgress("acceleration") + " acceleration");
    }
    private void GetMaxArmor(int maxArmor)
    {
        int currentMaxArmor = DictionaryAchievementProgress.Instance.GetAchievementsProgress("maxArmor", "OnLevel");

        if(maxArmor > currentMaxArmor)
            DictionaryAchievementProgress.Instance.UpdateAchievementProgressRewrite("maxArmor", maxArmor, "OnLevel");
        
        //Debug.Log(DictionaryAchievementProgress.Instance.GetAchievementsProgress("maxArmor") + " maxArmor");
    }
    private void CountShoot(string weaponName)
    {
        //Debug.Log(weaponName);
        DictionaryAchievementProgress.Instance.UpdateAchievementProgressAdd(weaponName, 1);
        //Debug.Log(DictionaryAchievementProgress.Instance.GetAchievementsProgress(weaponName) + weaponName);
    }
    private void HeroDied()
    {
        DictionaryAchievementProgress.Instance.UpdateAchievementProgressAdd("deathHero", 1);
        //Debug.Log(DictionaryAchievementProgress.Instance.GetAchievementsProgress("deathHero") + " deathHero");

        GiveProgressParameters();
    }
    private void CountTime(int time)
    {
        DictionaryAchievementProgress.Instance.UpdateAchievementProgressAdd("time", time);
        //Debug.Log(DictionaryAchievementProgress.Instance.GetAchievementsProgress("time") + " time");
    }
    private void CountCoin(int coin)
    {
        int coins = coin + (int)(coin * DictionaryUprades.Instance.HowMuchAdd("coin")/100);    
        DictionaryAchievementProgress.Instance.UpdateAchievementProgressAdd("coin", coins);
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + coins);
        //Debug.Log(DictionaryAchievementProgress.Instance.GetAchievementsProgress("coin") + " coin");
    }
    private void LeaveLevel()
    {
        GiveProgressParameters();
    }
    private void GiveProgressParameters()
    {
        //считаем параметры дл€ списков Sum и MaxOnLevel из словар€ Progress
        DictionaryAchievementProgress.Instance.UpdateSumFromOnLevel();
        DictionaryAchievementProgress.Instance.UpdateMaxOnLevelFromOnLevel();
        //—охран€ем прогресс
        DictionaryAchievementProgress.Instance.SaveAchivProgress();

        // ѕосле сохранени€, провер€ем достижени€ на основе актуального прогресса
        AchievementController.Instance.CheckProgress(true);//провер€ем дл€ суммарных достижений

        AchievementController.Instance.CheckProgress(false);//провер€ем достижени€ на уровне
    }
}
