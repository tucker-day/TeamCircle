using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : Equipment
{
    public GameObject FireballPrefab;
    public GameObject FireballInstance;
    public float rotationSpeed;
    [SerializeField]
    int damage;
    [SerializeField]
    int damageIncrease;//changes per level

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));
    }

    public void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.TryGetComponent<Enemy>(out Enemy enemy) == true)
        {
            enemy.TakeDamage(damage + damageIncrease * level);
        }
    }
    public override void LevelUp()
    {
        base.LevelUp();
        if (level == 1) { 
            FireballInstance = GameObject.Instantiate(FireballPrefab);
        }
    }
    public override void Trigger(Vector2 playerMovementDir)
    {

    }
}
