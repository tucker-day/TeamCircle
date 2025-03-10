using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float distance;
    public MeleeEnemy()
    {
        attackRange = 1.0f;
    }

    void Start()
    {
        damage = 20;
        base.Start();
        ChangeState(new Chase());
    }

    public override void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }

    public override void Attack()
    {
        playerStats.TakeDamage(damage);
    }
}
