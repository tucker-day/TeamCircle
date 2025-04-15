using System;
using UnityEngine;

[Serializable]
public enum WeaponType
{
    Sword,
    Bow,
    Fireball,
    Shield,
    IceSpike,
    AuricLightning,

    NUM_WEAPONS
}

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerMovement))]
public class WeaponInventory : MonoBehaviour
{
    [SerializeField]
    private Equipment[] weapons = new Equipment[(int)WeaponType.NUM_WEAPONS];
    private float[] cooldowns = new float[(int)WeaponType.NUM_WEAPONS];
    private PlayerStats player;
    private PlayerMovement movement;

    public void LevelUpWeapon(WeaponType weapon)
    {
        weapons[(int)weapon].LevelUp();
    }

    public Sprite GetWeaponSprite(WeaponType weapon)
    {
        return weapons[(int)weapon].icon;
    }

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
                        cooldowns[i] -= weapons[i].GetCooldown();
                        weapons[i].Trigger(movement.lastMovementDirection);
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
