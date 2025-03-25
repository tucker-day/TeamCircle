using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;
    public float PlayerSpeed;
    public bool isAlive;

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public GameObject GameOverScreen;
    [SerializeField]
    private GameObject HUD;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        MaxHP = 100;
        PlayerSpeed = GetComponent<PlayerMovement>().speed;
        CurrentHP = MaxHP;
        isAlive = true;
        anim.SetBool("isAlive", true);
    }



    public void TakeDamage(int damage)
    {
        if (isAlive == true) {
            CurrentHP -= damage;
            AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[6]);

            if (CurrentHP <= 0)
            {
                anim.SetBool("isAlive", false);
                GameOver();
            }
        }
    }

    void GameOver()
    {
        isAlive = false;
        anim.SetBool("isAlive", false);
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[7]);
        Debug.Log("Game over");
        Instantiate(GameOverScreen, HUD.transform);
    }
}
