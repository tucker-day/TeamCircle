using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public int HP;
    public int MaxHP;
    public bool isAlive;//0 = dead, 1 = life
    // Start is called before the first frame update
    void Awake()
    {
        MaxHP = 1000;
        HP = MaxHP;
        isAlive = true;
    }

    public void TakeDamage(int damage)
    {
        if (isAlive == true)
        {
            HP -= damage;
            if (HP <= 0)
            {
                isAlive = false;
                //anim.SetBool("isAlive", false);
                //GameOver();
                Destroy(gameObject);
            }
        }
    }
}
