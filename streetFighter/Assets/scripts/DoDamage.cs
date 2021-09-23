/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using UnityEngine;

public class DoDamage : MonoBehaviour
{
	byte damage = 10;

	Vector2 knockback = new Vector2(10, 20);

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			Transform player = collision.gameObject.transform;

			if (gameObject.transform.position.x < player.position.x)
			{
				collision.GetComponent<player>().healthbar.takeDamage(damage);
				collision.GetComponent<player>().knockback = knockback;
			}
			else
			{
				collision.GetComponent<player>().healthbar.takeDamage(damage);
				collision.GetComponent<player>().knockback = new Vector2(-knockback.x, knockback.y);
			}
		}
	}
}