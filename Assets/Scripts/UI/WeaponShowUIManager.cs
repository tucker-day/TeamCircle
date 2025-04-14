using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShowUIManager : MonoBehaviour
{
   public List<GameObject> weaponUIObjects; 
    private int WeaponShowing = 1; 

    void Start()
    {
        UpdateWeaponShown();
    }

//this is just for checking if Weapon on UI is showing correctly or not.
    void Update()
{
    if (Input.GetKeyDown(KeyCode.P))
    {
        ShowNextWeapon();
    }
}


    public void ShowNextWeapon()
    {
        if (WeaponShowing < weaponUIObjects.Count)
        {
            WeaponShowing++;
            UpdateWeaponShown();
        }
    }

    void UpdateWeaponShown()
    {
        for (int i = 0; i < weaponUIObjects.Count; i++)
        {
            weaponUIObjects[i].SetActive(i < WeaponShowing);
        }
    }
}
