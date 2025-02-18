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
        
        SceneManager.LoadScene("Main");
    }
}
