/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Gr�goire, Antoine, R�my, Gabriel
 */

using UnityEngine;

public class DoDamage : MonoBehaviour
{
	byte damage = 10;

	Vector2 knockbackLeft = new Vector2(10, 20);
	Vector2 knockbackRight = new Vector2(-10, 20);

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			Transform player = collision.gameObject.transform;

			if (gameObject.transform.position.y > player.position.y)
			{
				collision.GetComponent<player>().healthbar.takeDamage(damage);
				//collision.attachedRigidbody.velocity = knockbackLeft;
				collision.GetComponent<player>().knockback = knockbackLeft;
			}
			else
			{
				collision.GetComponent<player>().healthbar.takeDamage(damage);
				//collision.attachedRigidbody.velocity = knockbackRight;
				collision.GetComponent<player>().knockback = knockbackRight;
			}
		}
	}
}