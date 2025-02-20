using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
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
        Debug.Log("Melee is chasing the player");
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public override void Attack()
    {
        playerHP -= damage; // Placeholder
        // playerStats.TakeDamage(damage);
        Debug.Log("Melee is attacking the player");
    }
}
