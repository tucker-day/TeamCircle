using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnPool", menuName = "Procedural Generation/Enemy Spawn Pool")]
public class EnemySpawnPool : ScriptableObject
{
    public List<WeightedItem<Enemy>> weights;
}
