using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button startButton;  

    void Start()
    {
        
        startButton.onClick.AddListener(OnStartButtonPressed);
    }


     public void OnStartButtonPressed()
    {
        
        SceneManager.LoadScene("Dungeon");
    }
}

