/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCharactersSelection : MonoBehaviour
{
	/// <summary>
	/// Noms des personnages sélectionnés
	/// </summary>
	public static string[] chosenCharactersNames;

	/// <summary>
	/// GameObjects de sélection des personnages
	/// </summary>
	GameObject[] players;

	/// <summary>
	/// Couleur de base des bordure des personnages
	/// </summary>
	Color32 VALIDATED_COLOR = new Color32(0, 255, 0, 255);

	/// <summary>
	/// Couleur de base des bordure des personnages
	/// </summary>
	Color32 defaultColor;

	/// <summary>
	/// Images des personnages jouables
	/// </summary>
	List<Texture2D> images = new List<Texture2D>();

	/// <summary>
	/// Noms des axes de validation des joueurs
	/// </summary>
	List<string[]> validationAxisNames = new List<string[]>()
	{
		new string[]
		{
			"Attack1Player1"
		},
		new string[]
		{
			"Attack1Player2"
		}
	};

	/// <summary>
	/// Si les joueurs ont appuyé sur un bouton
	/// </summary>
	bool[] pressedValidation = new bool[]
	{
		false,
		false
	};

	void Start()
	{
		// Initialisation des variables
		players = GameObject.FindGameObjectsWithTag("Selection");
		defaultColor = players[0].GetComponent<Outline>().effectColor;

		foreach (Texture2D image in Resources.FindObjectsOfTypeAll(typeof(Texture2D)) as Texture2D[])
		{
			if (image.name.EndsWith("Choice"))
			{
				images.Add(image);
			}
		}
	}

	void Update()
	{
		for (int i = 0; i <= 1; i++)
		{
			foreach (string axisName in validationAxisNames[i])
			{
				if (Input.GetAxis(axisName) > 0)
				{
					pressedValidation[i] = true;
				}
				else
				{
					if (pressedValidation[i])
					{
						pressedValidation[i] = false;

						if (players[i].GetComponent<Outline>().effectColor == VALIDATED_COLOR)
						{
							players[i].GetComponent<Outline>().effectColor = defaultColor;

							for (int iChild = 1; iChild < players[i].transform.childCount; iChild++)
							{
								players[i].transform.GetChild(iChild).gameObject.SetActive(true);
							}
						}
						else
						{
							players[i].GetComponent<Outline>().effectColor = VALIDATED_COLOR;

							for (int iChild = 1; iChild < players[i].transform.childCount; iChild++)
							{
								players[i].transform.GetChild(iChild).gameObject.SetActive(false);
							}
						}
					}
				}
			}
		}

		if (players[0].GetComponent<Outline>().effectColor == VALIDATED_COLOR && players[1].GetComponent<Outline>().effectColor == VALIDATED_COLOR)
		{
			GameObject.Find("Countdown").GetComponent<Animator>().SetTrigger("Selected");
			GameObject.Find("Countdown").GetComponent<Animator>().ResetTrigger("Interrupt");
		}
		else
		{
			GameObject.Find("Countdown").GetComponent<Animator>().ResetTrigger("Selected");
			GameObject.Find("Countdown").GetComponent<Animator>().SetTrigger("Interrupt");
		}
	}
}