using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float distance;
    void Start()
    {
        hp = 25;
        speed = 1.0f;
        attackRange = 5.0f;
        damage = 10;
        base.Start();
        ChangeState(new Chase());
    }

    public override void Chase()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }

        if (playerPos.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerPos.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    public override void Attack()
    {
        if (timer <= 0)
        {
            playerStats.TakeDamage(damage);
            timer = cooldown;
        }
    }
}
