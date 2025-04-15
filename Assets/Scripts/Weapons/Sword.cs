using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment
{
    const float SPAWN_DELAY = 0.15f;
    const float Y_OFFSET = 1.5f;

    [SerializeField] 
    private SwordProjectile swordProjectilePrefab;

    [SerializeField]
    private int initialDamage;
    [SerializeField]
    private int damagePerLevel;

    [SerializeField]
    private List<int> projectileIncreaseThresholds;

    private bool flipSword = false;
    
    public override void Trigger(Vector2 playerMovementDir)
    {
        print("Player Movement: " + Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Horizontal") > 0.0f)
        { flipSword = false; }
        else if (Input.GetAxis("Horizontal") < 0.0f)
        {  flipSword = true; }

        int projectiles = 1;
        foreach (int i in projectileIncreaseThresholds)
        {
            if (level >= i) projectiles++;
        }

        for (int i = 0; i < projectiles; i++)
        {
            bool flip = flipSword;
            Vector3 offset = new();

            if (i % 2 != 0) flip = !flip;

            int raise = Mathf.FloorToInt((float)i / 2.0f);
            offset.y += raise * Y_OFFSET;

            StartCoroutine(SpawnProjectile(flip, offset, i * SPAWN_DELAY));
        }
    }

    private IEnumerator SpawnProjectile(bool flip, Vector3 offset, float delay)
    {
        yield return new WaitForSeconds(delay);

        swordProjectilePrefab.damage = GetDamage();
        GameObject instance = Instantiate(swordProjectilePrefab.gameObject, transform.position + offset, Quaternion.identity, gameObject.transform);
        AudioManager.instance.PlaySFX(AudioManager.instance.soundEffects[10]);

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

    public override void LevelUp()
    {
        base.LevelUp();
        Debug.Log("Sword is now Level" + level);
    }
}
