using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour//, IDroppable
{
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
            //change it to not destroy later when the inventory works
            Debug.Log("Item Picked Up");
            Destroy(other.gameObject);//move this later
        }
    }
}
