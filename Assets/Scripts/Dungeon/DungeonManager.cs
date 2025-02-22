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

    private void Start()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        // create the dungeon grid
        int dungeonDimensions = settings.maxLength * 2 + 1;
        dungeonGrid = new RoomData[dungeonDimensions, dungeonDimensions];
        spawnOffset = new Vector2(dungeonDimensions + 1, dungeonDimensions + 1) * settings.tileset.tileSize / 2;

        SpawnRoom(new Vector2Int(settings.maxLength + 1, settings.maxLength + 1), settings.tileset.spawnRoom);
        SpawnRoom(new Vector2Int(settings.maxLength + 2, settings.maxLength + 1));
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

        GameObject test = Instantiate(room, pos * settings.tileset.tileSize - spawnOffset, Quaternion.identity, gameObject.transform);

        return SpawnRoomResult.Success;
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

        // check space below
        current = dungeonGrid[pos.x, pos.y - 1];
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

        // check space to right
        current = dungeonGrid[pos.x + 1, pos.y];
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

        // check space to left
        current = dungeonGrid[pos.x - 1, pos.y];
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

        // compare lower edge data to touching tile
        comparison = dungeonGrid[pos.x, pos.y - 1];
        if (comparison != null)
        {
            if (lowestDistance > comparison.distance)
            {
                lowestDistance = comparison.distance;
            }

            newData.SetEdgeType(Edges.Lower, comparison.GetEdgeType(Edges.Upper));
            downSet = true;
        }

        // compare right edge data to touching tile
        comparison = dungeonGrid[pos.x + 1, pos.y];
        if (comparison != null)
        {
            if (lowestDistance > comparison.distance)
            {
                lowestDistance = comparison.distance;
            }

            newData.SetEdgeType(Edges.Right, comparison.GetEdgeType(Edges.Left));
            rightSet = true;
        }

        // compare left edge data to touching tile
        comparison = dungeonGrid[pos.x - 1, pos.y];
        if (comparison != null)
        {
            if (lowestDistance > comparison.distance)
            {
                lowestDistance = comparison.distance;
            }

            newData.SetEdgeType(Edges.Left, comparison.GetEdgeType(Edges.Right));
            leftSet = true;
        }

        if (!upSet)
        {
            int type = Random.Range(0, 2);
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
            int type = Random.Range(0, 2);
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
            int type = Random.Range(0, 2);
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
            int type = Random.Range(0, 2);
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
}
