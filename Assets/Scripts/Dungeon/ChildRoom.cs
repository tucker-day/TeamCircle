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
    public Vector2[] spawnPoints;
    [Header("Edge Rules")]
    public EdgeRulesGroup edgeRules = new();

    // [HideInInspector]
    public List<GameObject> enemySpawns = new();
    [HideInInspector]
    public List<ChildRoom> chainedRooms;
    [HideInInspector]
    public List<GameObject> hallBlockers = new();
    [HideInInspector]
    public bool enemiesSpawned;

    private List<GameObject> minimapObjects = new();
    private bool revealed = false;

    void Awake()
    {
        chainedRooms = null;
    }

    private void Start()
    {
        RecursivelyAddChildrenToMinimapList(gameObject);
    }

    private void RecursivelyAddChildrenToMinimapList(GameObject main)
    {
        int numChildren = main.transform.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            GameObject child = main.transform.GetChild(i).gameObject;

            RecursivelyAddChildrenToMinimapList(child);

            if (child.layer == LayerMask.NameToLayer("Minimap"))
            {
                if (spawnEnemies) child.SetActive(false);
                minimapObjects.Add(child);
            }
        }
    }

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        revealed = true;
        foreach (GameObject mini in minimapObjects)
        {
            mini.SetActive(true);
        }

        if (spawnEnemies && !enemiesSpawned && other.gameObject.CompareTag("Player"))
        {
            enemiesSpawned = true;
            Debug.Log("Spawn some enemies!");

            foreach (ChildRoom room in chainedRooms)
            {
                Vector2 roomPos = room.gameObject.transform.position;
                foreach (GameObject door in room.hallBlockers)
                {
                    door.SetActive(true);
                }

                room.enemiesSpawned = true;
                foreach (GameObject enemy in room.enemySpawns)
                {
                    Vector2 enemyPos = new Vector2(roomPos.x + room.spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].x,
                        roomPos.y + room.spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].y);
                    GameManager.instance.SpawnEnemy(enemy, enemyPos);
                }
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!revealed)
        {
            revealed = true;
            foreach (GameObject mini in minimapObjects)
            {
                mini.SetActive(true);
            }
        }

        if (other.gameObject.CompareTag("Player") && Enemy.s_enemyList.Count == 0)
        {
            foreach (ChildRoom room in chainedRooms)
            {
                foreach (GameObject door in room.hallBlockers)
                {
                    door.SetActive(false);
                }
            }
        }
    }
}
