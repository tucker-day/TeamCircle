using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    public EdgeRules upperEdgeRules = new EdgeRules();
    public EdgeRules rightEdgeRules = new EdgeRules();
    public EdgeRules lowerEdgeRules = new EdgeRules();
    public EdgeRules leftEdgeRules = new EdgeRules();
}
