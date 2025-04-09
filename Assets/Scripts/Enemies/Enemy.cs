using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    public IEnemyState currentState;

    public static List<Enemy> s_enemyList = new List<Enemy>();
    public static List<Enemy> s_rareEnemyList = new List<Enemy>();

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;
    public GameObject playerObj;
    public Transform playerPos;
    public PlayerStats playerStats;

    public int hp;
    public int damage;
    public float speed;
    protected float frozenSpeed;
    protected float resSpeed; 
    public float detectionRange;
    public float attackRange;
    public bool isAlive;
    protected bool canMove;
    public bool isRareEnemy = false;

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

        isAlive = true;
        canMove = true;
        cooldown = 1.5f;

        resSpeed = speed;
        frozenSpeed = 0.0f;

        if (isRareEnemy)
        {
            hp *= 3;
            damage *= 2;
            speed *= 1.5f;
            this.gameObject.transform.localScale *= new Vector2(this.gameObject.transform.localScale.x * 1.5f, this.gameObject.transform.localScale.y * 1.5f);
            spriteRenderer.color = new Color(1f, 0.8f, 0.6f, 1f);
            s_rareEnemyList.Add(this);
        }
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

        // prototype functionality
        if (Input.GetKeyDown("f"))
        {
            if (canMove)
            {
                Freeze();
            }
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

    public virtual void Move() { }

    public virtual void Chase() { }

    public virtual void Attack() { }

    public void Freeze()
    {
        speed = frozenSpeed;
        anim.speed = 0;
        canMove = false;

        StartCoroutine("Unfreeze");
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(3);
        speed = resSpeed;
        anim.speed = 1;
        canMove = true;
    }

    public virtual void TakeDamage(int damage)
    {
        if (isAlive)
        {
            hp -= damage;

            if (hp <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log("Enemy killed");
        isAlive = false;
        s_enemyList.Remove(this);
        DropPickup();
        Destroy(gameObject);
        GameManager.instance.CheckForEnemies();

        if (isRareEnemy)
        {
            s_rareEnemyList.Remove(this);
            GameManager.instance.CheckForRareEnemies();
        }
    }

    void DropPickup()
    {
        if (!isRareEnemy)
        {
            int dropChance = UnityEngine.Random.Range(0, 100);
            if (dropChance >= 99)
            {
                Debug.Log("An enemy dropped a health pickup!");
                GameManager.instance.SpawnHealthPickup(this.transform.position);
            }
        }
        if (isRareEnemy)
        {
            GameManager.instance.SpawnWeaponPickup(this.transform.position);
        }
    }
}
