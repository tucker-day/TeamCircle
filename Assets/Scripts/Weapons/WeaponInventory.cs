using System;
using UnityEngine;

[Serializable]
public enum WeaponType
{
    Sword,

    NUM_WEAPONS
}

public class WeaponInventory : MonoBehaviour
{
    [SerializeField]
    private Equipment[] weapons = new Equipment[(int)WeaponType.NUM_WEAPONS];
    private float[] cooldowns = new float[(int)WeaponType.NUM_WEAPONS];
    private PlayerStats player;
    private PlayerMovement movement;

    private void Start()
    {
        player = GetComponent<PlayerStats>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (player.isAlive && Enemy.s_enemyList.Count > 0)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].level > 0)
                {
                    cooldowns[i] += Time.deltaTime;

                    if (cooldowns[i] > weapons[i].GetCooldown())
                    {
                        weapons[i].Trigger(movement.lastMovementDirection);
                        cooldowns[i] -= weapons[i].GetCooldown();
                    }
                }
            }
        }
    }

    private void OnValidate()
    {
        if (weapons.Length != (int)WeaponType.NUM_WEAPONS)
        {
            cooldowns = new float[(int)WeaponType.NUM_WEAPONS];

            Equipment[] temp = new Equipment[(int)WeaponType.NUM_WEAPONS];
            int len = (weapons.Length < (int)WeaponType.NUM_WEAPONS) ? weapons.Length : (int)WeaponType.NUM_WEAPONS;

            for (int i = 0; i < len; i++)
            {
                temp[i] = weapons[i];
            }

            weapons = temp;
        }
    }
}
