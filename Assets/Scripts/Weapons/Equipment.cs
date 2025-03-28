using System;
using UnityEngine;

[Serializable]
public abstract class Equipment : MonoBehaviour
{
    private int level = 0;
    const int MAX_LEVEL = 25;

    public abstract void Trigger();

    public void LevelUp()
    {
        if (level < MAX_LEVEL)
        {
            level++;
        }
    }
}
