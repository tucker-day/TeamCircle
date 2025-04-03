using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditScene : MonoBehaviour
{
    public Button CreditButton;
    void Start()
    {
        
        CreditButton.onClick.AddListener(OnCreditButtonPressed);
    }


     public void OnCreditButtonPressed()
    {
        
        SceneManager.LoadScene("Credit");
    }
}
