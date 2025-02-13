using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    Transform target;

    public int hp;
    public int damage;
    public float speed;
    float detectionRange;
    float attackRange;
    bool isDead = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public virtual void Move()
    {

    }
    public virtual void Chase()
    {

    }

    public virtual void Attack()
    {
        Debug.Log("Attacking");
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
