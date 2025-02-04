using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonTileset", menuName = "Procedural Generation/Dungeon Tileset")]
public class DungeonTileset : ScriptableObject
{
    public GameObject spawnRoom;
    public List<GameObject> tiles;
    public List<GameObject> capTiles;
    public List<GameObject> bossRooms;
    public List<GameObject> endRooms;
}
