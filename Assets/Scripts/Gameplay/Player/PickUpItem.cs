using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour//, IDroppable
{
    private PlayerStats playerStats;

    private void Start()
    {
        //once its a singleton then call register on start with this .gameobject
        //DroppableRegistry

        //DroppableRegistry.gameobject();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            playerStats.CurrentHP = playerStats.MaxHP / 2;
            Debug.Log("Healing");
            if(playerStats.MaxHP < playerStats.CurrentHP)
            {
                playerStats.CurrentHP = playerStats.MaxHP;
            }
            Debug.Log("Item Picked Up");
            Destroy(other.gameObject);
        }
    }
}
