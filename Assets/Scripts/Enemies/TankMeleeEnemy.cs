using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMeleeEnemy : MeleeEnemy
{
    public float distance;
    void Start()
    {
        hp = 60;
        speed = 0.8f;
        attackRange = 0.5f;
        damage = 10;
        base.Start();
        ChangeState(new Chase());
    }

    public override void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

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
