using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
   private bool pickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!pickedUp && other.CompareTag("Player"))
        {
            pickedUp = true;

            // Tell the UI manager to show next weapon
            FindObjectOfType<WeaponShowUIManager>().ShowNextWeapon();

            // Optionally play sound, animation, etc.
            Debug.Log("Weapon Picked Up!");

            // Then destroy this pickup object
            Destroy(gameObject);
        }
    }
}
