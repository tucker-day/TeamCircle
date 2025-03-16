using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditScene : MonoBehaviour
{
    public Button CreditButton;
    FadeInOutSceneAnim fade;
    void Start()
    {
        fade=FindObjectOfType<FadeInOutSceneAnim>();
        CreditButton.onClick.AddListener(OnCreditButtonPressed);
    }

public IEnumerator ChangeScene(){
        fade.FadeIn();
        yield return new WaitForSeconds(1);
         SceneManager.LoadScene("Credit");
    }

     public void OnCreditButtonPressed()
    {
         StartCoroutine(ChangeScene());
       
    }
}
