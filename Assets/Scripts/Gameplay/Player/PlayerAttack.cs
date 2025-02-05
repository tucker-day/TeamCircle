using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float Timer = 0.0f;
    public float AttackDelay = 2.0f;
    public float WaitTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AutoAttack()
    {
        Debug.Log("Attack log");
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > AttackDelay)
        {
            AutoAttack();
            Timer = Timer - WaitTime;
        }
    }
}
