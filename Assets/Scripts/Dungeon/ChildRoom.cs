using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildRoom : MonoBehaviour
{
    [SerializeField] List<Hallway> hallways;
    // [SerializeField] EnemySpawnParty spawnParty;
    [SerializeField] int cost;
    public bool enemiesSpawned;

    public bool InValidPosition()
    {
        return true;
    }
}
