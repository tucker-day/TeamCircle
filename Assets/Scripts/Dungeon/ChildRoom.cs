using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EdgeRulesGroup
{
    public EdgeRules upper;
    public EdgeRules lower;
    public EdgeRules right;
    public EdgeRules left;
}

[Serializable]
public class EdgeRules
{
    [field: SerializeField] public bool BuildInEdge { get; private set; } = false;
    [field: SerializeField] public EdgeType BuiltInEdgeType { get; private set; } = EdgeType.Wall;
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ChildRoom : MonoBehaviour
{
    [Header("Room Settings")]
    public bool spawnEnemies = true;
    [Header("Edge Rules")]
    public EdgeRulesGroup edgeRules = new();

    [HideInInspector]
    public List<GameObject> enemySpawns = new();
    [HideInInspector]
    public List<ChildRoom> chainedRooms;
    [HideInInspector]
    public bool enemiesSpawned;

    public EdgeRules GetRulesByEnum(Edges edge)
    {
        switch (edge)
        {
            case Edges.Upper:
                return edgeRules.upper;
            case Edges.Lower:
                return edgeRules.lower;
            case Edges.Right:
                return edgeRules.right;
            case Edges.Left:
                return edgeRules.left;
            default:
                return null;
        }
    }
}
