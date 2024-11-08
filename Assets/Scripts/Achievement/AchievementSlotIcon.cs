using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlotIcon : MonoBehaviour
{
    [SerializeField] private Image progressCircle; 

    
    public void SetupAchievementIcon(string achievementKey, int level, int maxLevel)
    {

        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Prefab/AchievementPic/{achievementKey}");

        
        float fillAmount = level / maxLevel; 
        progressCircle.fillAmount = fillAmount;
    }
}
