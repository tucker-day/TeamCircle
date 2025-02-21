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

    [Header("Edge Fillers")]
    public List<GameObject> halls;
    public List<GameObject> walls;
    public List<GameObject> corners;

    public Vector2 tileSize;
}
