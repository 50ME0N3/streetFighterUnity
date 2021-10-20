using UnityEngine;

public class KenKick : MonoBehaviour
{
	public byte kbx;
	public byte kby;
    public byte damageKick = 20;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			MakeDamage.Hit(collision, damageKick, new Vector2(kbx, kby), gameObject);
		}
	}
}