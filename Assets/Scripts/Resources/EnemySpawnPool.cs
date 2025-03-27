using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnPool", menuName = "Procedural Generation/Enemy Spawn Pool")]
public class EnemySpawnPool : ScriptableObject
{
    public List<WeightedItem<GameObject>> enemies;
    public int maxWeight;

    public void OnValidate()
    {
        maxWeight = GetMaxWeight(enemies);
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

    public void GetRandomEnemy(out GameObject enemy, out int cost)
    {
        int selection = UnityEngine.Random.Range(0, maxWeight);
        int weightProgress = 0;

        enemy = null;
        cost = 0;

        foreach (WeightedItem<GameObject> item in enemies)
        {
            weightProgress += item.weight;

            if (selection < weightProgress)
            {
                enemy = item.item;
                cost = item.cost;

                break;
            }
        }
    }
}
