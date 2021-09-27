/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
	private Rigidbody2D rgbd;
	private Animator anim;
	public groundSensor groundSensor;
	public Healthbar healthbar;
	public GameObject ecranWin;

	private int health;

	float maxSpeed = 15;

	float speed = 0.75f;
	public float jumpForce = 20;
	public float fastFallSpeed = 1.05f;
	bool wasMoving = false;
	bool dead = false;

	bool cheating = false;

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

	void Update()
	{
		float jumpInput = Input.GetAxis("Jump" + name);
		float direction = Input.GetAxis("Horizontal" + name);
		float attackInput = Input.GetAxis("Attack" + name);
		float fastFallInput = Input.GetAxis("FastFall" + name);

		bool cheat = Input.GetKeyDown(KeyCode.LeftControl);

		if (cheat)
		{
			cheating = !cheating;

			GameObject.Find("Canvas").transform.Find("Cheat").gameObject.SetActive(cheating);
		}

		if (cheating)
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