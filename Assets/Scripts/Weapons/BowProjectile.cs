using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BowProjectile : MonoBehaviour
{
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public float speed;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}
