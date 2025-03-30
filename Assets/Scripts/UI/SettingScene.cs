using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingScene : MonoBehaviour
{   
    FadeInOutSceneAnim fade;
    public Button settingButton;  

    void Start()
    {
        fade=FindObjectOfType<FadeInOutSceneAnim>();
        settingButton.onClick.AddListener(OnSettingButtonPressed);
    }

    public IEnumerator ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Setting");
    }

    public void OnSettingButtonPressed()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[11]);
        StartCoroutine(ChangeScene());
    }
}
