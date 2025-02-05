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
    [SerializeField] HallwayDirection direction = HallwayDirection.Up;
    public bool connected = false;

    public void Open()
    {

    }
    
    public void Close()
    {

    }
}
