/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Gr�goire, Antoine, R�my, Gabriel
 */

using UnityEngine;

public class Slash : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			DoDamage.Hit(collision, 10, new Vector2(4, 8), gameObject);
		}
	}
}