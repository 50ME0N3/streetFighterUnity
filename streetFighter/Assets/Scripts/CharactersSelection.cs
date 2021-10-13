/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersSelection : MonoBehaviour
{
	/// <summary>
	/// Noms des personnages sélectionnés
	/// </summary>
	public static string[] chosenCharactersNames = new string[2];

	/// <summary>
	/// GameObjects de sélection des personnages
	/// </summary>
	GameObject[] players;

	/// <summary>
	/// Couleur des bordures des personnages sélectionnés
	/// </summary>
	Color32 VALIDATED_COLOR = new Color32(0, 255, 0, 255);

	/// <summary>
	/// Couleur de base des bordures des personnages
	/// </summary>
	Color32 defaultColor;

	/// <summary>
	/// Images des personnages jouables
	/// </summary>
	List<Sprite> images = new List<Sprite>();

	/// <summary>
	/// Compte à rebours du lancement de la partie
	/// </summary>
	Animator countdown;

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
	/// Si les joueurs ont appuyé sur un bouton de validation
	/// </summary>
	bool[] pressedValidation = new bool[2]
	{
		false,
		false
	};

	/// <summary>
	/// Si les joueurs ont appuyé sur un bouton de gauche
	/// </summary>
	bool[] pressedLeft = new bool[2]
	{
		false,
		false
	};

	/// <summary>
	/// Si les joueurs ont appuyé sur un bouton de droite
	/// </summary>
	bool[] pressedRight = new bool[2]
	{
		false,
		false
	};

	int[] characterIndex = new int[2]
	{
		0,
		0
	};

	void Start()
	{
		// Initialisation des variables
		players = GameObject.FindGameObjectsWithTag("Selection");
		defaultColor = players[0].GetComponent<Outline>().effectColor;
		countdown = GameObject.Find("Countdown").GetComponent<Animator>();

		foreach (Texture2D image in Resources.LoadAll<Texture2D>("Choices"))
		{
			images.Add(Sprite.Create(image, new Rect(Vector2.zero, new Vector2(image.width, image.height)), new Vector2(0.5f, 0.5f)));
			images[images.Count - 1].name = image.name.Replace("Choice", string.Empty);
		}

		for (int i = 0; i < characterIndex.Length; i++)
		{
			players[i].transform.GetChild(0).GetComponent<Image>().sprite = images[characterIndex[0]];
		}
	}

	void Update()
	{
		for (int i = 0; i <= 1; i++)
		{
			if (Input.GetAxis("HorizontalPlayer" + (i + 1)) < 0)
			{
				pressedLeft[i] = true;
			}
			else if (Input.GetAxis("HorizontalPlayer" + (i + 1)) > 0)
			{
				pressedRight[i] = true;
			}
			else
			{
				if (pressedLeft[i])
				{
					pressedLeft[i] = false;

					if (characterIndex[i] > 0)
					{
						characterIndex[i]--;
					}
				}
				else if (pressedRight[i])
				{
					pressedRight[i] = false;

					if (characterIndex[i] < characterIndex.Length - 1)
					{
						characterIndex[i]++;
					}
				}

				players[i].transform.GetChild(0).GetComponent<Image>().sprite = images[characterIndex[i]];
			}

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

							chosenCharactersNames[i] = images[characterIndex[i]].name;
						}
					}
				}
			}
		}

		if (players[0].GetComponent<Outline>().effectColor == VALIDATED_COLOR && players[1].GetComponent<Outline>().effectColor == VALIDATED_COLOR)
		{
			countdown.SetTrigger("Selected");
			countdown.ResetTrigger("Interrupt");
		}
		else
		{
			countdown.ResetTrigger("Selected");
			countdown.SetTrigger("Interrupt");
		}
	}
}