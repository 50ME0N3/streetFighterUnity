/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using System.Collections;
using UnityEngine;
using System;
using System.Threading;

public class randomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float Radius = 1;
    public static DateTime lastCreation;
    private Semaphore sem;

    private void Update()
    {

    }
    private void Start()
    {
        lastCreation = DateTime.Now;
        sem = new Semaphore(0, 1);

        Invoke("SpawnObjectAtRandom", 15);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quand le joueur touche la pièce 
        if (collision.CompareTag("Player"))
        {
            // Remove power up object 
            // attend 0.3 seconde pour detruire la pi�ce
            
            sem.WaitOne(3);

                Invoke("SpawnObjectAtRandom", 15);
                sem.Release();         
            
        }
    }

    public void SpawnObjectAtRandom()
    {

        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-3f, 3.52f), UnityEngine.Random.Range(-1.44f, -0.782f));

        
        ItemPrefab.transform.position = randomPos;
        ItemPrefab.SetActive(true);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}

   
