using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackScript : MonoBehaviour
{

    [SerializeField] private FeedbackScript attackEffect;
    [SerializeField] private KeyCode flash; 
    
    
    void Update()
    {
        if (Input.GetKeyDown(flash))
        {
          //  attackEffect.flash(); 
        }
    }
}
