using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Achievement : MonoBehaviour
{
    FadeInOutSceneAnim fade;
    public Button AchievementButton;

    void Start()
    {
        fade=FindObjectOfType<FadeInOutSceneAnim>();
        AchievementButton.onClick.AddListener(OnAchievementButtonPressed);
    }

    public IEnumerator ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Achievement");
    }

    public void OnAchievementButtonPressed()
    {
        AudioManager.instance.PlayFixedPitchSFX(AudioManager.instance.soundEffects[11]);
        StartCoroutine(ChangeScene());  
    }
} 

