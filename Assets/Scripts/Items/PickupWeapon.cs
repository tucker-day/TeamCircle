using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : Pickup
{
    int weaponValue;

    public WeaponInventory weapons;

    // Start is called before the first frame update
    void Awake()
    {
        base.Start();

        weapons = playerObj.GetComponent<WeaponInventory>();
        weaponValue = Random.Range(0, 2);
        Debug.Log("Spawned a weapon upgrade with a weapon value of " + weaponValue);
        // 0 is sword, 1 is bow. Future values will be added when more weapons are added and/or balanced.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("THERE IS A PICKUP HERE!");
            UpgradeWeapon(weapons, weaponValue);
            Destroy(this.gameObject);
        }
    }
}
