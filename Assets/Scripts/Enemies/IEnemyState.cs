using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void EnterState(Enemy enemy);
    void UpdateState(Enemy enemy);
    void ExitState(Enemy enemy);
}
