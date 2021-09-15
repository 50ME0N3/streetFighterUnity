/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Gr�goire, Antoine, R�my
 */

using UnityEngine;

public class player : MonoBehaviour
{
	public groundSensor groundSensor;
	private Rigidbody2D rgbd;
	public Healthbar healthbar;
	private SpriteRenderer SpriteRenderer;
	private Animator anim;

	public float speed;
	public float jumpForce;
	private int health;

	void Start()
	{
		rgbd = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

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
		}
	}

	public void Pause()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}