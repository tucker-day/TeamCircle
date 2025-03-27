using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour

{
    public SpriteRenderer playerSprites;
    public BoxCollider2D collider2D;
    public Animator animator;
    public GameObject Bow;

    public float speed = 10;
    public float damage = 15;
    public float range = 23;


    // Start is called before the first frame update
    void Start()
    {
        playerSprites = transform.root.GetComponent<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BowAttack();
        }

        if (playerSprites.flipX == true)
        {
            Bow.transform.rotation = Quaternion.Euler(0, -100, 0);
        }

        else
        {
            Bow.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void BowAttack()
    {
        animator.Play("BowAttack");
        collider2D.enabled = true;
    }
}





