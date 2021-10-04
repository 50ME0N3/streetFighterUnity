/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using UnityEngine;

public class DoDamage : MonoBehaviour
{
	byte damage = 10;
	public byte kbx = 10;
	public byte kby = 20;

	Vector2 knockback = new Vector2(4, 8);

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			Transform player = collision.gameObject.transform;

			if (gameObject.transform.position.x < player.position.x)
			{
				if (GetComponentInParent<player>().instantDeath)
				{
					collision.GetComponent<player>().healthBar.takeDamage(100);
				}
				else
				{
					collision.GetComponent<player>().healthBar.takeDamage(damage);
				}

				collision.GetComponent<player>().knockback = knockback;
			}
			else
			{
				if (GetComponentInParent<player>().instantDeath)
				{
					collision.GetComponent<player>().healthBar.takeDamage(100);
				}
				else
				{
					collision.GetComponent<player>().healthBar.takeDamage(damage);
				}

				collision.GetComponent<player>().knockback = new Vector2(-knockback.x, knockback.y);
			}
		}
	}
}