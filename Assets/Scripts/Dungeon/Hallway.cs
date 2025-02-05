using System;
using UnityEngine;

[Serializable]
public enum HallwayDirection
{
    Up,
    Down, 
    Left, 
    Right
}

public class Hallway : MonoBehaviour
{
    public HallwayDirection direction = HallwayDirection.Up;
    public bool connected = false;

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
}
