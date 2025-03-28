using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public Weapon[] weapons = new Weapon[(int)WeaponType.NUM_WEAPONS];
}
