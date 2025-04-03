using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : Equipment
{
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));
        if(level != 0)
        {

        }
    }

    public void OnTriggerEnter2D(Collider2D Enemy)
    {
        if(Enemy.TryGetComponent<Enemy>(out Enemy enemy) == true)
        {
            enemy.TakeDamage(20);
        }
    }
    public override void Trigger(Vector2 playerMovementDir)
    {
        Debug.Log("Sword Attack In Direction: " + playerMovementDir);
    }
}
