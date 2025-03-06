using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float Timer = 0.0f;
    public float attackSpeed = 2.0f;
    public float WaitTime = 2.0f;

    private PlayerStats playerStats;


    public void Start()
    {
        playerStats = GetComponent<PlayerStats>(); 
    }


    public void AutoAttack()
    {
        Debug.Log("Attack log");
    }

    // Update is called once per frame

    void Update()
    {
        if (playerStats.CurrentHP != 0)
        {
            Timer += Time.deltaTime;
            if (Timer > attackSpeed)
            {
                AutoAttack();
                Timer = Timer - WaitTime;
            }
        }
    }
}
