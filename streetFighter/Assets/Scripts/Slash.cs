/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 * Description : Pas utiliser (evite un srash du jeux lorsque ont utilise le HeevyBandit)
 */

using UnityEngine;

public class Slash : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			MakeDamage.Hit(collision, 10, new Vector2(4, 8), gameObject);
		}
	}
}