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
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public float walk;
    public Vector2 lastMovementDirection { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim.SetFloat("velocity", (horizontal * speed + vertical * speed) / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.CurrentHP > 0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            if (horizontal != 0) {

                if (horizontal >= 0)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }
            }

            lastMovementDirection = new Vector2(horizontal, vertical);
            lastMovementDirection.Normalize();
        }
        else { 
            horizontal = 0;
            vertical = 0;
        }
        if (Input.GetKeyDown("h"))
        {
            playerStats.TakeDamage(1020012);
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2 (horizontal * speed, vertical * speed);

        float velocity = (horizontal * speed + vertical * speed) / 2.0f;
        if (velocity < 0)
        {
            velocity = -velocity;
        }
        anim.SetFloat("velocity", velocity);
    }
}
