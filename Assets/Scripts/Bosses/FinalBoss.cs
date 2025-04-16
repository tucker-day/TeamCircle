using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Enemy
{
    public GameObject projectile;
    public Projectile projScript;
    public GameObject launchPoint;

    public float distance;
    public int MaxHP;
    void Awake()
    {
        MaxHP = 1000;
        hp = MaxHP;
        speed = 3.0f;
        attackRange = 5.0f;
        damage = 15;
        isAlive = true;

        s_rareEnemyList.Add(this);

        ChangeState(new Chase());
    }
    public override void Chase()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
    }

    public override void Attack()
    {
        if (timer <= 0)
        {
            Instantiate(projectile, launchPoint.transform.position, Quaternion.identity);
            projScript = projectile.GetComponent<Projectile>();
            projScript.isBossProjectile = true;

            timer = cooldown;
            AudioManager.instance.CheckForLastPlayed(AudioManager.instance.soundEffects[14]);
        }
    }

    public override void TakeDamage(int damage)
    {
        if (isAlive == true)
        {
            ShowDamage(damage.ToString());
            hp -= damage;
            AudioManager.instance.CheckForLastPlayed(AudioManager.instance.soundEffects[15]);

            if (hp <= 0)
            {
                isAlive = false;
                anim.SetBool("isAlive", false);
                //GameOver();
                AudioManager.instance.CheckForLastPlayed(AudioManager.instance.soundEffects[16]);
                s_rareEnemyList.Remove(this);
                Die();
            }
        }
    }
}
