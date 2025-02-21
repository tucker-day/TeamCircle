using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EdgeRestrictions
{
    [field: SerializeField] public bool canBeWall { get; private set; } = true;
    [field: SerializeField] public bool canBeDoor { get; private set; } = true;
    [field: SerializeField] public bool canBeOpen { get; private set; } = true;
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ChildRoom : MonoBehaviour
{
    [Header("Edge Restrictions")]
    public EdgeRestrictions upperEdgeRestrictions = new EdgeRestrictions();
    public EdgeRestrictions rightEdgeRestrictions = new EdgeRestrictions();
    public EdgeRestrictions lowerEdgeRestrictions = new EdgeRestrictions();
    public EdgeRestrictions leftEdgeRestrictions = new EdgeRestrictions();
}
