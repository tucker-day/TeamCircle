using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonTileset", menuName = "Procedural Generation/Dungeon Tileset")]
public class DungeonTileset : ScriptableObject
{
    [Header("Rooms")]
    public GameObject spawnRoom;
    public List<WeightedItem<GameObject>> rooms;
    public List<WeightedItem<GameObject>> bossRooms;
    public List<WeightedItem<GameObject>> endRooms;

    [Header("Halls")]
    public GameObject upperHall;
    public GameObject rightHall;
    public GameObject lowerHall;
    public GameObject leftHall;

    [Header("Walls")]
    public GameObject upperWall;
    public GameObject rightWall;
    public GameObject lowerWall;
    public GameObject leftWall;

    [Header("Corners")]
    public GameObject upperLeftCorner;
    public GameObject upperRightCorner;
    public GameObject lowerLeftCorner;
    public GameObject lowerRightCorner;

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
        int selection = Random.Range(0, maxWeightRooms);
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