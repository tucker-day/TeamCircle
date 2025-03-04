using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonTileset", menuName = "Procedural Generation/Dungeon Tileset")]
public class DungeonTileset : ScriptableObject
{
    [Header("Rooms")]
    public GameObject spawnRoom;
    public List<WeightedItem<GameObject>> rooms;
    public List<WeightedItem<GameObject>> bossRooms;
    public List<WeightedItem<GameObject>> endRooms;

    [Header("Halls")]
    public GameObject upperHall;
    public GameObject rightHall;
    public GameObject lowerHall;
    public GameObject leftHall;

    [Header("Walls")]
    public GameObject upperWall;
    public GameObject rightWall;
    public GameObject lowerWall;
    public GameObject leftWall;

    [Header("Corners")]
    public GameObject upperLeftCorner;
    public GameObject upperRightCorner;
    public GameObject lowerLeftCorner;
    public GameObject lowerRightCorner;

    public Vector2 tileSize;
}
