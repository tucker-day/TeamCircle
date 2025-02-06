using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public DungeonSettings settings;

    int branchReduction = 0;
    bool bossRoomSpawned = false;

    private void Start()
    {
        GenerateDungeon();
    }

    // Start the dungeon generation
    public void GenerateDungeon() 
    {
        StartCoroutine(SpawnSpawnRoom());
    }

    private IEnumerator SpawnSpawnRoom()
    {
        GameObject spawnRoom = Instantiate(settings.tileset.spawnRoom);
        Hallway spawnHall = spawnRoom.GetComponent<ChildRoom>().hallways[0];

        yield return SpawnChildRoom(spawnHall, 0);
    }

    private IEnumerator SpawnChildRoom(Hallway connect, int distance)
    {

        GameObject childRoom = Instantiate(settings.tileset.spawnRoom, SpawnPointFromHall(connect), Quaternion.identity);

        // Wait for collisions to happen
        yield return new WaitForFixedUpdate();
    }

    private Vector3 SpawnPointFromHall(Hallway hall)
    {
        Vector3 result = hall.gameObject.transform.position;

        switch(hall.direction)
        {
            case HallwayDirection.Up:
                result.y += settings.tileset.tileSize.y / 2;
                break;
            case HallwayDirection.Down:
                result.y += settings.tileset.tileSize.y / 2;
                break;
            case HallwayDirection.Right:
                result.x += settings.tileset.tileSize.x / 2;
                break;
            case HallwayDirection.Left:
                result.x += settings.tileset.tileSize.x / 2;
                break;
        }

        return result;
    }
}
