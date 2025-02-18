using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
       public Button quitButton;
    void Start()
    {
        
        quitButton.onClick.AddListener(OnQuitButtonPressed);
    }


     public void OnQuitButtonPressed()
    {
        Debug.Log("Quit button is pressed. Bye !!!");
         Application.Quit();
    }
}
