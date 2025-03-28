using System;
using UnityEngine;

[Serializable]
public enum WeaponType
{
    Sword, 
    Bow,

    NUM_WEAPONS
}

public class WeaponInventory : MonoBehaviour
{
    [SerializeField]
    private Equipment[] weapons = new Equipment[(int)WeaponType.NUM_WEAPONS];
    private float[] cooldowns = new float[(int)WeaponType.NUM_WEAPONS];
}
