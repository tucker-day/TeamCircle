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
        GameObject spawnRoom = Instantiate(settings.tileset.spawnRoom, gameObject.transform);
        Hallway spawnHall = spawnRoom.GetComponent<ChildRoom>().hallways[0];

        yield return SpawnChildRoom(spawnHall, 0);
    }

    private IEnumerator SpawnChildRoom(Hallway connect, int currentDistance)
    {
        Vector3 spawnPoint = SpawnPointFromHall(connect);

        if (currentDistance < settings.maxLength - branchReduction)
        {
            while (!connect.connected)
            {
                // Get random tile and spawn it
                GameObject prefab = GetRandomGameObjectFromList(settings.tileset.tiles);
                GameObject roomObject = Instantiate(prefab, spawnPoint, Quaternion.identity, gameObject.transform);
                ChildRoom childRoom = roomObject.GetComponent<ChildRoom>();

                // Wait for collisions to happen
                yield return new WaitForFixedUpdate();
                // yield return new WaitForSeconds(0.25f);

                if (!childRoom.InValidPosition())
                {
                    Destroy(roomObject);
                }
                else
                {
                    childRoom.ConnectAllCollidingHalls();

                    foreach (Hallway hall in childRoom.hallways)
                    {
                        yield return SpawnChildRoom(hall, currentDistance + 1);
                    }
                }
            }
        }
        else
        {
            // Boss room spawning will be implemented here later
        }
    }

    // Gets the correct spawn point of a room based on the hall it's connecting to
    private Vector3 SpawnPointFromHall(Hallway hall)
    {
        Vector3 result = hall.gameObject.transform.position;

        switch(hall.direction)
        {
            case HallwayDirection.Up:
                result.y += settings.tileset.tileSize.y / 2;
                break;
            case HallwayDirection.Down:
                result.y -= settings.tileset.tileSize.y / 2;
                break;
            case HallwayDirection.Right:
                result.x += settings.tileset.tileSize.x / 2;
                break;
            case HallwayDirection.Left:
                result.x -= settings.tileset.tileSize.x / 2;
                break;
        }

        return result;
    }

    private GameObject GetRandomGameObjectFromList(List<GameObject> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
