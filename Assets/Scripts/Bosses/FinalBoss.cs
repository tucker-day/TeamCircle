using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public int HP;
    public int MaxHP;
    // Start is called before the first frame update
    void Awake()
    {
        MaxHP = 1000;
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
