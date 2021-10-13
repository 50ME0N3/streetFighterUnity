/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using System.Collections;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.UI;


public class randomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float Radius = 1;
    public static DateTime lastCreation;
    private Semaphore sem;
    private int timeWaitCoin = 10;

    private void Update()
    {

    }
    private void Start()
    {
        lastCreation = DateTime.Now;
        sem = new Semaphore(0, 1);

        Invoke("SpawnObjectAtRandom", timeWaitCoin);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quand le joueur touche la pièce 
        if (collision.CompareTag("Player"))
        {
            // Remove power up object 
            // attend 0.3 seconde pour detruire la pi�ce
            
            sem.WaitOne(3);

                Invoke("SpawnObjectAtRandom", timeWaitCoin);
                sem.Release();         
            
        }
    }

    public void SpawnObjectAtRandom()
    {
        int randomPosition = UnityEngine.Random.Range(0, 3);
        if(randomPosition == 0)
        {
            Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-3.097f, -2.512f), UnityEngine.Random.Range(-0.845f, -0.776f));
            ItemPrefab.transform.position = randomPos;
            ItemPrefab.SetActive(true);
        }
        else
        {
            if(randomPosition == 1)
            {
                Vector3 randomPos = new Vector3(UnityEngine.Random.Range(0.155f, 0.597f), UnityEngine.Random.Range(-0.953f, -0.879f));
                ItemPrefab.transform.position = randomPos;
                ItemPrefab.SetActive(true);
            }
            else
            {
                if(randomPosition == 2)
                {
                    Vector3 randomPos = new Vector3(UnityEngine.Random.Range(2.1f, 3.147f), UnityEngine.Random.Range(-1.095f, -0.938f));
                    ItemPrefab.transform.position = randomPos;
                    ItemPrefab.SetActive(true);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}

   
