using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Transform Hit; 
    public SpriteRenderer playerSprites;
    public BoxCollider2D collider2D;
    public Animator animator;
    public GameObject Sword;


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
        if(Input.GetKeyDown(KeyCode.Space))
        {
           SwordAttack();
        }

        if (playerSprites.flipX == true)
        {
            Sword.transform.rotation = Quaternion.Euler(0, -100, 0); 
        }

        else
        {
            Sword.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void SwordAttack()
    {
        animator.Play("SwordAttack");
        collider2D.enabled = true;
        Collider2D[] DamageEnemy = Physics.OverlapCircleAll(Hit.position, range);

        RaycastHit2D Hit = Physics2D.BoxCast(damage.position, new Vector2(range, 1.5f), 0f, Vector2.down, range); 
       
    }

    
}





/*  references: 
 *  https://www.youtube.com/watch?v=aMO0ZyWWF5k
 *  https://www.youtube.com/watch?v=ZtP12Pcoz8k&t=440s
 *  https://www.youtube.com/watch?v=AXkaqW3E9OI 
 
 
 */
