using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public PlayerStats playerStats;
    public Rigidbody2D projectileRb;
    public PlayerMovement playerMovement;

    private Vector2 direction;
    private float lifespan = 2f;
    public float speed;

    public bool isBossProjectile = false;

    void Start()
    {
        speed = 4.0f;
        projectileRb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        direction = (target.transform.position - transform.position).normalized * speed;

        playerStats = target.GetComponent<PlayerStats>();
        playerMovement = target.GetComponent<PlayerMovement>();
        projectileRb.velocity = new Vector2(direction.x, direction.y);

        Destroy(gameObject, lifespan);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile hit " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            playerStats.TakeDamage(5);
            
            if (isBossProjectile == true)
            {
                playerMovement.Freeze();
            }
            Destroy(gameObject);
        }
    }
}
