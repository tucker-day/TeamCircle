using System;
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
    [Header("Edge Rules")]
    public EdgeRulesGroup edgeRules = new();
}
