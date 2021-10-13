/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using UnityEngine;

public class MakeDamage : MonoBehaviour
{
	/// <summary>
	/// Donne un coup au joueur touché
	/// </summary>
	/// <param name="collider">Hit box de la cible</param>
	/// <param name="damage">Dégâts de l'attaque</param>
	/// <param name="knockback">Éjection</param>
	/// <param name="hitBoxObject">Coup</param>
	public static void Hit(Collider2D collider, byte damage, Vector2 knockback, GameObject hitBoxObject)
	{
		// Position du joueur
		Transform player = collider.gameObject.transform;

		// Si l'attaquant est a gauche de la cible
		if (hitBoxObject.transform.position.x < player.position.x)
		{
			// Inflige les dégâts à la cible
			if (hitBoxObject.GetComponentInParent<player>().instantDeath)
			{
				collider.GetComponent<player>().healthBar.TakeDamage(100);
			}
			else
			{
				collider.GetComponent<player>().healthBar.TakeDamage(damage);
			}

			// Donne du recul à la cible
			collider.GetComponent<player>().knockback = knockback;
		}
		else
		{
			// Inflige les dégâts à la cible
			if (hitBoxObject.GetComponentInParent<player>().instantDeath)
			{
				collider.GetComponent<player>().healthBar.TakeDamage(100);
			}
			else
			{
				collider.GetComponent<player>().healthBar.TakeDamage(damage);
			}

			// Donne du recul à la cible
			collider.GetComponent<player>().knockback = new Vector2(-knockback.x, knockback.y); 
		}
	}
}