using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    public IEnemyState currentState;

    public Animator anim;
    public Transform player;
    public PlayerStats playerStats;

    public int hp;
    public int damage;
    public float speed;
    public float detectionRange;
    public float attackRange;
    bool isDead = false;

    protected void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(IEnemyState state)
    {
        if(currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = state;
        currentState.EnterState(this);
    }
    public virtual void Move() { }

    public virtual void Chase() { }

    public virtual void Attack() { }

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
