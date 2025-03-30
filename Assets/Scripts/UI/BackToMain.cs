using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMain : MonoBehaviour
{
    public Button BackButton;  

    void Start()
    {
        BackButton.onClick.AddListener(OnBackButtonPressed);
    }

    public void OnBackButtonPressed()
    {
        if (GameManager.instance)
        {
            AudioManager.instance.StopMusic();
            Destroy(AudioManager.instance);
        }

        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[12]);
        SceneManager.LoadScene("Main");
    }
}
