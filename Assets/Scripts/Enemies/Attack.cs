using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Entering attack state");
    }
    public void UpdateState(Enemy enemy)
    {
        Debug.Log("Executing attack state");
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting attack state");
    }
}
