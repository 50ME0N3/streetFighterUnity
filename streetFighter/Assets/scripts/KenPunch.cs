using UnityEngine;

public class KenPunch : MonoBehaviour
{
	public byte kbx;
	public byte kby;
	public byte damagePunch = 15;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			MakeDamage.Hit(collision, damagePunch, new Vector2(kby, kby), gameObject);
		}
	}
}