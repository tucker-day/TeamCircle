using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    public IEnemyState currentState;

    public static List<Enemy> s_enemyList = new List<Enemy>();

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;
    public GameObject playerObj;
    public Transform playerPos;
    public PlayerStats playerStats;

    public int hp;
    public int damage;
    public float speed;
    public float detectionRange;
    public float attackRange;
    bool isDead;
    bool canMove;

    public float cooldown;
    protected float timer;

    protected void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.transform;
        playerStats = playerObj.GetComponent<PlayerStats>();

        s_enemyList.Add(this);

        isDead = false;
        canMove = true;
        cooldown = 1.5f;
    }

    void Update()
    {
        currentState.UpdateState(this);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        //prototype button
        if (Input.GetKeyDown("p"))
        {
            TakeDamage(10000);
        }

        // prototype function
        if (Input.GetKeyDown("f"))
        {
            
        }
    }

    public void Flip()
    {
        if (playerPos.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerPos.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
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

    /*public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            TakeDamage(damage);
        }
    }*/
    public virtual void Move() { }

    public virtual void Chase() { }

    public virtual void Attack() { }

    public void FreezeMovement()
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
        isDead = true;
        s_enemyList.Remove(this);
        Destroy(gameObject);
        GameManager.instance.CheckForEnemies();
    }
}
