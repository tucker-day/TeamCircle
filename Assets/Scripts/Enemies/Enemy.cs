using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public IEnemyState currentState;

    Animator anim;
    public Transform target;

    public int hp;
    public int damage;
    public float speed;
    float detectionRange;
    float attackRange;
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
    public virtual void Move()
    {

    }

    public virtual void Chase()
    {
        Debug.Log("Base enemy chase");
    }

    public virtual void Attack()
    {
        Debug.Log("Base enemy attack");
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
