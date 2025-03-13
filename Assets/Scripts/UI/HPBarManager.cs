using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarManager : MonoBehaviour
{
    [SerializeField]   
    private PlayerStats playerStats;
    public Image Square;
    public float healthAmount; 

    // Start is called before the first frame update
    void Start()
    {
        healthAmount = playerStats.CurrentHP;
        Debug.Log(healthAmount);
    }

    // Update is called once per frame
    public void Update()
    {
        //healthAmount = playerStats.CurrentHP;

        Square.fillAmount = (float)playerStats.CurrentHP / playerStats.MaxHP;
        Debug.Log(Square.fillAmount);
    }
}
