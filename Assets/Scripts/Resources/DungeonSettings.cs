using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonSettings", menuName = "Procedural Generation/Dungeon Settings")]
public class DungeonSettings : ScriptableObject
{
    [Header("Base Settings")]
    [Range(1, 255)] public int maxLength;
    public DungeonTileset tileset;
    public EnemySpawnPool spawnPool;

    [Header("Generation Settings")]
    [Range(0, 1)] public float branchChance;
    [Range(0, 1)] public float allHallChance;
    public int maxBranchDistance;

    [Header("Pathway Settings")]
    [Range(0, 1)] public float openChance;

    [Header("Enemy Settings")]
    public int initialBudget;
    public int budgetIncreasePerDistance;
    public float enemyHealthMult;

    [Header("Boss Settings")]
    public bool bossFloor;
    public GameObject bossObject;
    public EnemySpawnPool bossSpawnPool;

    public void OnValidate()
    {
        if (branchChance + allHallChance > 1.0f)
        {
            Debug.LogError("Branch Chance and All Hall Chance combined are over one on " + name + "!");
        }
    }
}
