using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : Equipment
{
    public float rotateSpeed;

    [SerializeField]
    private FireballProjectile OrbitingFireball;

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


    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f,0f, transform.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
    }
    public override void Trigger(Vector2 playerMovementDir)
    {
        OrbitingFireball.damage = GetDamage();
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
        Debug.Log("Fireball is now Level" + level);
    }
    public override void Activate()
    {
        base.Activate();
        OrbitingFireball.gameObject.SetActive(true);
    }
}
