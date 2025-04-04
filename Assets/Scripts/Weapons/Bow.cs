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

    public override void Trigger(Vector2 playerMovementDir)
    {
        int projectiles = 1;
        foreach (int i in projectileIncreaseThresholds)
        {
            if (level >= i) projectiles++;
        }

        for (int i = 0; i < projectiles; i++)
        {
            StartCoroutine(SpawnProjectile(playerMovementDir, i * SPAWN_DELAY));
        }
    }

    private IEnumerator SpawnProjectile(Vector2 direction, float delay)
    {
        yield return new WaitForSeconds(delay);

        bowProjectilePrefab.damage = GetDamage();
        bowProjectilePrefab.speed = GetSpeed();
        
        GameObject instance = Instantiate(bowProjectilePrefab.gameObject, transform.position, Quaternion.identity);

        Vector3 lookAt = instance.transform.position + new Vector3(direction.x, direction.y, 0);

        instance.transform.LookAt(lookAt);
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
        return cooldown + cooldownPerLevel * level;
    }
}
