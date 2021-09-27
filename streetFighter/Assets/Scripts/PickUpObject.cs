using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class PickUpObject : MonoBehaviour
{
    
    Animator myAnimation;
    public float multiplier = 2f;
    public GameObject gameObject;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quand le joueur touche la pi�ce 
        if (collision.CompareTag("Player"))
        {
            
            PickUp(collision);

            StartCoroutine(CoinDestroy());
            // Remove power up object 
            // attend 0.3 seconde pour detruire la pi�ce
            IEnumerator CoinDestroy()
            {
                yield return new WaitForSeconds(0.7f);
                
                gameObject.SetActive(false);

            }

            
        }
    }

    

    void PickUp(Collider2D Player)
    {

        // Spawn a cool effect 
        
        myAnimation.SetBool("estToucher", true);

        
        // Remove power up object 
        // attend 0.3 seconde pour detruire la pi�ce
       

        // Apply effect to the player
        // - Grandit
        //Player.transform.localScale *= multiplier;
    }
    private void Start()
    {
        myAnimation = GetComponent<Animator>();
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(3);
        // ItemPrefab.SetActive(true);


        gameObject.SetActive(true);

    }







}



   

