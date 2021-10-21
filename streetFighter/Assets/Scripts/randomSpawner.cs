/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Gabriel
 * Description : Fait apparaitre une pièce aléatoirement si une piece a été atrapée
 */

using System.Collections;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.UI;


public class randomSpawner : MonoBehaviour
{
    // Initialisation des variables 
    public GameObject ItemPrefab;
    public float Radius = 1;
    public static DateTime lastCreation;
    private Semaphore sem;
    private int timeWaitCoin = 10;

   // Méthode qui se lance au start
    private void Start()
    {
        lastCreation = DateTime.Now;

        Invoke("SpawnObjectAtRandom", timeWaitCoin);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quand le joueur touche la pièce 
        if (collision.CompareTag("Player"))
        {
            // Remove power up object 
            // attend 0.3 seconde pour detruire la pi�ce
            

                Invoke("SpawnObjectAtRandom", timeWaitCoin);
                sem.Release();         
            
        }
    }
    /// <summary>
    /// tire aléatoirement entre plusieur plage de position aléatoires
    /// </summary>
	/// <param name="Player"></param>
    public void SpawnObjectAtRandom()
    {
        // Spawn aléatoire de la pièce slon trois espace diffèrent 
        int randomPosition = UnityEngine.Random.Range(0, 3);
        if(randomPosition == 0)
        {
            newPos((-3.097f), (-2.512f), (-0.845f), (-0.776f));
        }
        else
        {
            if(randomPosition == 1)
            {
                newPos((-0.155f), (0.597f), (-0.953f), (-0.879f));
            }
            else
            {
                if(randomPosition == 2)
                {
                    newPos((2.1f), (3.147f), (-1.095f), (-0.938f));
                }
            }
        }
    }

    /// <summary>
    /// Méthodes qui créer une nouvel position et l'applique a la pièce
    /// </summary>
    private void newPos(float x1, float x2, float y1, float y2)
    {
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(x1, x2), UnityEngine.Random.Range(y1, y2));
        ItemPrefab.transform.position = randomPos;
        ItemPrefab.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}

   
