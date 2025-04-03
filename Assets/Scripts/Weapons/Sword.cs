using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment
{
    [SerializeField] 
    private SwordProjectile swordProjectilePrefab;

    [SerializeField]
    private int initialDamage;
    [SerializeField]
    private int damagePerLevel;

    private bool flipSword = false;
    
    public override void Trigger(Vector2 playerMovementDir)
    {
        if (playerMovementDir.x != 0)
        {
            flipSword = playerMovementDir.x < 0;
        }

        SpawnProjectile(flipSword, Vector3.zero);
    }

    private void SpawnProjectile(bool flip, Vector3 offset)
    {
        swordProjectilePrefab.damage = GetDamage();
        GameObject instance = Instantiate(swordProjectilePrefab.gameObject, transform.position + offset, Quaternion.identity, gameObject.transform);

        if (flip)
        {
            Vector3 scale = instance.transform.localScale;
            instance.transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
        }
    }

    private int GetDamage()
    {
        return initialDamage + damagePerLevel * level;
    }
}
