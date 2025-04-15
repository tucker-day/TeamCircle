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
            GameManager.instance.SpawnWeaponPickup(this.transform.position);
            Destroy(this.gameObject);
        }
    }
}
