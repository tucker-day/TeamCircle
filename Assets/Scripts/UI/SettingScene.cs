using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingScene : MonoBehaviour
{
   public Button settingButton;  

    void Start()
    {
        
        settingButton.onClick.AddListener(OnSettingButtonPressed);
    }


     public void OnSettingButtonPressed()
    {
        
        SceneManager.LoadScene("Setting");
    }
}
