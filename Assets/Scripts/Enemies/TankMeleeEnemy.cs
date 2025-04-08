using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMeleeEnemy : MeleeEnemy
{
    void Awake()
    {
        hp = 60;
        speed = 0.8f;
        attackRange = 1.0f;
        damage = 10;
        ChangeState(new Chase());
    }
}
