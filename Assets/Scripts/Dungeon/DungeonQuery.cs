using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DungeonQueryResults
{
    public DungeonQueryResults()
    {
        hallClaims = new();
        childRooms = new();
    }

    public List<QueryHall> hallClaims;
    public List<Rect> childRooms;
}

[Serializable]
public struct QueryHall
{
    public Vector2 claimPoint;
    public HallwayDirection direction;
}

public class DungeonQuery : MonoBehaviour
{
    public List<Hallway> collidingHalls;
    public List<ChildRoom> collidingRooms;

    public DungeonQueryResults GetQueryResults()
    {
        DungeonQueryResults queryResult = new();

        // turn colliding room data into rects
        if (collidingRooms.Count != 0)
        {
            foreach (ChildRoom room in collidingRooms)
            {
                BoxCollider2D collider = room.gameObject.GetComponent<BoxCollider2D>();

                Rect rect = new(collider.offset, collider.size);
                rect.center += new Vector2(room.transform.position.x, room.transform.position.y);

                queryResult.childRooms.Add(rect);
            }
        }

        if (collidingHalls.Count != 0)
        {
            foreach (Hallway hall in collidingHalls)
            {
                BoxCollider2D collider = hall.gameObject.GetComponent<BoxCollider2D>();

                QueryHall result = new();
                result.direction = hall.direction;
                result.claimPoint = new Vector2(hall.transform.position.x, hall.transform.position.y) + collider.offset;

                queryResult.hallClaims.Add(result);
            }
        }

        return queryResult;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Keep track of what hall claims the room is colliding with
        if (collision.gameObject.layer == LayerMask.NameToLayer("Child Room") &&
            collision.gameObject.TryGetComponent(out ChildRoom room))
        {
            collidingRooms.Add(room);
        }

        // Keep track of what hall claims the room is colliding with
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Hallway Claim") &&
            collision.gameObject.TryGetComponent(out Hallway hall))
        {
            collidingHalls.Add(hall);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Keep track of what hall claims the room is colliding with
        if (collision.gameObject.layer == LayerMask.NameToLayer("Child Room") &&
            collision.gameObject.TryGetComponent(out ChildRoom room))
        {
            collidingRooms.Remove(room);
        }

        // Remove hall claims when they disapear
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Hallway Claim") &&
            collision.gameObject.TryGetComponent(out Hallway hall))
        {
            collidingHalls.Remove(hall);
        }
    }
}
