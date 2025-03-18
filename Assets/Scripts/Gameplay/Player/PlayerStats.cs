using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    BaseStuct test = new BaseStuct();

    public int MaxHP;
    public int CurrentHP;
    public float PlayerSpeed;
    private PlayerStats playerStats;

    public void Awake()
    {
        MaxHP = 100;
        PlayerSpeed = GetComponent<PlayerMovement>().speed;
        CurrentHP = MaxHP;
    }



    public void TakeDamage(int damage)
    {
        CurrentHP -= damage; 
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[6]);

        if (CurrentHP <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[7]);
        Debug.Log("Game over");
    }
}
