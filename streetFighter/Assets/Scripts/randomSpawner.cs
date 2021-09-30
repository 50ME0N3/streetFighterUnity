/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using System.Collections;
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
		StartCoroutine(respawn());
		// Remove power up object 
		// attend 0.3 seconde pour detruire la pi�ce
		IEnumerator respawn()
		{
			yield return new WaitForSeconds(3f);
		}

		Vector3 randomPos = new Vector3(Random.Range(-3f, 3.52f), Random.Range(-1.44f, -0.782f));
		Debug.Log("x =" + randomPos.x + "y =" + randomPos.y + "z =" + randomPos.z);

		Instantiate(gameObject, randomPos, Quaternion.identity);
		ItemPrefab.SetActive(true);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(this.transform.position, Radius);
	}
}