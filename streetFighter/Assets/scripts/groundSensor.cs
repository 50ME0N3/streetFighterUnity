/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using UnityEngine;

public class groundSensor : MonoBehaviour
{
	private bool grounded = true;
	string INVISIBLE_WALL_TAG = "InvisibleWall";

	public bool Grounded { get => grounded; set => grounded = value; }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag != INVISIBLE_WALL_TAG)
		{
			Grounded = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag != INVISIBLE_WALL_TAG)
		{
			Grounded = false;
		}
	}
}