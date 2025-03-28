using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public Rigidbody2D projectileRb;

    public float speed;
    public float lifespan = 2f;

    void Start()
    {
        projectileRb = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        
    }

    // Prototype function
    void FixedUpdate()
    {
        //projectileRb.velocity = direction * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile hit " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
