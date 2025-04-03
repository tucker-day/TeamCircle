using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Equipment
{
    public GameObject ShieldPrefab;
    public GameObject ShieldInstance;

    [SerializeField]
    int damage;
    [SerializeField]
    int damageIncrease;//changes per level

    public void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.TryGetComponent<Enemy>(out Enemy enemy) == true)
        {
            enemy.TakeDamage(damage + damageIncrease * level);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Trigger(Vector2 playerMovementDir)
    {

    }
}
