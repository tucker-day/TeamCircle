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
    private Vector2 spawnOffset;
    private int dungeonSize;

    private RoomData[,] dungeonGrid;
    private Stack<Vector2Int> spawnList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int numChildren = transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }

            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            GenerateDungeon();
            stopWatch.Stop();
            Debug.Log(stopWatch.Elapsed);
        }
    }

    public void GenerateDungeon()
    {
        // create the dungeon grid
        dungeonSize = settings.maxLength * 2 + 1;
        dungeonGrid = new RoomData[dungeonSize, dungeonSize];
        spawnList = new Stack<Vector2Int>();
        spawnOffset = new Vector2(dungeonSize - 1, dungeonSize - 1) * settings.tileset.tileSize / 2;

        SpawnRoom(new Vector2Int(settings.maxLength, settings.maxLength), settings.tileset.spawnRoom);
        while (spawnList.Count > 0)
        {
            Vector2Int spawnCoord = spawnList.Pop();
            SpawnRoom(spawnCoord);
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
        SpawnPerimeterObjects(pos, dungeonGrid[pos.x, pos.y], child, spawnedRoom);

        if (dungeonGrid[pos.x, pos.y].distance < settings.maxLength)
        {
            RoomData data = dungeonGrid[pos.x, pos.y];

            // spawn rooms on edges that aren't walls
            foreach (Edges edge in Enum.GetValues(typeof(Edges)))
            {
                // if edge is wall, go to next iteration
                if (data.GetEdgeType(edge).Equals(EdgeType.Wall))
                {
                    continue;
                }

                Vector2Int newRoomPos = pos;

                switch (edge)
                {
                    case Edges.Upper:
                        newRoomPos += Vector2Int.up;
                        break;
                    case Edges.Lower:
                        newRoomPos += Vector2Int.down;
                        break;
                    case Edges.Left:
                        newRoomPos += Vector2Int.left;
                        break;
                    case Edges.Right:
                        newRoomPos += Vector2Int.right;
                        break;
                }

                spawnList.Push(newRoomPos);
            }
        }

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

            if (rules.BuildInEdge && rules.BuiltInEdgeType != edgeType)
            {
                return false;
            }
        }

        return true;
    }

    // NOTE: this function does not check if what it's doing is valid as you're
    // only supposed to use this after you check if the room is in a vaild position.
    // using on an invalid room will cause bugs.
    private RoomData CreateRoomData(Vector2Int pos, ChildRoom child, int cost)
    {
        RoomData newData = new();

        bool[] occupied = { false, false, false, false };

        foreach (Edges edge in Enum.GetValues(typeof(Edges)))
        {
            occupied[(int)edge] = TryInheritEdgeData(child, pos, edge, newData);
        }

        // distance is received in the TryInheritEdgeData function, so this
        // must stay below or else cost won't be added
        newData.distance += (byte)cost;
        newData.distanceSinceBranch += 1;

        int occupiedCount = 0;

        foreach (bool b in occupied)
        {
            occupiedCount += b ? 1 : 0;
        }

        if (newData.distance < settings.maxLength && occupiedCount < 4)
        {
            int nonWallCount = newData.GetNonWallCount();
            int nonWallTarget = 2;

            Debug.Log("distance from branch " + newData.distanceSinceBranch);

            if (newData.distanceSinceBranch >= settings.maxBranchDistance)
            {
                nonWallTarget = 3;
                Debug.Log("spawning distance branch");
            }
            else
            {
                float rngRoll = UnityEngine.Random.Range(0.0f, 1.0f);
                Debug.Log("rngRoll " + rngRoll);


                if (rngRoll < settings.branchChance)
                {
                    nonWallTarget = 3;
                    Debug.Log("spawning branch");
                }
                else if (rngRoll < settings.branchChance + settings.allHallChance)
                {
                    nonWallTarget = 4;
                    Debug.Log("spawning big branch");
                }
            }

            while (nonWallCount < nonWallTarget && occupiedCount < 4)
            {
                Edges target = (Edges)UnityEngine.Random.Range(0, 4);

                if (!occupied[(int)target])
                {
                    EdgeType edgeType = UnityEngine.Random.Range(0.0f, 1.0f) > settings.openChance ? EdgeType.Hall : EdgeType.Open;
                    newData.SetEdgeType(target, edgeType);
                    nonWallCount++;

                    occupied[(int)target] = true;
                    occupiedCount = 0;
                    foreach (bool b in occupied)
                    {
                        occupiedCount += b ? 1 : 0;
                    }
                }
            }
        }

        if (newData.GetNonWallCount() > 2)
        {
            newData.distanceSinceBranch = 0;
        }

        return newData;
    }

    private bool TryInheritEdgeData(ChildRoom child, Vector2Int pos, Edges edge, RoomData newData)
    {
        bool set = false;
        Vector2Int target = pos + RoomData.GetEdgeVectorConversion(edge);

        if (child.GetRulesByEnum(edge).BuildInEdge)
        {
            set = true;
            newData.SetEdgeType(edge, child.GetRulesByEnum(edge).BuiltInEdgeType);
        }
        else if (target.x < dungeonSize && target.x >= 0  &&
                 target.y < dungeonSize && target.y >= 0)
        {
            set = TryCopyEdgeFromNeighbor(pos, newData, edge);
        }

        return set;
    }

    private bool TryCopyEdgeFromNeighbor(Vector2Int pos, RoomData data, Edges edge)
    {
        Vector2Int comparePos = pos;
        Edges compareEdge = edge;

        switch (edge)
        {
            case Edges.Upper:
                comparePos += new Vector2Int(0, 1);
                compareEdge = Edges.Lower;
                break;
            case Edges.Lower:
                comparePos += new Vector2Int(0, -1);
                compareEdge = Edges.Upper;
                break;
            case Edges.Left:
                comparePos += new Vector2Int(-1, 0);
                compareEdge = Edges.Right;
                break;
            case Edges.Right:
                comparePos += new Vector2Int(1, 0);
                compareEdge = Edges.Left;
                break;
        }

        // compare edge data to touching tile
        RoomData comparison = dungeonGrid[comparePos.x, comparePos.y];
        if (comparison != null)
        {
            if ((data.distance > comparison.distance || data.distance == 0) && comparison.GetEdgeType(compareEdge) != EdgeType.Wall)
            {
                data.distance = comparison.distance;
                data.distanceSinceBranch = comparison.distanceSinceBranch;
            }

            data.SetEdgeType(edge, comparison.GetEdgeType(compareEdge));
            return true;
        }
        return false;
    }

    private void SpawnPerimeterObjects(Vector2Int pos, RoomData roomData, ChildRoom child, GameObject parent)
    {
        Vector2 spawnOrigin = GetSpawnPos(pos);

        if (!child.edgeRules.upper.BuildInEdge) 
            SpawnObjectsOnEdge(spawnOrigin, roomData, Edges.Upper, parent);
        if (!child.edgeRules.lower.BuildInEdge)
            SpawnObjectsOnEdge(spawnOrigin, roomData, Edges.Lower, parent);
        if (!child.edgeRules.right.BuildInEdge)
            SpawnObjectsOnEdge(spawnOrigin, roomData, Edges.Right, parent);
        if (!child.edgeRules.left.BuildInEdge)
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