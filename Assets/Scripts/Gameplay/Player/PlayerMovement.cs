using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    private PlayerStats playerStats;
    public float speed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.CurrentHP != 0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        else { 
            horizontal = 0;
            vertical = 0;
        }
        if (Input.GetKeyDown("h"))
        {
            playerStats.CurrentHP = 0f;
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2 (horizontal * speed, vertical * speed);
    }
}
