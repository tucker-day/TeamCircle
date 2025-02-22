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
            if (current != null)
            {
                EdgeType edgeType = current.GetEdgeType(Edges.Lower);

                switch (edgeType)
                {
                    case EdgeType.Wall:
                        if (!room.upperEdgeRules.CanBeWall) return false;
                        break;
                    case EdgeType.Door:
                        if (!room.upperEdgeRules.CanBeDoor) return false;
                        break;
                    case EdgeType.Open:
                        if (!room.upperEdgeRules.CanBeOpen) return false;
                        break;
                }
            }
        }

        if (pos.y > 0)
        {
            // check space below
            RoomData current = dungeonGrid[pos.x, pos.y - 1];
            if (current != null)
            {
                EdgeType edgeType = current.GetEdgeType(Edges.Upper);

                switch (edgeType)
                {
                    case EdgeType.Wall:
                        if (!room.lowerEdgeRules.CanBeWall) return false;
                        break;
                    case EdgeType.Door:
                        if (!room.lowerEdgeRules.CanBeDoor) return false;
                        break;
                    case EdgeType.Open:
                        if (!room.lowerEdgeRules.CanBeOpen) return false;
                        break;
                }
            }
        }

        if (pos.x < dungeonSize - 1)
        {
            // check space to right
            RoomData current = dungeonGrid[pos.x + 1, pos.y];
            if (current != null)
            {
                EdgeType edgeType = current.GetEdgeType(Edges.Left);

                switch (edgeType)
                {
                    case EdgeType.Wall:
                        if (!room.rightEdgeRules.CanBeWall) return false;
                        break;
                    case EdgeType.Door:
                        if (!room.rightEdgeRules.CanBeDoor) return false;
                        break;
                    case EdgeType.Open:
                        if (!room.rightEdgeRules.CanBeOpen) return false;
                        break;
                }
            }
        }

        if (pos.x > 0)
        {
            // check space to left
            RoomData current = dungeonGrid[pos.x - 1, pos.y];
            if (current != null)
            {
                EdgeType edgeType = current.GetEdgeType(Edges.Right);

                switch (edgeType)
                {
                    case EdgeType.Wall:
                        if (!room.leftEdgeRules.CanBeWall) return false;
                        break;
                    case EdgeType.Door:
                        if (!room.leftEdgeRules.CanBeDoor) return false;
                        break;
                    case EdgeType.Open:
                        if (!room.leftEdgeRules.CanBeOpen) return false;
                        break;
                }
            }
        }

        // if the code made it here, it's a valid position!
        return true;
    }

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
            int type = UnityEngine.Random.Range(0, 2);
            if (type == 0)
            {
                newData.SetEdgeType(Edges.Upper, EdgeType.Door);
            }
            else if (type == 1)
            {
                newData.SetEdgeType(Edges.Upper, EdgeType.Wall);
            }
        }

        if (!downSet)
        {
            int type = UnityEngine.Random.Range(0, 2);
            if (type == 0)
            {
                newData.SetEdgeType(Edges.Lower, EdgeType.Door);
            }
            else if (type == 1)
            {
                newData.SetEdgeType(Edges.Lower, EdgeType.Wall);
            }
        }

        if (!rightSet)
        {
            int type = UnityEngine.Random.Range(0, 2);
            if (type == 0)
            {
                newData.SetEdgeType(Edges.Right, EdgeType.Door);
            }
            else if (type == 1)
            {
                newData.SetEdgeType(Edges.Right, EdgeType.Wall);
            }
        }

        if (!leftSet)
        {
            int type = UnityEngine.Random.Range(0, 2);
            if (type == 0)
            {
                newData.SetEdgeType(Edges.Left, EdgeType.Door);
            }
            else if (type == 1)
            {
                newData.SetEdgeType(Edges.Left, EdgeType.Wall);
            }
        }

        newData.distance = (byte)(lowestDistance + cost);

        return newData;
    }

    private void SpawnPerimeterObjects(Vector2Int pos, RoomData roomData, GameObject parent)
    {
        Vector2 spawnOrigin = GetSpawnPos(pos);

        // upper
        Vector2 spawnPoint = spawnOrigin + new Vector2(0, settings.tileset.tileSize.y / 2);
        if (roomData.GetEdgeType(Edges.Upper) == EdgeType.Door)
        {
            Instantiate(settings.tileset.upperHall, spawnPoint, Quaternion.identity, parent.transform);
        }
        else
        {
            Instantiate(settings.tileset.upperWall, spawnPoint, Quaternion.identity, parent.transform);
        }

        // lower
        spawnPoint = spawnOrigin + new Vector2(0, -settings.tileset.tileSize.y / 2);
        if (roomData.GetEdgeType(Edges.Lower) == EdgeType.Door)
        {
            Instantiate(settings.tileset.lowerHall, spawnPoint, Quaternion.identity, parent.transform);
        }
        else
        {
            Instantiate(settings.tileset.lowerWall, spawnPoint, Quaternion.identity, parent.transform);
        }

        // right
        spawnPoint = spawnOrigin + new Vector2(settings.tileset.tileSize.x / 2, 0);
        if (roomData.GetEdgeType(Edges.Right) == EdgeType.Door)
        {
            Instantiate(settings.tileset.rightHall, spawnPoint, Quaternion.identity, parent.transform);
        }
        else
        {
            Instantiate(settings.tileset.rightWall, spawnPoint, Quaternion.identity, parent.transform);
        }

        // left
        spawnPoint = spawnOrigin + new Vector2(-settings.tileset.tileSize.x / 2, 0);
        if (roomData.GetEdgeType(Edges.Left) == EdgeType.Door)
        {
            Instantiate(settings.tileset.leftHall, spawnPoint, Quaternion.identity, parent.transform);
        }
        else
        {
            Instantiate(settings.tileset.leftWall, spawnPoint, Quaternion.identity, parent.transform);
        }
    }
}
