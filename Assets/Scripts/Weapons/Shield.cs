using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Equipment , IWeaponIconProvider
{
    const float SPAWN_DELAY = 0.1f;

    [SerializeField]
    private ShieldProjectile ShieldProjectilePrefab;

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

    [SerializeField]
    private WeaponLevel levelDisplay;

    [SerializeField]
    private int rotationSpeed;

    public int BounceCount;

    public Sprite GetIcon()
{
    return icon; 
}

    public override void Trigger(Vector2 playerMovementDir)
    {
        SpawnProjectile(playerMovementDir);
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));
    }

    private void SpawnProjectile(Vector2 direction)
    {
        ShieldProjectilePrefab.damage = GetDamage();
        ShieldProjectilePrefab.speed = GetSpeed();

        ShieldProjectilePrefab.BounceCount = level;

        GameObject instance = Instantiate(ShieldProjectilePrefab.gameObject, transform.position, Quaternion.identity);

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

    public override void LevelUp()
    {
        base.LevelUp();
        levelDisplay.SetWeaponLevel(level);
        Debug.Log("Shield is now Level" + level);
    }
}

