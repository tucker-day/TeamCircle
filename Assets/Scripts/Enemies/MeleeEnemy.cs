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
            AudioManager.instance.CheckForLastPlayed(AudioManager.instance.soundEffects[0]);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        AudioManager.instance.CheckForLastPlayed(AudioManager.instance.soundEffects[1]);
    }

    public override void Die()
    {
        AudioManager.instance.CheckForLastPlayed(AudioManager.instance.soundEffects[2]);
        base.Die();
    }
}
