using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnRoomResult
{
    Success,
    Failed,
    AlreadyClaimed,
    ImpossibleForcePlace
}

public class DungeonManager : MonoBehaviour
{
    public DungeonSettings settings;
    private RoomData[,] dungeonGrid;
    private Vector2 spawnOffset;
    private int dungeonSize;

    private void Start()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        // create the dungeon grid
        dungeonSize = settings.maxLength * 2 + 1;
        dungeonGrid = new RoomData[dungeonSize, dungeonSize];
        spawnOffset = new Vector2(dungeonSize - 1, dungeonSize - 1) * settings.tileset.tileSize / 2;

        SpawnRoom(new Vector2Int(settings.maxLength, settings.maxLength), settings.tileset.spawnRoom);

        // temporarily fill the array with tiles
        for (int x = 0; x < dungeonGrid.GetLength(0); x++)
        {
            for (int y = 0; y < dungeonGrid.GetLength(1); y++)
            {
                SpawnRoom(new Vector2Int(x, y));
            }
        }
    }

    // spawn a random room in a specific position. if a forceRoom is passed in, it will try to spawn
    // that room instead of a random room.
    private SpawnRoomResult SpawnRoom(Vector2Int pos, GameObject forceRoom = null)
    {
        // return if position is already claimed
        if (dungeonGrid[pos.x, pos.y] != null)
        {
            return SpawnRoomResult.AlreadyClaimed;
        }

        GameObject room;
        ChildRoom child;
        int cost = 0;

        if (forceRoom == null)
        {
            // if couldn't get a valid room, return early
            bool result = GetRandomValidRoom(pos, out room, out child, out cost);
            if (!result) return SpawnRoomResult.Failed; 
        }
        else
        {
            room = forceRoom;

            // validate force room, return if impossible to fufil
            if (!room.TryGetComponent<ChildRoom>(out child))
            {
                Debug.LogError("Force room wasn't a room!");
                return SpawnRoomResult.ImpossibleForcePlace;
            }

            if (!IsValidPosition(pos, child))
            {
                return SpawnRoomResult.ImpossibleForcePlace;
            }
        }

        dungeonGrid[pos.x, pos.y] = CreateRoomData(pos, child, cost);

        GameObject spawnedRoom = Instantiate(room, GetSpawnPos(pos), Quaternion.identity, gameObject.transform);
        SpawnPerimeterObjects(pos, dungeonGrid[pos.x, pos.y], spawnedRoom);

        return SpawnRoomResult.Success;
    }

    private Vector2 GetSpawnPos(Vector2Int pos)
    {
        return pos * settings.tileset.tileSize - spawnOffset;
    }

    private bool GetRandomValidRoom(Vector2Int pos, out GameObject room, out ChildRoom child, out int cost)
    {
        List<GameObject> exclude = new List<GameObject>();

        do
        {
            do
            {
                // get a random room
                settings.tileset.GetRandomRoom(out room, out cost);
            }
            while (exclude.Contains(room));

            // validate the room
            if (!room.TryGetComponent<ChildRoom>(out child))
            {
                Debug.LogError("Got a random room that wasn't a room!");
                exclude.Add(room);
                room = null;
            }

            if (!IsValidPosition(pos, child))
            {
                exclude.Add(room);
                room = null;
            }
        }
        while (room == null && exclude.Count < settings.tileset.rooms.Count);

        if (room == null)
        {
            Debug.Log("Unable to find valid random room!");

            child = null;
            cost = 0;

            return false;
        }

        return true;
    }

    private bool IsValidPosition(Vector2Int pos, ChildRoom room)
    {
        // check space above
        if (pos.y < dungeonSize - 1)
        {
            RoomData current = dungeonGrid[pos.x, pos.y + 1];
            if (!CompareEdgeToRoomData(current, Edges.Lower, room.edgeRules.upper))
            {
                return false;
            }
        }

        // check space below
        if (pos.y > 0)
        {
            RoomData current = dungeonGrid[pos.x, pos.y - 1];
            if (!CompareEdgeToRoomData(current, Edges.Upper, room.edgeRules.lower))
            {
                return false;
            }
        }

        // check space to right
        if (pos.x < dungeonSize - 1)
        {
            RoomData current = dungeonGrid[pos.x + 1, pos.y];
            if (!CompareEdgeToRoomData(current, Edges.Left, room.edgeRules.right))
            {
                return false;
            }
        }

        // check space to left
        if (pos.x > 0)
        {
            RoomData current = dungeonGrid[pos.x - 1, pos.y];
            if (!CompareEdgeToRoomData(current, Edges.Right, room.edgeRules.left))
            {
                return false;
            }
        }

        // if the code made it here, it's a valid position!
        return true;
    }

    private bool CompareEdgeToRoomData(RoomData other, Edges edge, EdgeRules rules)
    {
        if (other != null)
        {
            EdgeType edgeType = other.GetEdgeType(edge);

            switch (edgeType)
            {
                case EdgeType.Wall:
                    if (!rules.CanBeWall) return false;
                    break;
                case EdgeType.Hall:
                    if (!rules.CanBeDoor) return false;
                    break;
                case EdgeType.Open:
                    if (!rules.CanBeOpen) return false;
                    break;
            }
        }

        return true;
    }

    // NOTE: this function does not check if what it's doing is valid as you're
    // only supposed to use this after you check if the room is in a vaild position.
    // using on an invalid room will cause bugs.
    private RoomData CreateRoomData(Vector2Int pos, ChildRoom child, int cost)
    {
        int lowestDistance = 0;
        RoomData newData = new();

        bool upSet = false;
        bool rightSet = false;
        bool downSet = false;
        bool leftSet = false;

        if (pos.y < dungeonSize - 1)
        {
            // compare upper edge data to touching tile
            RoomData comparison = dungeonGrid[pos.x, pos.y + 1];
            if (comparison != null)
            {
                if (lowestDistance > comparison.distance)
                {
                    lowestDistance = comparison.distance;
                }

                newData.SetEdgeType(Edges.Upper, comparison.GetEdgeType(Edges.Lower));
                upSet = true;
            }
        }

        if (pos.y > 0)
        {
            // compare lower edge data to touching tile
            RoomData comparison = dungeonGrid[pos.x, pos.y - 1];
            if (comparison != null)
            {
                if (lowestDistance > comparison.distance)
                {
                    lowestDistance = comparison.distance;
                }

                newData.SetEdgeType(Edges.Lower, comparison.GetEdgeType(Edges.Upper));
                downSet = true;
            }
        }

        if (pos.x < dungeonSize - 1)
        {
            // compare right edge data to touching tile
            RoomData comparison = dungeonGrid[pos.x + 1, pos.y];
            if (comparison != null)
            {
                if (lowestDistance > comparison.distance)
                {
                    lowestDistance = comparison.distance;
                }

                newData.SetEdgeType(Edges.Right, comparison.GetEdgeType(Edges.Left));
                rightSet = true;
            }
        }

        if (pos.x > 0)
        {
            // compare left edge data to touching tile
            RoomData comparison = dungeonGrid[pos.x - 1, pos.y];
            if (comparison != null)
            {
                if (lowestDistance > comparison.distance)
                {
                    lowestDistance = comparison.distance;
                }

                newData.SetEdgeType(Edges.Left, comparison.GetEdgeType(Edges.Right));
                leftSet = true;
            }
        }    

        if (!upSet)
        {
            int type = UnityEngine.Random.Range(0, 3);
            if (type == 0)
            {
                newData.SetEdgeType(Edges.Upper, EdgeType.Hall);
            }
            else if (type == 1)
            {
                newData.SetEdgeType(Edges.Upper, EdgeType.Wall);
            }
            else if (type == 2)
            {
                newData.SetEdgeType(Edges.Upper, EdgeType.Open);
            }
        }

        if (!downSet)
        {
            int type = UnityEngine.Random.Range(0, 3);
            if (type == 0)
            {
                newData.SetEdgeType(Edges.Lower, EdgeType.Hall);
            }
            else if (type == 1)
            {
                newData.SetEdgeType(Edges.Lower, EdgeType.Wall);
            }
            else if (type == 2)
            {
                newData.SetEdgeType(Edges.Lower, EdgeType.Open);
            }
        }

        if (!rightSet)
        {
            int type = UnityEngine.Random.Range(0, 3);
            if (type == 0)
            {
                newData.SetEdgeType(Edges.Right, EdgeType.Hall);
            }
            else if (type == 1)
            {
                newData.SetEdgeType(Edges.Right, EdgeType.Wall);
            }
            else if (type == 2)
            {
                newData.SetEdgeType(Edges.Right, EdgeType.Open);
            }
        }

        if (!leftSet)
        {
            int type = UnityEngine.Random.Range(0, 3);
            if (type == 0)
            {
                newData.SetEdgeType(Edges.Left, EdgeType.Hall);
            }
            else if (type == 1)
            {
                newData.SetEdgeType(Edges.Left, EdgeType.Wall);
            }
            else if (type == 2)
            {
                newData.SetEdgeType(Edges.Left, EdgeType.Open);
            }
        }

        newData.distance = (byte)(lowestDistance + cost);

        return newData;
    }

    private void SpawnPerimeterObjects(Vector2Int pos, RoomData roomData, GameObject parent)
    {
        Vector2 spawnOrigin = GetSpawnPos(pos);

        SpawnObjectsOnEdge(spawnOrigin, roomData, Edges.Upper, parent);
        SpawnObjectsOnEdge(spawnOrigin, roomData, Edges.Lower, parent);
        SpawnObjectsOnEdge(spawnOrigin, roomData, Edges.Right, parent);
        SpawnObjectsOnEdge(spawnOrigin, roomData, Edges.Left, parent);
    }

    private void SpawnObjectsOnEdge(Vector2 pos, RoomData data, Edges edge, GameObject parent)
    {
        Vector2 adjustment = Vector2.zero;
        EdgeGroup edgeGroup = new();

        switch (edge)
        {
            case Edges.Upper:
                adjustment = new Vector2(0, settings.tileset.tileSize.y / 2);
                edgeGroup = settings.tileset.edges.upper;
                break;
            case Edges.Lower:
                adjustment = new Vector2(0, -settings.tileset.tileSize.y / 2);
                edgeGroup = settings.tileset.edges.lower;
                break;
            case Edges.Left:
                adjustment = new Vector2(-settings.tileset.tileSize.x / 2, 0);
                edgeGroup = settings.tileset.edges.left;
                break;
            case Edges.Right:
                adjustment = new Vector2(settings.tileset.tileSize.x / 2, 0);
                edgeGroup = settings.tileset.edges.right;
                break;
        }

        Vector2 spawnPoint = pos + adjustment;
        GameObject prefab;

        if (data.GetEdgeType(edge) == EdgeType.Hall)
        {
            prefab = edgeGroup.hall;        
        }
        else if (data.GetEdgeType(edge) == EdgeType.Wall)
        {
            prefab = edgeGroup.wall;
        }
        else if (data.GetEdgeType(edge) == EdgeType.Open)
        {
            prefab = edgeGroup.open;
        }
        else
        {
            // it should be impossible to get here, but if it does just spawn a wall and print errror
            Debug.LogError("Tried spawning objects on invalid edge!");
            prefab = edgeGroup.wall;
        }

        Instantiate(prefab, spawnPoint, Quaternion.identity, parent.transform);
    }
}
