/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Gabriel, Gr�goire, R�my
 * Description : Detecte si le joueur est pr�s d'une echelle
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