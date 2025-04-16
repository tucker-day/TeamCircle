using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectile;
    public GameObject launchPoint;

    public float distance;
    void Awake()
    {
        hp = 25;
        speed = 1.0f;
        attackRange = 5.0f;
        damage = 10;
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
            Instantiate(projectile, launchPoint.transform.position, Quaternion.identity);
            timer = cooldown;
            AudioManager.instance.CheckForLastPlayed(AudioManager.instance.soundEffects[3]);
        }
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        AudioManager.instance.CheckForLastPlayed(AudioManager.instance.soundEffects[4]);
    }

    public override void Die()
    {
        AudioManager.instance.CheckForLastPlayed(AudioManager.instance.soundEffects[5]);
        base.Die();
    }
}
