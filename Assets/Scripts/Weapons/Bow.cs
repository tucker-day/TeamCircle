using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Equipment
{
    const float SPAWN_DELAY = 0.1f;

    [SerializeField]
    private BowProjectile bowProjectilePrefab;

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

    private int burstCount;

    public override void Trigger(Vector2 playerMovementDir)
    {
        if (burstCount <= 0)
        {
            Debug.Log("Burst Init");
            foreach (int i in projectileIncreaseThresholds)
            {
                if (level >= i) burstCount++;
            }
        }
        else
        {
            burstCount--;
        }

        SpawnProjectile(playerMovementDir);
    }

    private void SpawnProjectile(Vector2 direction)
    {
        bowProjectilePrefab.damage = GetDamage();
        bowProjectilePrefab.speed = GetSpeed();
        
        GameObject instance = Instantiate(bowProjectilePrefab.gameObject, transform.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[18]);

        float angle = Vector2.Angle(Vector2.right, direction);
        if (direction.y < 0)
        {
            angle *= -1;
        }
        instance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private int GetDamage()
    {
        return initialDamage + damagePerLevel * level;
    }

    private float GetSpeed()
    {
        return initialSpeed + speedPerLevel * level;
    }

    public override float GetCooldown()
    {
        if (burstCount > 0)
        {
            return SPAWN_DELAY;
        }
        else
        {
            return cooldown + cooldownPerLevel * level;
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
        Debug.Log("Bow is now Level" + level);
    }
}
