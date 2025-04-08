using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ShieldProjectile : MonoBehaviour
{
    const float LIFETIME = 5.0f;

    [HideInInspector]
    public int damage;
    [HideInInspector]
    public float speed;

    public float endTime;
    int rotationSpeed;

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
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));
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
