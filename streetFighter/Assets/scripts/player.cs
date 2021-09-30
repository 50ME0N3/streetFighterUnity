/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

#region using
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion

public class player : MonoBehaviour
{
    private Rigidbody2D rgbd;
	private Animator anim;

    public groundSensor groundSensor;
	public Healthbar healthbar;
	public GameObject ecranWin;

	private int health;

	float maxSpeed = 2.5f;

	float speed = 0.5F;
	float jumpForce = 11;
	float fastFallSpeed = 1.2f;
	bool wasMoving = false;
	bool dead = false;

	// Cheats
	bool invincible = false;
	public bool suddenDeath = false;
	bool fly = false;

	public Vector2 knockback = Vector2.zero;

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
	/// <summary>
    /// Tout
    /// </summary>
	void Update()
	{
		float jumpInput = Input.GetAxis("Jump" + name);
		float direction = Input.GetAxis("Horizontal" + name);
		float attackInput = Input.GetAxis("Attack" + name);
		float fastFallInput = Input.GetAxis("FastFall" + name);

		bool invincibleKeyDown = Input.GetKeyDown(KeyCode.Alpha1);
		bool suddenDeathKey = Input.GetKeyDown(KeyCode.Alpha2);
		bool flyKey = Input.GetKeyDown(KeyCode.Alpha3);

		if (invincibleKeyDown || suddenDeathKey || flyKey)
		{
			if (invincibleKeyDown)
			{
				invincible = !invincible;
			}

			if (suddenDeathKey)
			{
				suddenDeath = !suddenDeath;
			}

			if (flyKey)
			{
				fly = !fly;
			}

			GameObject.Find("Cheat").GetComponent<Text>().text = string.Empty;

			if (invincible)
			{
				healthbar.heal(1);

				GameObject.Find("Cheat").GetComponent<Text>().text += "Invincible\r\n";
			}

			if (suddenDeath)
			{
				GameObject.Find("Cheat").GetComponent<Text>().text += "Sudden Death\r\n";
			}

			if (fly)
			{
				GameObject.Find("Cheat").GetComponent<Text>().text += "Fly";
			}
		}

		if (invincible)
		{
			healthbar.takeDamage(-1);
		}

		health = healthbar.getHealth();

		if (health > 0)
		{
			if (attackInput > 0)
			{
				anim.SetBool("Attack", true);
			}

			if ((groundSensor.Grounded || fly) && jumpInput > 0)
			{
				rgbd.velocity = new Vector2(rgbd.velocity.x, jumpForce);
				anim.SetBool("Grounded", false);
			}

			if (groundSensor.Grounded)
			{
				anim.SetBool("Grounded", true);
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
				transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
				gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().flipX = true;
				gameObject.transform.GetChild(2).transform.localPosition = new Vector3(gameObject.transform.GetChild(2).transform.localPosition.x, gameObject.transform.GetChild(2).transform.localPosition.y, 1);
				anim.SetInteger("AnimState", 2);

				if (rgbd.velocity.x < maxSpeed)
				{
					rgbd.velocity += new Vector2(direction * speed, 0);
				}

				wasMoving = true;
			}
			else if (direction < 0)
			{
				transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
				gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().flipX = false;
				gameObject.transform.GetChild(2).transform.localPosition = new Vector3(gameObject.transform.GetChild(2).transform.localPosition.x, gameObject.transform.GetChild(2).transform.localPosition.y, -1);
				anim.SetInteger("AnimState", 2);

				if (rgbd.velocity.x > -maxSpeed)
				{
					rgbd.velocity += new Vector2(direction * speed, 0);
				}

				wasMoving = true;
			}
			else if (wasMoving)
			{
				anim.SetInteger("AnimState", 0);
				wasMoving = false;
			}

			if (knockback != Vector2.zero)
			{
				rgbd.velocity = knockback;
				knockback = Vector2.zero;
			}
		}
		else
		{
			if (!dead)
			{
				dead = true;
				anim.SetTrigger("Death");
				GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
				GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);

                if (name[name.Length - 1] == '1')
				{
					GameObject.Find("Winner").GetComponent<Text>().text = "Le joueur 2 a gagne";
				}
				else
				{
					GameObject.Find("Winner").GetComponent<Text>().text = "Le joueur 1 a gagne";
				}

                StartCoroutine(goMainMenu());

                IEnumerator goMainMenu()
                {
                    yield return new WaitForSeconds(10.0f);
                    SceneManager.LoadScene("Title Screen");
                }
            }
		}
	}
}