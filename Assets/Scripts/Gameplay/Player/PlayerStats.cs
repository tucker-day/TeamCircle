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
    //private PlayerStats playerStats;
    public bool isAlive;

    public Animator anim;
    public SpriteRenderer spriteRenderer;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        MaxHP = 100;
        PlayerSpeed = GetComponent<PlayerMovement>().speed;
        CurrentHP = MaxHP;
        anim.SetBool("isAlive", true);
    }



    public void TakeDamage(int damage)
    {
        CurrentHP -= damage; 
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[6]);

        if (CurrentHP <= 0)
        {
            anim.SetBool("isAlive", false);
            GameOver();
        }
    }

    void GameOver()
    {
        isAlive = false;
        
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[7]);
        Debug.Log("Game over");
    }
}
