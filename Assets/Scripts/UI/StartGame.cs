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
    if (fade != null)
    {
        fade.FadeIn(); // fade to black
        yield return new WaitForSeconds(1.2f);
    }

    if (AudioManager.instance != null)
    {
        Destroy(AudioManager.instance);
    }

    string sceneToLoad = "Dungeon";

    //  If already in Dungeon, reload it instead
    if (SceneManager.GetActiveScene().name == "Dungeon")
    {
        SceneManager.LoadScene("Dungeon");
        yield break; // no need to do async load if it's a reload
    }

    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
    asyncLoad.allowSceneActivation = false;

    while (!asyncLoad.isDone)
    {
        if (asyncLoad.progress >= 0.9f)
        {
            asyncLoad.allowSceneActivation = true;
        }

        yield return null;
    }
}


    public void OnStartButtonPressed()
    {
        AudioManager.instance.PlayFixedPitchSFX(AudioManager.instance.soundEffects[13]);
        AudioManager.instance.StopMusic();
        StartCoroutine(ChangeScene());
    }
}

