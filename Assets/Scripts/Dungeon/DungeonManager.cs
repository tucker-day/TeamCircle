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
        int cost = 0;

        if (forceRoom == null)
        {
            // if couldn't get a valid room, return early
            bool result = GetRandomValidRoom(pos, out room, out cost);
            if (!result) return SpawnRoomResult.Failed; 
        }
        else
        {
            room = forceRoom;

            // validate force room, return if impossible to fufil
            if (!room.TryGetComponent<ChildRoom>(out ChildRoom child))
            {
                Debug.LogError("Force room wasn't a room!");
                return SpawnRoomResult.ImpossibleForcePlace;
            }

            if (!IsValidPosition(pos, child))
            {
                return SpawnRoomResult.ImpossibleForcePlace;
            }
        }

        dungeonGrid[pos.x, pos.y] = new RoomData();

        GameObject test = Instantiate(room, pos * settings.tileset.tileSize - spawnOffset, Quaternion.identity, gameObject.transform);

        return SpawnRoomResult.Success;
    }

    private bool GetRandomValidRoom(Vector2Int pos, out GameObject room, out int cost)
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
            if (!room.TryGetComponent<ChildRoom>(out ChildRoom child))
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
}
