using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Enemy
{
    public GameObject projectile;
    public GameObject launchPoint;

    public float distance;
    public int MaxHP;
    void Awake()
    {
        MaxHP = 1000;
        hp = MaxHP;
        speed = 2.0f;
        attackRange = 8.0f;
        damage = 15;
        isAlive = true;
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
