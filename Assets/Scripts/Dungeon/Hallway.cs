using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum HallwayDirection
{
    Up,
    Down, 
    Left, 
    Right
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Hallway : MonoBehaviour
{
    public HallwayDirection direction = HallwayDirection.Up;
    public bool connected = false;

    public ChildRoom collidingRoom = null;

    public void Open()
    {

    }
    
    public void Close()
    {

    }

    public HallwayDirection OppositeDir()
    {
        switch (direction)
        {
            case HallwayDirection.Left:
                return HallwayDirection.Right;
            case HallwayDirection.Right:
                return HallwayDirection.Left;
            case HallwayDirection.Up:
                return HallwayDirection.Down;
            case HallwayDirection.Down:
                return HallwayDirection.Up;
        }

        // Should be imposable to reach here, but printing
        // an error and returning just in case
        Debug.LogError("Ended opposite dir without finding opposite");
        return direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Keep track of what room the hall claim is colliding with
        if (collision.gameObject.layer == LayerMask.NameToLayer("Child Room"))
        {
            if (collision.gameObject.TryGetComponent(out ChildRoom room))
            {
                collidingRoom = room;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Remove room when it disapears
        if (collision.gameObject.layer == LayerMask.NameToLayer("Child Room"))
        {
            if (collision.gameObject.TryGetComponent(out ChildRoom room))
            {
                collidingRoom = null;
            }
        }
    }
}
