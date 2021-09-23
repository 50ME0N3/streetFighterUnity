using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float Radius = 1;

    private void Update()
    {
        
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SpawnObjectAtRandom();
                Debug.Log("+1 pieces");


            }
        
    }
    private void Start()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quand le joueur touche la pièce 
        if (collision.CompareTag("Player"))
        {
           
            // Remove power up object 
            // attend 0.3 seconde pour detruire la pièce
            
            SpawnObjectAtRandom();
            
           
        }
    }

    void SpawnObjectAtRandom()
    {
         Vector3 randomPos = Random.insideUnitCircle * Radius;
         
         Instantiate(ItemPrefab, randomPos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}
