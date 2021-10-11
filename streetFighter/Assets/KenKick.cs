using UnityEngine;

public class KenKick : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			DoDamage.Hit(collision, 20, new Vector2(4, 8), gameObject);
		}
	}
}