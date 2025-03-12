using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AchievementTitleDisplay : MonoBehaviour
{
    public Title achievementTitle;
    public TMP_Text achievementtitleText;
    public TMP_Text descriptionText;
    public TMP_Text scoreText;

    
    void Start()
    {
       achievementtitleText.text=achievementTitle.AchievementTitle;
       descriptionText.text=achievementTitle.description;
       scoreText.text= achievementTitle.score.ToString();
    }

   
}
