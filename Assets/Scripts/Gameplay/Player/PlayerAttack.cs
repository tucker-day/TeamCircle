using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float Timer = 0.0f;
    public float attackSpeed = 2.0f;
    public float WaitTime = 2.0f;
    // Start is called before the first frame update

    public void AutoAttack()
    {
        Debug.Log("Attack log");
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > attackSpeed)
        {
            AutoAttack();
            Timer = Timer - WaitTime;
        }
    }
}
