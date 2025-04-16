using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShowUIManager : MonoBehaviour
{
    [Tooltip("Assign Weapon1 to Weapon6 in order.")]
    public List<GameObject> weaponUIObjects;

    private HashSet<int> shownWeaponIndices = new HashSet<int>();

    void Start()
    {
        
        foreach (GameObject ui in weaponUIObjects)
        {
            if (ui != null)
                ui.SetActive(false);
        }

        // Show only the first weapon slot (e.g., Sword)
        ShowSpecificWeapon(0);
    }

    public void ShowSpecificWeapon(int weaponIndex)
    {
        if (!shownWeaponIndices.Contains(weaponIndex))
        {
            shownWeaponIndices.Add(weaponIndex);

            if (weaponIndex >= 0 && weaponIndex < weaponUIObjects.Count)
            {
                weaponUIObjects[weaponIndex].SetActive(true);
                Debug.Log($"Showing Weapon{weaponIndex + 1}");
            }
            else
            {
                Debug.LogWarning($" Weapon index {weaponIndex} out of range");
            }
        }
    }
}
