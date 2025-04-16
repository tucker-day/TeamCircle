using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarManager : MonoBehaviour
{
    public FinalBoss BossStats;
    public Image Square;

    // Update is called once per frame
    public void Update()
    {
        if (BossStats.isAlive)
        {
            Square.fillAmount = (float)BossStats.hp / BossStats.MaxHP;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}