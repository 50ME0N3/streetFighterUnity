using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class PickUpObject : MonoBehaviour
{
    public Healthbar healthbar;
    Animator myAnimation;
    public float multiplier = 2f;
    randomSpawner randomSpawner;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quand le joueur touche la pi�ce 
        if (collision.CompareTag("Player"))
        {

            PickUp(collision);

            StartCoroutine(CoinDestroy());
            // attend 0.7 seconde pour detruire la pi�ce
            IEnumerator CoinDestroy()
            {
                yield return new WaitForSeconds(0.7f);
                gameObject.SetActive(false);

            }
        }
    }

    

    void PickUp(Collider2D Player)
    {

        // Fait l'animation
        
        myAnimation.SetBool("estToucher", true);

        // Remove power up object 

        // Apply effect to the player :

        // -Rajoute de la vie
         
        healthbar.heal(50);
        
        
    }
    private void Start()
    {
        myAnimation = GetComponent<Animator>();
    }
}



   

