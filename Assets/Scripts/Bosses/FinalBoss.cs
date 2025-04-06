using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Enemy
{
    public int MaxHP;
    void Awake()
    {
        MaxHP = 1000;
        hp = MaxHP;
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
