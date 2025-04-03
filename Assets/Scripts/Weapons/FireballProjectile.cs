using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.TryGetComponent<Enemy>(out Enemy enemy) == true)
        {
            enemy.TakeDamage(20);
        }
    }
}
