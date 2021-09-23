/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using UnityEngine;

public class groundSensor : MonoBehaviour
{
	private bool grounded = true;

	public bool Grounded { get => grounded; set => grounded = value; }

	private void OnTriggerStay2D(Collider2D collision)
	{
		Grounded = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Grounded = false;
	}
}