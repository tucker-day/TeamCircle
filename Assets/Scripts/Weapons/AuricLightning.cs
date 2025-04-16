using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuricLightning : Equipment
{
    const float SPAWN_DELAY = 0.1f;

    [SerializeField]
    private AuricLightningProjectile AuricLightningProjectilePrefab;
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
    private float rotateSpeed;

    public void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
    }
    public override void Trigger(Vector2 playerMovementDir)
    {
        AuricLightningProjectilePrefab.damage = GetDamage();
    }

    private void SpawnProjectile(Vector2 direction)
    {

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
        Debug.Log("Auric Lightning is now Level" + level);
    }
    public override void Activate()
    {
        base.Activate();
        AuricLightningProjectilePrefab.gameObject.SetActive(true);
    }
}

