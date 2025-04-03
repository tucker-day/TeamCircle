using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SwordProjectile : MonoBehaviour
{
    const float LIFETIME = 0.5f;

    [HideInInspector]
    public int damage;

    private void Start()
    {
        Destroy(gameObject, LIFETIME);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}
