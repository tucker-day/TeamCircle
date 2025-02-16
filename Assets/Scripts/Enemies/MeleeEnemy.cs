using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    void Start()
    {
        base.Start();
        ChangeState(new Chase());
    }

    public override void Chase()
    {
        Debug.Log("Melee is chasing the player");
    }

    public override void Attack()
    {
        Debug.Log("Melee is attacking the player");
    }
}
