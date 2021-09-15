/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Gr�goire, Antoine, R�my
 */

using UnityEngine;

public class player : MonoBehaviour
{
	public groundSensor groundSensor;
	private Rigidbody2D rgbd;
	private Healthbar healthbar;
	private SpriteRenderer SpriteRenderer;
	private Animator anim;

	public float speed;
	public float jumpForce;
	private int health;
	void Start()
	{
		rgbd = gameObject.GetComponent<Rigidbody2D>();
		SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

		if (Input.GetKeyDown(KeyCode.Space))
		{
			healthbar.takeDamage(10);
		}

		health = healthbar.getHealth();

		if (health > 0)
		{
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
				SpriteRenderer.flipX = true;
				anim.SetInteger("AnimState", 2);
				rgbd.velocity = new Vector2(direction * speed, rgbd.velocity.y);
			}
			else if (direction < 0)
			{
				SpriteRenderer.flipX = false;
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