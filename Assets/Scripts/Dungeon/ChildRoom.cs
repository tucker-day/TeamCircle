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
    [field: SerializeField] public bool CanBeWall { get; private set; } = true;
    [field: SerializeField] public bool CanBeDoor { get; private set; } = true;
    [field: SerializeField] public bool CanBeOpen { get; private set; } = true;
    [SerializeField] public GameObject ForcedSpawn = null;
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ChildRoom : MonoBehaviour
{
    [Header("Edge Rules")]
    public EdgeRulesGroup edgeRules = new();
}
