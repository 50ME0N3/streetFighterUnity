using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject pickupEffect;

    public float multiplier = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quand le joueur touche la pièce 
        if (collision.CompareTag("Player"))
        {
            // Remove power up object 
            Destroy(gameObject);
            PickUp(collision);
        }
    }

    void PickUp(Collider2D Player)
    {
        // Spawn a cool effect 
        Instantiate(pickupEffect, transform.position, transform.rotation);

        // Apply effect to the player

        // - Grandit
        Player.transform.localScale *= multiplier;
        
    
        
    }
    


   

    
}



   

