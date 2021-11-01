/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 * Description : Script relatif a tout ce qui touche au déplacement du joueur 
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
	const float MAX_SPEED = 2;

	/// <summary>
	/// Vitesse de course du personnage
	/// </summary>
	public float speed = 0.5f;

	/// <summary>
	/// Hauteur de saut du personnage
	/// </summary>
	const float JUMP_HEIGHT = 6;

	/// <summary>
	/// Vitesse de chute rapide
	/// </summary>
	const float FAST_FALL_SPEED = 1.3f;

	/// <summary>
	/// Recul des attaques subies
	/// </summary>
	public Vector2 knockback = Vector2.zero;
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

	/// <summary>
	/// Si le joueur a activé la chute rapide
	/// </summary>
	bool isFastFalling = false;

	/// <summary>
	/// Si le jeu est fini et que le menu de fin s'est affiché
	/// </summary>
	bool end = false;
	#endregion

	/// <summary>
	/// Moment où le joueur est mort
	/// </summary>
	float deathTime;
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
		#endregion

		// Détection du sol
		if (groundSensor.Grounded)
		{
			anim.SetBool("Grounded", true);
		}
		else
		{
			anim.SetBool("Grounded", false);
		}

		health = healthBar.Health;

		if (!end)
		{
			if (health > 0 && !GameObject.Find("Timer").GetComponent<Timer>().tie)
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

					// Attaque 2
					if (attack2Input > 0)
					{
						anim.SetBool("AttackKick", true);
					}
					else
					{
						anim.SetBool("AttackKick", false);
					}

					// Saut
					if ((groundSensor.Grounded) && jumpInput > 0)
					{
						rgbd.velocity = new Vector2(rgbd.velocity.x, JUMP_HEIGHT);
						anim.SetBool("Grounded", false);
						isFastFalling = false;
					}

					// Chute rapide
					if ((!groundSensor.Grounded || jumpInput > 0) && fastFallInput > 0)
					{
						isFastFalling = true;
					}

					if (isFastFalling && rgbd.velocity.y <= 0)
					{
						rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y * FAST_FALL_SPEED);
					}
					#endregion

					#region Run
					const int PLAYER_TAG_INDEX = 2;

					if (direction > 0)
					{
						transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
						gameObject.transform.GetChild(PLAYER_TAG_INDEX).GetComponent<SpriteRenderer>().flipX = true;
						gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition = new Vector3(gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition.x, gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition.y, 1);
						anim.SetBool("moving", true);

						if (rgbd.velocity.x < MAX_SPEED)
						{
							rgbd.velocity += new Vector2(direction * speed, 0);
						}

						wasMoving = true;
					}
					else if (direction < 0)
					{
						transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
						gameObject.transform.GetChild(PLAYER_TAG_INDEX).GetComponent<SpriteRenderer>().flipX = false;
						gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition = new Vector3(gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition.x, gameObject.transform.GetChild(PLAYER_TAG_INDEX).transform.localPosition.y, -1);
						anim.SetBool("moving", true);

						if (rgbd.velocity.x > -MAX_SPEED)
						{
							rgbd.velocity += new Vector2(direction * speed, 0);
						}

						wasMoving = true;
					}
					else if (wasMoving)
					{
						anim.SetBool("moving", false);
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
					anim.SetBool("DEAD", true);
					Lose();
				}
			}
		}

		const float WAIT_TIME = 1;

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

						ShowRound.round++;

						if (ShowRound.round < ShowRound.MAX_ROUND)
						{
							SceneManager.LoadScene("Battle");
						}
						else
						{
							SceneManager.LoadScene("Title Screen");
						}

					}
				}
			}
			else
			{
				GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = false;
			}
		}
	}

	/// <summary>
	/// Le joueur perd la partie
	/// </summary>
	/// <param name="tie"><see langword="sealed"/>S'il y a une égalité</param>
	public void Lose(bool tie = false)
	{
		dead = true;
		deathTime = Time.time;

		GameObject.Find("Timer").GetComponent<Timer>().ended = true;

		GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);

		// Affiche le joueur gagnant du round et augmente son score
		if (!tie)
		{
			if (name[name.Length - 1] == '1')
			{
				ShowWinner(1, 2);
			}
			if (name[name.Length - 1] == '2')
			{
				ShowWinner(0, 1);
			}
		}
		else
		{
			GameObject.Find("Timer").GetComponent<Timer>().tie = true;
		}

		// Affichage du gagnant de la partie
		if (ShowRound.round == ShowRound.MAX_ROUND)
		{
			GameObject.Find("ButtonNextRound").SetActive(false);

			if (!tie)
			{
				if (ShowRound.score[0] > ShowRound.score[1])
				{
					GameObject.Find("Winner").GetComponent<Text>().text = "Player 1 has won the match";
				}
				else
				{
					GameObject.Find("Winner").GetComponent<Text>().text = "Player 2 has won the match";
				}
			}
			else
			{
				GameObject.Find("Winner").GetComponent<Text>().text = "The match ended in a tie";
			}
		}
		else if (tie)
		{
			GameObject.Find("Winner").GetComponent<Text>().text = "Tie";
		}

		foreach (AnimatorControllerParameter parameter in GetComponent<Animator>().parameters)
		{
			if (parameter.name != "DEAD")
			{
				GetComponent<Animator>().SetBool(parameter.name, false);
			}
		}
	}

	/// <summary>
	/// Affiche le gagnant
	/// </summary>
	/// <param name="playerIndex">Index du joueur dans l'array des personnages</param>
	/// <param name="displayPlayer">Numéro du joueur</param>
	private void ShowWinner(int playerIndex, int displayPlayer)
	{
		ShowRound.score[playerIndex]++;
		GameObject.Find("ScorePlayer2").GetComponent<Text>().text = ShowRound.score[playerIndex].ToString();
		GameObject.Find("Winner").GetComponent<Text>().text = "Player " + displayPlayer + " has won the round";
	}
}