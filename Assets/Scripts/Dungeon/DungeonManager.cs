using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    public GameObject prefabQuery;

    public DungeonSettings settings;

    int branchReduction = 0;
    bool bossRoomSpawned = false;
    GameObject query;

    public void Start()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon() 
    {
        StartCoroutine(SpawnSpawnRoom());
    }

    private IEnumerator SpawnSpawnRoom()
    {
        float startTime = Time.realtimeSinceStartup;
        
        GameObject spawnRoom = Instantiate(settings.tileset.spawnRoom, gameObject.transform);
        Hallway spawnHall = spawnRoom.GetComponent<ChildRoom>().hallways[0];

        // create an instance of the query object
        query = Instantiate(prefabQuery, gameObject.transform);

        // Starts the spawn recursion
        yield return SpawnChildRoom(spawnHall, 0);
        // Everything after happens once recursion is finished

        float endTime = Time.realtimeSinceStartup - startTime;

        Debug.Log("Generation Complete in " + endTime + " seconds or approx. " + (int)(endTime * 50) + " fixed updates");

        ChildRoom[] game = GetComponentsInChildren<ChildRoom>();
        Debug.Log("Created " + game.Length + " rooms");
    }

    private IEnumerator SpawnChildRoom(Hallway connection, int currentDistance)
    {
        Vector3 spawnPoint = SpawnPointFromHall(connection);

        if (currentDistance < settings.maxLength - branchReduction)
        {
            List<GameObject> exclude = new List<GameObject>();
            List<GameObject> tiles = settings.tileset.tiles;

            while (!connection.connected)
            {
                // Get random tile and spawn it
                GameObject prefab = GetRandomGameObjectFromList(tiles, exclude);

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
                        childRoom.ConnectAllClaims();

                        foreach (Hallway hall in childRoom.hallways)
                        {
                            yield return SpawnChildRoom(hall, currentDistance + 1);
                        }
                    }
                }
                else
                {
                    tiles = settings.tileset.capTiles;
                    exclude.Clear();
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
        if (list.Count > exclude.Count)
        {
            do
            {
                result = list[Random.Range(0, list.Count)];
            } while (exclude.Contains(result));
        }

        return result;
    }
}
