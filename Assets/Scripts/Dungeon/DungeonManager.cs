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

    public void GenerateDungeon() 
    {
        StartCoroutine(SpawnSpawnRoom());
    }

    private IEnumerator SpawnSpawnRoom()
    {
        GameObject spawnRoom = Instantiate(settings.tileset.spawnRoom, gameObject.transform);
        Hallway spawnHall = spawnRoom.GetComponent<ChildRoom>().hallways[0];

        yield return SpawnChildRoom(spawnHall, 0);

        Debug.Log("Generation Complete");
    }

    private IEnumerator SpawnChildRoom(Hallway connect, int currentDistance)
    {
        Vector3 spawnPoint = SpawnPointFromHall(connect);

        if (currentDistance < settings.maxLength - branchReduction)
        {
            List<GameObject> exclude = new List<GameObject>();

            while (!connect.connected)
            {
                // Get random tile and spawn it
                GameObject prefab = GetRandomGameObjectFromList(settings.tileset.tiles, exclude);

                if (prefab != null)
                {
                    GameObject roomObject = Instantiate(prefab, spawnPoint, Quaternion.identity, gameObject.transform);
                    ChildRoom childRoom = roomObject.GetComponent<ChildRoom>();

                    // Wait for collisions to happen
                    yield return new WaitForFixedUpdate();

                    if (!childRoom.InValidPosition())
                    {
                        exclude.Add(prefab);
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
                else
                {
                    // Will implement spawning cap rooms here later. For now, just say the
                    // hall gets connected and move on
                    Debug.Log("Couldn't spawn non-cap room.");
                    connect.connected = true;
                }
            }
        }
        else
        {
            // Boss room & cap room spawning will be implemented here later
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
        if (list.Count > 0)
        {
            return list[Random.Range(0, list.Count)];
        }
        else
        {
            return null;
        }
    }

    // Gets a random object from list that is not contained in the exclude list.
    // Returns null if none can be found.
    private GameObject GetRandomGameObjectFromList(List<GameObject> list, List<GameObject> exclude)
    {
        GameObject result = null;

        // Assuming that exclude only contains values that are also in list, if
        // exclude is larger or equal to list, there is no possible object that
        // can be returned, so return null.
        if (exclude.Count < list.Count)
        {
            do
            {
                result = list[Random.Range(0, list.Count)];
            } while (exclude.Contains(result));
        }

        return result;
    }
}
