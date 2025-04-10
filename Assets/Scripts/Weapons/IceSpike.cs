using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : Equipment
{
    const float SPAWN_DELAY = 0.1f;

    [SerializeField]
    private IceSpikeProjectile IceSpikeProjectilePrefab;

    [SerializeField]
    private int initialDamage;
    [SerializeField]
    private int damagePerLevel;

    [SerializeField]
    private float initialSpeed;
    [SerializeField]
    private float speedPerLevel;

    [SerializeField]
    private float cooldownPerLevel;

    [SerializeField]
    private List<int> projectileIncreaseThresholds;

    public override void Trigger(Vector2 playerMovementDir)
    {
        SpawnProjectile(playerMovementDir);
    }

    private void SpawnProjectile(Vector2 direction)
    {
        IceSpikeProjectilePrefab.damage = GetDamage();
        IceSpikeProjectilePrefab.speed = GetSpeed();
        Debug.Log("Ice Spike Spawned");
        GameObject instance = Instantiate(IceSpikeProjectilePrefab.gameObject, transform.position, Quaternion.identity);
    }

    private int GetDamage()
    {
        return initialDamage + damagePerLevel * level;
    }

    private float GetSpeed()
    {
        return initialSpeed + speedPerLevel * level;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        Debug.Log("Ice Spike is now Level" + level);
    }
}

