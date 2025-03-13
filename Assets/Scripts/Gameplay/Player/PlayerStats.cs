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
    //public float PlayerLuck = 100.0f;
    private PlayerStats playerStats;

    //[SerializeField]
    //public HPBarManager hpBarManager;

    public void Awake()
    {
        MaxHP = 100;
        PlayerSpeed = GetComponent<PlayerMovement>().speed;
        CurrentHP = MaxHP;
        Debug.Log(PlayerSpeed);
        //playerStats = GetComponent<PlayerStats>();


        //TakeDamage(1);
    }



    public void TakeDamage(int damage)
    {
        //playerStats.CurrentHP = 1;//testing taking damage and death

        CurrentHP -= (int)damage; // Uncomment to test 
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[6]);

        //HPBarManager.UpdateHP();

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
