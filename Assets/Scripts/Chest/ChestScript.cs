using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


                          //Interactable
public class ChestScript : MonoBehaviour
{

    //public Item contents; 
    public bool ChestOpen;
    //public Signal raiseItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
