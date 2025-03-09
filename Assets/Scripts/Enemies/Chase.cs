using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Entering chase state");
    }
    public void UpdateState(Enemy enemy)
    {
        Debug.Log("Executing chase state");
        enemy.Chase();
        float distance = Vector3.Distance(enemy.playerPos.position, enemy.transform.position);
        Debug.Log("distance is:" + distance);
        Debug.Log("player pos is:" + enemy.playerPos);

        if (distance <= enemy.attackRange)
        {
            enemy.anim.SetBool("isColliding", true);
            enemy.ChangeState(new Attack());
        }
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting chase state");
    }
}
