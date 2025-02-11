using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    enum Rarities
    {
        COMMON,
        UNCOMMON,
        RARE,
        EPIC,
        LEGENDARY,
        MYTHICAL
    }
    //stats that apply to both classes go here

}

class Weapons : Equipment
{
    //placeholder common weapon stats
    public float attack = 5.0f;
    public float attackSpeed = 2.0f;

}

class Accessories : Equipment
{
    //stubbed out the Accessories class for inheritance
}
