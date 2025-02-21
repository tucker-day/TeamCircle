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
    public SpawnRoomResult SpawnRoom(Vector2Int pos, GameObject forceRoom = null)
    {
        // return if position is already claimed
        if (dungeonGrid[pos.x, pos.y] != null)
        {
            return SpawnRoomResult.AlreadyClaimed;
        }

        dungeonGrid[pos.x, pos.y] = new RoomData();

        GameObject room = forceRoom;
        int cost = 0;

        // if no force room was given, get a random room
        if (room == null)
        {
            settings.tileset.GetRandomRoom(out room, out cost);
        }

        GameObject test = Instantiate(room, pos * settings.tileset.tileSize - spawnOffset, Quaternion.identity, gameObject.transform);

        return SpawnRoomResult.Success;
    }
}
