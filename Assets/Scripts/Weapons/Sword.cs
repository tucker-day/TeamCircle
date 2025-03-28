using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment
{
    public override void Trigger(Vector2 playerMovementDir)
    {
        Debug.Log("Boop :3 " + playerMovementDir);
    }
}
