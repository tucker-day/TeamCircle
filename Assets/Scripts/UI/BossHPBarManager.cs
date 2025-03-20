using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarManager : MonoBehaviour
{
    [SerializeField]
    private FinalBoss BossStats;
    public Image Square;
    public float healthAmount;

    // Start is called before the first frame update
    void Awake()
    {
        healthAmount = BossStats.HP;
        Debug.Log(healthAmount);
    }

    // Update is called once per frame
    public void Update()
    {
        //healthAmount = playerStats.CurrentHP;
        if (Input.GetKeyDown("i"))
        {
            BossStats.HP -= 100;
        }

        Square.fillAmount = (float)BossStats.HP / BossStats.MaxHP;

    }
}