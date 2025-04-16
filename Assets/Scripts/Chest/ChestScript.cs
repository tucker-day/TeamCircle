using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour

{
    public bool ChestOpen; 

     void Start()
    {
        
    }

     void Update()
    {
       
    }

   

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Wow! A CHEST!");
            for (int i = 0; i < 3; i++)
            {
                GameManager.instance.SpawnWeaponPickup(new Vector3(this.transform.position.x + Random.Range(-2, 2),
                    this.transform.position.y + Random.Range(-2, 2), this.transform.position.z));
            }

            Destroy(this.gameObject);
        }
    }
}