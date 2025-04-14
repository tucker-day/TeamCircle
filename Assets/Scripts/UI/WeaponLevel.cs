using UnityEngine;
using UnityEngine.UI;

public class WeaponLevel : MonoBehaviour
{
    [SerializeField] int weaponLevel = 1;
    public Image[] bars;

    // Colors based on weapon level ranges
    [System.Serializable]
    public struct LevelColor
    {
        public int minLevel;
        public int maxLevel;
        public Color color;
    }

    public LevelColor[] levelColors;

    void Start()
    {
        ShowWeaponLevel();
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = Mathf.Clamp(level, 0, 25);
        HideOtherWeaponBars();
        ShowWeaponLevel();
    }

        void ShowWeaponLevel()
    {
       
        int barsToShow = (weaponLevel - 1) % 5 + 1;
        Color currentColor = GetColorForLevel(weaponLevel);
        currentColor.a = 1f; 

        for (int i = 0; i < bars.Length; i++)
{
    if (i < barsToShow)
    {
        bars[i].gameObject.SetActive(true);
        bars[i].color = currentColor;
    }
    else
    {
        bars[i].gameObject.SetActive(false);
    }
}


    }


    Color GetColorForLevel(int level)
    {
        foreach (var lvlColor in levelColors)
        {
            if (level >= lvlColor.minLevel && level <= lvlColor.maxLevel)
                return lvlColor.color;
        }
        // Default color if no match found
        return Color.white;
    }

    void HideOtherWeaponBars()
    {
        WeaponLevel[] allWeaponLevels = FindObjectsOfType<WeaponLevel>();

        foreach (var weapon in allWeaponLevels)
        {
            if (weapon != this)
            {
                foreach (var bar in weapon.bars)
                {
                    bar.enabled = false;
                }
            }
        }
    }
}
