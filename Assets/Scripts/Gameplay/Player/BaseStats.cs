using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BaseStuct
{
    public float MaxHP;//
    public float CurrentHP;// use this stat when modifying the players hp
    public float PlayerSpeed;//default speed
    public float PlayerLuck;//default luck
};




public class PlayerStats 
{
    BaseStuct test = new BaseStuct();
    void TakeDamage()
    { 
        test.CurrentHP = 1.0f;//testing taking damage and death

        if(test.CurrentHP == 0.0f)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        Debug.Log("Game over");
    }
}


















