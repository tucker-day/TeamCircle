using System;
using UnityEngine;

[Serializable]
public abstract class Equipment : MonoBehaviour
{
    const int MAX_LEVEL = 25;
    protected int level = 0;
    protected float cooldown = 1.0f;

    public abstract void Trigger(Vector2 playerMovementDir);

    public void LevelUp()
    {
        if (level < MAX_LEVEL)
        {
            level++;
        }
    }

    public bool IsMaxLevel()
    {
        return level == MAX_LEVEL;
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}
