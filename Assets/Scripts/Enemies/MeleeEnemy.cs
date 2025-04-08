using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float distance;
    void Awake()
    {
        hp = 50;
        speed = 2.0f;
        attackRange = 1.0f;
        damage = 5;
        ChangeState(new Chase());
    }

    public override void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

        Flip();
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
