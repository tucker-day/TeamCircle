using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats playerStats;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    Rigidbody2D body;

    float horizontal;
    float vertical;

    private bool canMove;
    public float speed = 5.0f;
    private float frozenSpeed;
    private float resSpeed;

    public float walk;
    public Vector2 lastMovementDirection { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        canMove = true;
        resSpeed = speed;
        frozenSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.CurrentHP > 0 && canMove)
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

            Vector2 temp = new Vector2(horizontal, vertical);
            if (temp.sqrMagnitude > 0)
            {
                lastMovementDirection = temp.normalized;
            }
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

        float velocity = (horizontal * speed) + (vertical * speed) / 2.0f;

        if (velocity != 0)
        {
            if (velocity < 0)
            {
                velocity = -velocity;
            }

            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        anim.SetFloat("velocity", velocity);
    }

    public void Freeze()
    {
        speed = frozenSpeed;
        anim.speed = 0;
        canMove = false;

        StartCoroutine("Unfreeze");
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(2);
        speed = resSpeed;
        anim.speed = 1;
        canMove = true;
    }
}
