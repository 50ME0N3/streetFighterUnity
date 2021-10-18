/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine
 * Description : Vérifie si le joueur touche le sol
 */

using System.Collections.Generic;
using UnityEngine;

public class groundSensor : MonoBehaviour
{
	/// <summary>
	/// Si le joueur touche le sol
	/// </summary>
	private bool grounded = true;

	/// <summary>
	/// les GameObjects ayant l'un de ces tags ne sont ignorés
	/// </summary>
	List<string> ignoredTags = new List<string>()
	{
		"InvisibleWall",
		"GroundSensor"
	};

	/// <summary>
	/// Si le joueur touche le sol
	/// </summary>
	public bool Grounded { get => grounded; set => grounded = value; }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!ignoredTags.Contains(collision.tag))
		{
			Grounded = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!ignoredTags.Contains(collision.tag))
		{
			Grounded = false;
		}
	}
}