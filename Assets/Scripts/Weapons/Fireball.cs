using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Equipment
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));
    }

    public override void Trigger(Vector2 playerMovementDir)
    {
        Debug.Log("Sword Attack In Direction: " + playerMovementDir);
    }
}
