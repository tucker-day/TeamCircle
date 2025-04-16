using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPrompt : MonoBehaviour
{
    public Animator promptAnimation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            promptAnimation.SetTrigger("ShowPrompt");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            promptAnimation.SetTrigger("ShowPrompt");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            promptAnimation.SetTrigger("ShowPrompt");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            promptAnimation.SetTrigger("ShowPrompt");
        }


    }
}

