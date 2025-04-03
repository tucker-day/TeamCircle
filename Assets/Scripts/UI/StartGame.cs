using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button startButton; 
    FadeInOutSceneAnim fade; 

    void Start()
    {
        fade=FindObjectOfType<FadeInOutSceneAnim>();
        startButton.onClick.AddListener(OnStartButtonPressed);
    }

    public IEnumerator ChangeScene()
    {
        if (!GameManager.instance)
        { fade.FadeIn(); }
        yield return new WaitForSeconds(1);
        Destroy(AudioManager.instance);
        SceneManager.LoadScene("Dungeon");
    }

    public void OnStartButtonPressed()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[13]);
        AudioManager.instance.StopMusic();
        StartCoroutine(ChangeScene());
    }
}

