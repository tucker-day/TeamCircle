using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;

    public int hp;
    public int damage;
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0 )
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy killed");
        // Destroy(gameObject);
    }
}
