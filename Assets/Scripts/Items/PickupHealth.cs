using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : Pickup
{
    // Start is called before the first frame update
    void Awake()
    {
        base.Start();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("THERE IS A PICKUP HERE!");
            Heal();
            Destroy(this.gameObject);
        }
    }
}
