using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyWeights
{
    [SerializeField] GameObject enemy;
    [SerializeField] int weight;
    [SerializeField] int cost;
}

[CreateAssetMenu(fileName = "EnemySpawnPool", menuName = "Procedural Generation/Enemy Spawn Pool")]
public class EnemySpawnPool : ScriptableObject
{
    public List<EnemyWeights> weights;
}
