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
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[11]);
        AudioManager.instance.StopMusic();
        Destroy(AudioManager.instance);
        SceneManager.LoadScene("Dungeon");
    }
}

