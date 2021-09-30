/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using System.Collections;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
	Animator myAnimation;
	public float multiplier = 2f;

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
}