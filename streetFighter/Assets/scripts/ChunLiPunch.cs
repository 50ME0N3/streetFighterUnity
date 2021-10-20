/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Antoine
 * Description : Coup de poing de Chun-Li
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunLiPunch : MonoBehaviour
{

	public byte kbx;
	public byte kby;
	public byte damagePunch = 15;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			MakeDamage.Hit(collision, damagePunch, new Vector2(kbx, kby), gameObject);
		}
	}
}
