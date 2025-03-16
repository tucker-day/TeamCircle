using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D projectileRb;
    public Transform launchPoint;
    public float speed;

    public GameObject target;

    public float lifespan;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
