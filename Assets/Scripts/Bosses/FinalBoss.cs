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

        Start();
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
        }
    }

    public override void TakeDamage(int damage)
    {
        if (isAlive == true)
        {
            hp -= damage;
            if (hp <= 0)
            {
                isAlive = false;
                //anim.SetBool("isAlive", false);
                //GameOver();
                Destroy(gameObject);
            }
        }
    }
}
