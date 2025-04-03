using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonSettings", menuName = "Procedural Generation/Dungeon Settings")]
public class DungeonSettings : ScriptableObject
{
    [Header("Base Settings")]
    public int maxLength;
    public DungeonTileset tileset;
    public List<EnemySpawnPool> spawnPools;

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
    public EnemySpawnPool bossSpawnPool;

    public void OnValidate()
    {
        // cap max length at 255
        if (maxLength > 255)
        {
            maxLength = 255;
        }
    }
}
