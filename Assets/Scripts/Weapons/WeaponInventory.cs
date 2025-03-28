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
