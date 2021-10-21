/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan
 * Description : Gère la durée du round
 */

using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	/// <summary>
	/// Durée du match en secondes
	/// </summary>
	const int MATCH_DURATION = 120;

	/// <summary>
	/// Le minuteur deviendra rouge quand il atteindra cette valeur (en secondes)
	/// </summary>
	const int LAST_SECONDS = 10;

	/// <summary>
	/// Quand la partie a commencé
	/// </summary>
	float startTime = 0;


	/// <summary>
	/// Si le round est terminé
	/// </summary>
	public bool ended = false;

	/// <summary>
	/// s'il y a une égalité
	/// </summary>
	public bool tie = false;

	void Start()
	{
		startTime = Time.time;
	}

	void Update()
	{
		if (!ended)
		{
			int minutes = Mathf.FloorToInt((MATCH_DURATION - (Time.time - startTime)) / 60);
			int seconds = Mathf.FloorToInt(MATCH_DURATION - (Time.time - startTime) - 60 * minutes);

			Text text = GetComponent<Text>();
			text.text = minutes + ":";
			if (seconds < 10)
			{
				text.text += '0';
			}
			text.text += seconds;

			if (minutes == 0 && seconds <= LAST_SECONDS)
			{
				text.color = Color.red;

				if (seconds == 0)
				{
					player player1 = GameObject.Find("Player1").GetComponent<player>();
					player player2 = GameObject.Find("Player2").GetComponent<player>();

					if (player1.healthBar.Health == player2.healthBar.Health)
					{
						player1.Lose(true);
						player2.Lose(true);
					}
					else if (player1.healthBar.Health < player2.healthBar.Health)
					{
						player1.Lose();
					}
					else
					{
						player2.Lose();
					}
				}
			}
			else
			{
				text.color = new Color32(50, 50, 50, 255);
			}
		}

		if (GameObject.Find("Player1").GetComponent<player>().resetTime)
		{
			startTime = Time.time;
		}
	}
}