using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildRoom : MonoBehaviour
{
    // [SerializeField]
    // EnemySpawnParty spawnParty;
    public int cost;
    public List<Hallway> hallways;
    public List<Hallway> collidingClaims;

    bool enemiesSpawned;

    private void FixedUpdate()
    {
        Debug.Log(name + " is in a " + (InValidPosition() ? "Valid" : "Invalid") + " position");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Keep track of what hall claims the room is colliding with
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hallway Claim"))
        {
            if (collision.gameObject.TryGetComponent(out Hallway hall))
            {
                collidingClaims.Add(hall);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Remove hall claims when they disapear
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hallway Claim"))
        {
            if (collision.gameObject.TryGetComponent(out Hallway hall))
            {
                collidingClaims.Remove(hall);
            }
        }
    }

    public bool InValidPosition()
    {
        bool result = true;

        if (collidingClaims.Count != 0)
        {
            foreach (Hallway claim in collidingClaims)
            {
                bool canConnect = false;

                foreach (Hallway hall in hallways)
                {
                    if (claim.direction == hall.OppositeDir() && 
                        claim.gameObject.transform.position == hall.gameObject.transform.position)
                    {
                        canConnect = true;
                        break;
                    }
                }

                if (!canConnect)
                {
                    result = false;
                    break;
                }
            }
        }

        return result;
    }
}
