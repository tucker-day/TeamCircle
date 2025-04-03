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

    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main");
    }

    public void OnBackButtonPressed()
    {
        if (GameManager.instance)
        {
            AudioManager.instance.StopMusic();
            Destroy(AudioManager.instance);
        }

        AudioManager.instance.PlayFixedPitchSFX(AudioManager.instance.soundEffects[12]);
        StartCoroutine(ChangeScene());
    }
}
