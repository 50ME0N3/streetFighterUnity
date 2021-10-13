/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Gr�goire, Antoine, R�my, Gabriel
 */

using UnityEngine;

public class DoDamage : MonoBehaviour
{
	/// <summary>
	/// Donne un coup au joueur touch�
	/// </summary>
	/// <param name="collider">Hit box de la cible</param>
	/// <param name="damage">D�g�ts de l'attaque</param>
	/// <param name="knockback">�jection</param>
	/// <param name="hitBoxObject">Coup</param>
	public static void Hit(Collider2D collider, byte damage, Vector2 knockback, GameObject hitBoxObject)
	{
		Transform player = collider.gameObject.transform;

		if (hitBoxObject.transform.position.x < player.position.x)
		{
			if (hitBoxObject.GetComponentInParent<player>().instantDeath)
			{
				collider.GetComponent<player>().healthBar.takeDamage(100);
			}
			else
			{
				collider.GetComponent<player>().healthBar.takeDamage(damage);
			}

			collider.GetComponent<player>().knockback = knockback;
		}
		else
		{
			if (hitBoxObject.GetComponentInParent<player>().instantDeath)
			{
				collider.GetComponent<player>().healthBar.takeDamage(100);
			}
			else
			{
				collider.GetComponent<player>().healthBar.takeDamage(damage);
			}

			collider.GetComponent<player>().knockback = new Vector2(-knockback.x, knockback.y); 
		}
	}
}