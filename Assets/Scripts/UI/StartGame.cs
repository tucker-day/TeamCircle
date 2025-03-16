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
     public IEnumerator ChangeScene(){
        fade.FadeIn();
        yield return new WaitForSeconds(1);
         SceneManager.LoadScene("Dungeon");
    }


     public void OnStartButtonPressed()
    {
        
       StartCoroutine(ChangeScene());
    }
}

