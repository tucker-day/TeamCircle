using System;
using UnityEngine;

[Serializable]
public abstract class Equipment : MonoBehaviour
{
    const int MAX_LEVEL = 25;
    public int level { get; protected set; } = 0;
    [SerializeField] protected float cooldown = 1.0f;
    [field: SerializeField] public Sprite icon { get; private set; }

    public abstract void Trigger(Vector2 playerMovementDir);

    private bool activated = false;
    public virtual void LevelUp()
    {
        if (level < MAX_LEVEL) level++;

        if (level >= 1 && !activated)
        {
            activated = true;
            Activate();
        }
    }

    public virtual void Activate() { }

    public bool IsMaxLevel()
    {
        return level == MAX_LEVEL;
    }

    public virtual float GetCooldown()
    {
        return cooldown;
    }
}
