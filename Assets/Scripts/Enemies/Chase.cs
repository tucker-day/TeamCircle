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
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting chase state");
    }
}
