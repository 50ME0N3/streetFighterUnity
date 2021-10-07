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