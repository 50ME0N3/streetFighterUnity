using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float Radius = 1;

    private void Update()
    {
        
    }
    private void Start()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Quand le joueur touche la pièce 
        if (collision.CompareTag("Player"))
        {
            
            SpawnObjectAtRandom();
        }
    }

    void SpawnObjectAtRandom()
    {
        //  Vector3 randomPos = Random.insideUnitCircle * Radius;
        StartCoroutine(goMainMenu());
        IEnumerator goMainMenu()
        {
            yield return new WaitForSeconds(3.0f);
           // ItemPrefab.SetActive(true);
           
            
        }

            Vector3 randomPos = new Vector3(Random.Range(-10f, +10f), Random.Range(-2f, +7f));
            Debug.Log("x =" + randomPos.x + "y =" + randomPos.y + "z =" + randomPos.z);

            Instantiate(ItemPrefab, randomPos, Quaternion.identity);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}
