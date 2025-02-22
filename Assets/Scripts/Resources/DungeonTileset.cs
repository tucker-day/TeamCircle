using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EdgeData
{
    public EdgeGroup upper;
    public EdgeGroup lower;
    public EdgeGroup left;
    public EdgeGroup right;
}

[Serializable]
public struct EdgeGroup
{
    public GameObject hall;
    public GameObject wall;
    public GameObject open;
}

[Serializable]
public struct CornerData
{
    public CornerGroup upperLeft;
    public CornerGroup upperRight;
    public CornerGroup lowerLeft;
    public CornerGroup lowerRight;
}

[Serializable]
public struct CornerGroup
{
    public GameObject both;
    public GameObject horizontal;
    public GameObject vertical;
}

[CreateAssetMenu(fileName = "DungeonTileset", menuName = "Procedural Generation/Dungeon Tileset")]
public class DungeonTileset : ScriptableObject
{
    [Header("Rooms")]
    public GameObject spawnRoom;
    public List<WeightedItem<GameObject>> rooms;
    public List<WeightedItem<GameObject>> bossRooms;
    public List<WeightedItem<GameObject>> endRooms;

    [Header("Edges")]
    public EdgeData edges;

    [Header("Corners")]
    public CornerData corners;

    public Vector2 tileSize;

    [Header("Max Weights")]
    [SerializeField] int maxWeightRooms;
    [SerializeField] int maxWeightBossRooms;
    [SerializeField] int maxWeightEndRooms;

    // runs when values are changed in the inspector
    public void OnValidate()
    {
        // instead of calculating the max costs for everything at runtime, calculate
        // them right when rooms are added
        maxWeightRooms = GetMaxWeight(rooms);
        maxWeightBossRooms = GetMaxWeight(bossRooms);
        maxWeightEndRooms = GetMaxWeight(endRooms);
    }

    public void GetRandomRoom(out GameObject room, out int cost)
    {
        int selection = UnityEngine.Random.Range(0, maxWeightRooms);
        int weightProgress = 0;

        room = null;
        cost = 0;

        foreach (WeightedItem<GameObject> item in rooms)
        {
            weightProgress += item.weight;

            if (selection < weightProgress)
            { 
                room = item.item;
                cost = item.cost;
            }
        }
    }

    private int GetMaxWeight(List<WeightedItem<GameObject>> list)
    {
        if (list.Count == 0) return 0;

        int maxCost = 0;

        foreach (WeightedItem<GameObject> item in list)
        {
            maxCost += item.weight;
        }

        return maxCost;
    }
}