using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject playerObj;
    public Transform playerPos;
    public PlayerStats playerStats;

    // Start is called before the first frame update
    protected void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.transform;
        playerStats = playerObj.GetComponent<PlayerStats>();
    }

    public void Heal()
    {
        playerStats.CurrentHP += playerStats.MaxHP / 2;
        Debug.Log("Picked up a Healing Item!");
        if (playerStats.CurrentHP > playerStats.MaxHP)
        {
            playerStats.CurrentHP = playerStats.MaxHP;
        }
    }

    public void UpgradeWeapon(WeaponInventory weapons, int weaponValue)
    {
        Debug.Log("Picked up a Weapon Upgrade!");
        WeaponType type = (WeaponType)weaponValue;
        weapons.LevelUpWeapon(type);
    }
}
