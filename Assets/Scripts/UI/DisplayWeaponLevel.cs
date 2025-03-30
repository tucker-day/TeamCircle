using UnityEngine;
using UnityEngine.UI;

public class DisplayWeaponLevel : MonoBehaviour
{
     public int weaponLevel = 1;
    public Image[] bars; 

    void Start()
    {
        ShowWeaponLevel();
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = Mathf.Clamp(level, 1, 5);
        ShowWeaponLevel();
    }

    void ShowWeaponLevel()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            bars[i].enabled = i < weaponLevel;
        }
    }
}
