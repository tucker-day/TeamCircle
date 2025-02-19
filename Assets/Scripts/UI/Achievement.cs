using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Achievement : MonoBehaviour
{
      public Button AchievementButton;
    void Start()
    {
        
        AchievementButton.onClick.AddListener(OnAchievementButtonPressed);
    }


     public void OnAchievementButtonPressed()
    {
        
        SceneManager.LoadScene("Achievement");
    }
} 

