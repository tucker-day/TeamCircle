using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BowProjectile : MonoBehaviour
{
    const float LIFETIME = 10.0f;

    [HideInInspector]
    public int damage;
    [HideInInspector]
    public float speed;

    public float endTime;

    public void Start()
    {
        endTime = Time.time + LIFETIME;
    }

    private void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime * transform.right;

        if (Time.time > endTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
