/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
	public groundSensor groundSensor;
	private Rigidbody2D rgbd;
	public Healthbar healthbar;
	private Animator anim;

	private int health;

	public float speed = 13;
	public float jumpForce = 20;
	public float fastFallSpeed = 1.2f;

	void Start()
	{
		rgbd = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();

		if (name == "Player1")
		{
			healthbar = GameObject.FindGameObjectWithTag("SliderP1").GetComponent<Healthbar>();
		}

		else if (name == "Player2")
		{
			healthbar = GameObject.FindGameObjectWithTag("SliderP2").GetComponent<Healthbar>();
		}

		healthbar.SetMaxHealth(100);
	}

	void Update()
	{
		float jumpInput = Input.GetAxis("Jump" + name);
		float direction = Input.GetAxis("Horizontal" + name);
		float attackInput = Input.GetAxis("Attack" + name);
		float fastFallInput = Input.GetAxis("FastFall" + name);

		health = healthbar.getHealth();

		if (health > 0)
		{
			if (attackInput > 0)
			{
				anim.SetBool("Attack", true);
			}

			if (groundSensor.Grounded && jumpInput > 0)
			{
				rgbd.velocity = new Vector2(rgbd.velocity.x, jumpForce);
				anim.SetBool("Grounded", false);
			}

			if (groundSensor.Grounded)
			{
				anim.SetBool("Grounded", groundSensor.Grounded);
			}
			else
			{
				anim.SetBool("Grounded", false);
				
				if (fastFallInput > 0 && rgbd.velocity.y <= 0)
				{
					rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y * fastFallSpeed);
				}
			}

			anim.SetFloat("AirSpeed", rgbd.velocity.y);

			if (direction > 0)
			{
				transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z,transform.rotation.w);
				gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().flipX = true;
				gameObject.transform.GetChild(2).transform.localPosition = new Vector3(gameObject.transform.GetChild(2).transform.localPosition.x, gameObject.transform.GetChild(2).transform.localPosition.y, 1);
				anim.SetInteger("AnimState", 2);

				rgbd.velocity = new Vector2(direction * speed, rgbd.velocity.y);
			}
			else if (direction < 0)
			{
				transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
				gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().flipX = false;
				gameObject.transform.GetChild(2).transform.localPosition = new Vector3(gameObject.transform.GetChild(2).transform.localPosition.x, gameObject.transform.GetChild(2).transform.localPosition.y, -1);
				anim.SetInteger("AnimState", 2);
				rgbd.velocity = new Vector2(direction * speed, rgbd.velocity.y);
			}
			else
			{
				anim.SetInteger("AnimState", 0);
				rgbd.velocity = new Vector2(0, rgbd.velocity.y);
			}
		}
		else
		{
			anim.SetBool("Death", true);
			rgbd.velocity = new Vector2(0, rgbd.velocity.y);
			SceneManager.LoadScene("EcranWin");
		}
	}
}