using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ChildRoom : MonoBehaviour
{
    // [SerializeField]
    // EnemySpawnParty spawnParty;
    public int cost;
    public List<Hallway> hallways;
    public List<Hallway> collidingClaims;

    bool enemiesSpawned;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Keep track of what hall claims the room is colliding with
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hallway Claim") &&
            collision.gameObject.TryGetComponent(out Hallway hall))
        {
            collidingClaims.Add(hall);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Remove hall claims when they disapear
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hallway Claim") &&
            collision.gameObject.TryGetComponent(out Hallway hall))
        {
            collidingClaims.Remove(hall);
        }
    }

    public bool InValidPosition()
    {
        bool validPosition = true;

        // check if the room can connect to claims on current spot
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
                    validPosition = false;
                    break;
                }
            }
        }

        // check if hallways can connect to neighbor rooms
        if (validPosition)
        {
            foreach (Hallway hall in hallways)
            {
                if (hall.collidingRoom != null)
                {
                    bool noConflict = false;

                    foreach (Hallway connection in hall.collidingRoom.hallways)
                    {
                        if (connection.direction == hall.OppositeDir() &&
                            connection.gameObject.transform.position == hall.gameObject.transform.position)
                        {
                            noConflict = true;
                            break;
                        }
                    }

                    if (!noConflict)
                    {
                        validPosition = false;
                        break;
                    }
                }
            }
        }

        return validPosition;
    }

    public void ConnectAllClaims()
    {
        if (collidingClaims.Count != 0)
        {
            foreach (Hallway claim in collidingClaims)
            {
                foreach (Hallway hall in hallways)
                {
                    if (claim.direction == hall.OppositeDir() &&
                        claim.gameObject.transform.position == hall.gameObject.transform.position)
                    {
                        claim.connected = true;
                        hall.connected = true;
                    }
                }
            }
        }
    }
}
