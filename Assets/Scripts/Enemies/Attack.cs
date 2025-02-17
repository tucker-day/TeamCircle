using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Entering attack state");
    }
    public void UpdateState(Enemy enemy)
    {
        Debug.Log("Executing attack state");
        enemy.Attack();
        float distance = Vector3.Distance(enemy.player.transform.position, enemy.transform.position);

        if (distance >= enemy.attackRange)
        {
            enemy.anim.SetBool("isColliding", false);
            enemy.ChangeState(new Chase());
        }
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting attack state");
    }
}
