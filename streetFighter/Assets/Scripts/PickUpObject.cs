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
                yield return new WaitForSeconds(0.4f);
                gameObject.SetActive(false);
            }
        }
    }

    void PickUp(Collider2D Player)
    {
        // Apply effect to the player :
        int applyEffect = Random.Range(0, 2);
        
        Debug.Log(applyEffect);
        if (applyEffect == 0)
        {
            // Fait l'animation
            myAnimation.SetBool("estToucher", true);
            // -Rajoute de la vie
            healthbar.heal(50);
        }
        else
        {
            if(applyEffect == 1)
            {
                myAnimation.SetBool("estToucherPower", true);
                // -Fait plus de dégats
            }
            else
            {
                if(applyEffect == 2)
                {
                    myAnimation.SetBool("estToucherVitesse", true);
                    // -Cours plus vite 
                }
            }
        }
    }
    private void Start()
    {
        myAnimation = GetComponent<Animator>();
    } 
}



   

