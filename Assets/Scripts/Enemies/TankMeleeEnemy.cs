using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMeleeEnemy : MeleeEnemy
{
    void Start()
    {
        hp = 60;
        speed = 0.8f;
        attackRange = 1.0f;
        damage = 10;
        base.Start();
        ChangeState(new Chase());
    }
}
