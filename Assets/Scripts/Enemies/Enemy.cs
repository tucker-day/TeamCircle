using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public IEnemyState currentState;

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
