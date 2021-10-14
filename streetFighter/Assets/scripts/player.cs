/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	#region Global Variables
	#region Components

	/// <summary>
	/// Barre de vie du joueur
	/// </summary>
	public Healthbar healthBar;

	/// <summary>
	/// Objet qui détecte le contact avec le sol
	/// </summary>
	groundSensor groundSensor;

	/// <summary>
	/// Composant RigidBody du personnage
	/// </summary>
	Rigidbody2D rgbd;

	/// <summary>
	/// Arbre des animations du personnage
	/// </summary>
	Animator anim;
	#endregion

	#region Stats

	/// <summary>
	/// Nombre de points de vie du joueur
	/// </summary>
	int health = 100;

	/// <summary>
	/// Vitesse maximale de course atteignable
	/// </summary>
	const float MAX_SPEED = 2.5f;

	/// <summary>
	/// Vitesse de course du personnage
	/// </summary>
	const float SPEED = 1f;

	/// <summary>
	/// Hauteur de saut du personnage
	/// </summary>
	const float JUMP_HEIGHT = 1;

	/// <summary>
	/// Vitesse de chute rapide
	/// </summary>
	const float FAST_FALL_SPEED = 1.3f;

	/// <summary>
	/// Recul des attaques subies
	/// </summary>
	public Vector2 knockback = Vector2.zero;

	/// <summary>
	/// Nombre de victoires du joueur
	/// </summary>
	public int score = 0;
	#endregion

	#region Cheats Enabled

	/// <summary>
	/// Si la régénération infinie est activée
	/// </summary>
	bool infiniteRegen = false;

	/// <summary>
	/// Si la mort instantanée est activée
	/// </summary>
	public bool instantDeath = false;

	/// <summary>
	/// Si le vol illimité est activé
	/// </summary>
	bool illimitedFly = false;
	#endregion

	#region Cheat Keys

	/// <summary>
	/// Touche activant la régénération infinie
	/// </summary>
	const KeyCode INFINITE_REGEN_KEY = KeyCode.Alpha1;

	/// <summary>
	/// Touche activant la mort instantanée
	/// </summary>
	const KeyCode INSTANT_DEATH_KEY = KeyCode.Alpha2;

	/// <summary>
	/// Touche activant le vol illimité
	/// </summary>
	const KeyCode ILLIMITED_FLY_KEY = KeyCode.Alpha3;
	#endregion

	#region Axis

	/// <summary>
	/// Appui sur la touche de saut
	/// </summary>
	string jumpAxis;

	/// <summary>
	/// Appui sur la touche de déplacement vers la gauche ou la droite
	/// </summary>
	string horizontalAxis;

	/// <summary>
	/// Appui sur la touche d'attaque 1
	/// </summary>
	string attack1Axis;

	/// <summary>
	/// Appui sur la touche d'attaque 2
	/// </summary>
	string attack2Axis;

	/// <summary>
	/// Appui sur la touche de chute rapide
	/// </summary>
	string fastFallAxis;
	#endregion

	#region Booleans

	/// <summary>
	/// Si le joueur courait
	/// </summary>
	bool wasMoving = false;

	/// <summary>
	/// Si le joueur est mort
	/// </summary>
	bool dead = false;
	#endregion

	/// <summary>
	/// Moment où le joueur est mort
	/// </summary>
	float deathTime;

	/// <summary>
	/// Si le jeu est fini et que le menu de fin s'est affiché
	/// </summary>
	bool end = false;
	#endregion

	void Start()
	{
		// Initialisation des variables
		rgbd = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		groundSensor = GetComponentInChildren<groundSensor>();

		if (name == "Player1")
		{
			healthBar = GameObject.FindGameObjectWithTag("SliderP1").GetComponent<Healthbar>();
		}
		else if (name == "Player2")
		{
			healthBar = GameObject.FindGameObjectWithTag("SliderP2").GetComponent<Healthbar>();
		}

		healthBar.SetMaxHealth(health);

		jumpAxis = "Jump" + name;
		horizontalAxis = "Horizontal" + name;
		attack1Axis = "Attack1" + name;
		attack2Axis = "Attack2" + name;
		fastFallAxis = "FastFall" + name;
	}

	void Update()
	{
		#region Inputs
		float jumpInput = Input.GetAxis(jumpAxis);
		float direction = Input.GetAxis(horizontalAxis);
		float attack1Input = Input.GetAxis(attack1Axis);
		float attack2Input = Input.GetAxis(attack2Axis);
		float fastFallInput = Input.GetAxis(fastFallAxis);

		bool invincibleKeyDown = Input.GetKeyDown(INFINITE_REGEN_KEY);
		bool suddenDeathKeyDown = Input.GetKeyDown(INSTANT_DEATH_KEY);
		bool flyKeyDown = Input.GetKeyDown(ILLIMITED_FLY_KEY);
		#endregion

		// Activation/Désactivation des cheats
		if (invincibleKeyDown || suddenDeathKeyDown || flyKeyDown)
		{
			if (invincibleKeyDown)
			{
				infiniteRegen = !infiniteRegen;
			}

			if (suddenDeathKeyDown)
			{
				instantDeath = !instantDeath;
			}

			if (flyKeyDown)
			{
				illimitedFly = !illimitedFly;
			}

			GameObject.Find("Cheat").GetComponent<Text>().text = string.Empty;

			if (infiniteRegen)
			{
				healthBar.Heal(1);

				GameObject.Find("Cheat").GetComponent<Text>().text += "Infinite Regeneration\n";
			}

			if (instantDeath)
			{
				GameObject.Find("Cheat").GetComponent<Text>().text += "Instant Death\n";
			}

			if (illimitedFly)
			{
				GameObject.Find("Cheat").GetComponent<Text>().text += "Illimited Fly\n";
			}

			GameObject.Find("Cheat").GetComponent<Text>().text = GameObject.Find("Cheat").GetComponent<Text>().text.Trim('\n');
		}

		// Régénère a l'infini
		if (infiniteRegen)
		{
			healthBar.Heal(1);
		}

		health = healthBar.GetHealth();

		if (health > 0)
		{
			if (!GameObject.Find("Player2").GetComponent<player>().dead)
			{
				#region Actions
				// Attaque 1
				if (attack1Input > 0)
				{
					anim.SetBool("AttackPunch", true);
				}
				else
				{
					anim.SetBool("AttackPunch", false);
				}

				if (attack2Input > 0)
				{
					anim.SetBool("AttackKick", true);
				}
				else
				{
					anim.SetBool("AttackKick", false);
				}

				// Saut
				if ((groundSensor.Grounded || illimitedFly) && jumpInput > 0)
				{
					rgbd.velocity = new Vector2(rgbd.velocity.x, JUMP_HEIGHT);
					anim.SetBool("Grounded", false);
				}

				if (groundSensor.Grounded)
				{
					anim.SetBool("Grounded", true);
				}
				else
				{
					anim.SetBool("Grounded", false);

					// Chute rapide
					if (fastFallInput > 0 && rgbd.velocity.y <= 0)
					{
						rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y * FAST_FALL_SPEED);
					}
				}
				#endregion
				anim.SetFloat("AirSpeed", rgbd.velocity.y);

				const int PLAYER_TAG_INDEX = 2;
				#region Run
				if (direction > 0)
				{
					transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
					gameObject.transform.GetChild(PLAYER_TAG_INDEX).GetComponent<SpriteRenderer>().flipX = true;
					gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition = new Vector3(gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition.x, gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition.y, 1);
					anim.SetInteger("AnimState", 2);

					if (rgbd.velocity.x < MAX_SPEED)
					{
						rgbd.velocity += new Vector2(direction * SPEED, 0);
					}

					wasMoving = true;
				}
				else if (direction < 0)
				{
					transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
					gameObject.transform.GetChild(PLAYER_TAG_INDEX).GetComponent<SpriteRenderer>().flipX = false;
					gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition = new Vector3(gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition.x, gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition.y, -1);
					anim.SetInteger("AnimState", 2);

					if (rgbd.velocity.x > -MAX_SPEED)
					{
						rgbd.velocity += new Vector2(direction * SPEED, 0);
					}

					wasMoving = true;
				}
				else if (wasMoving)
				{
					anim.SetInteger("AnimState", 0);
					wasMoving = false;
				}
				#endregion

				// Éjection du personnage
				if (knockback != Vector2.zero)
				{
					rgbd.velocity = knockback;
					knockback = Vector2.zero;
				}
			}
		}
		else
		{
			// Mort du joueur
			if (!dead)
			{
				dead = true;
				deathTime = Time.time;
				anim.SetTrigger("Death");

				GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);

				// Sélectionne le joueur tué
				if (name[name.Length - 1] == '1')
				{
					GameObject.Find("Player2").GetComponent<player>().score++;
					GameObject.Find("ScorePlayer2" + name).GetComponent<Text>().text = GameObject.Find("Player2").GetComponent<player>().score.ToString();
					GameObject.Find("Winner").GetComponent<Text>().text = "Player 2 has won";
				}
				else
				{
					GameObject.Find("Player1").GetComponent<player>().score++;
					GameObject.Find("ScorePlayer1" + name).GetComponent<Text>().text = GameObject.Find("Player1").GetComponent<player>().score.ToString();
					GameObject.Find("Winner").GetComponent<Text>().text = "Player 1 has won";
				}
			}
		}

		const float WAIT_TIME = 3;

		if (dead)
		{
			if (Time.time >= deathTime + WAIT_TIME)
			{
				if (!end)
				{
					end = true;

					GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = true;
					GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);

					StartCoroutine(goMainMenu());

					IEnumerator goMainMenu()
					{
						yield return new WaitForSeconds(10);
						SceneManager.LoadScene("Title Screen");
					}
				}
			}
			else
			{
				GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = false;
			}
		}
	}
}