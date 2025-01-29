using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyWeights
{
    [SerializeField] GameObject enemy;
    [SerializeField] int weight;
}

[CreateAssetMenu(menuName = "Enemy Spawn Pool")]
public class EnemySpawnPool : ScriptableObject
{
    public List<EnemyWeights> weights;
}
