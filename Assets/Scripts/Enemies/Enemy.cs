using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    public IEnemyState currentState;

    public Animator anim;
    public GameObject playerObj;
    public Transform playerPos;
    public PlayerStats playerStats;

    public int hp;
    public int damage;
    public float speed = 2f;
    public float detectionRange;
    public float attackRange;
    bool isDead = false;

    protected void Start()
    {
        anim = GetComponent<Animator>();

        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.transform;
        playerStats = playerObj.GetComponent<PlayerStats>();
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
        Destroy(gameObject);
    }
}
