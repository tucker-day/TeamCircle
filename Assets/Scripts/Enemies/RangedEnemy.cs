using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectile;

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

        Flip();
    }

    public override void Attack()
    {
        if (timer <= 0)
        {
            Instantiate(projectile);
            playerStats.TakeDamage(damage);
            timer = cooldown;
        }
    }
}
