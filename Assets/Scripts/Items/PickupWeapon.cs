using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class PickupWeapon : Pickup
{
    int weaponValue;

    public WeaponInventory weapons;

    // Start is called before the first frame update
    void Awake()
    {
        base.Start();

        weapons = playerObj.GetComponent<WeaponInventory>();
        weaponValue = Random.Range(0, (int)WeaponType.NUM_WEAPONS);
        Debug.Log("Spawned a weapon upgrade with a weapon value of " + weaponValue);

        if (weapons.GetWeaponSprite((WeaponType)weaponValue) != null)
        {
            GetComponent<SpriteRenderer>().sprite = weapons.GetWeaponSprite((WeaponType)weaponValue);
        }

        // 0 is sword, 1 is bow. Future values will be added when more weapons are added and/or balanced.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("THERE IS A PICKUP HERE!");
            UpgradeWeapon(weapons, weaponValue);

            // Show the next available weapon UI
        FindObjectOfType<WeaponShowUIManager>().ShowSpecificWeapon(weaponValue);

        // Show weapon icon in specific slot
        string uiName = "Weapon" + (weaponValue + 1);
        GameObject uiObj = GameObject.Find(uiName);

        if (uiObj != null)
        {
            uiObj.SetActive(true);

            Transform iconHolder = uiObj.transform.Find("1");
            if (iconHolder != null)
            {
                Image img = iconHolder.GetComponent<Image>();
                Sprite sprite = weapons.GetWeaponSprite((WeaponType)weaponValue);

                if (img != null && sprite != null)
                {
                    img.sprite = sprite;
                    img.color = Color.white;
                }
            }
        }
            Destroy(this.gameObject);
        }
    }
}
