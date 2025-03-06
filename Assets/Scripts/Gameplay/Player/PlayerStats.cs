using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    BaseStuct test = new BaseStuct();

    public float MaxHP;
    public float CurrentHP;
    public float PlayerSpeed;
    //public float PlayerLuck = 100.0f;

    public void Start()
    {
        MaxHP = 100.0f;
        PlayerSpeed = GetComponent<PlayerMovement>().speed;
        CurrentHP = GetComponent<PlayerStats>().MaxHP;
        Debug.Log(PlayerSpeed);
    }

    void TakeDamage()
    {
        test.CurrentHP = 1.0f;//testing taking damage and death

        if (test.CurrentHP == 0.0f)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        Debug.Log("Game over");
    }
}
