using UnityEngine;

public class KenPunch : MonoBehaviour
{
    public byte damagePunch = 15;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			DoDamage.Hit(collision, damagePunch, new Vector2(4, 8), gameObject);
		}
	}
}