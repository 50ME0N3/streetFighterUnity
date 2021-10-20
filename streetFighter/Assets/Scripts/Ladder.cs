/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Grégoire, Rémy
 * Description : Détecte si le joueur est près d'une échelle
 */

using UnityEngine;

public class Ladder : MonoBehaviour
{
	/// <summary>
	/// Si le joueur touche l'echelle
	/// </summary>
	public bool isInRange;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isInRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isInRange = false;
		}
	}
}