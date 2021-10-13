/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersSelection : MonoBehaviour
{
	#region Global Variables
	/// <summary>
	/// Noms des personnages sélectionnés
	/// </summary>
	public static string[] chosenCharactersNames;

	/// <summary>
	/// GameObjects de sélection des personnages
	/// </summary>
	GameObject[] players;

	/// <summary>
	/// Couleur des bordures des personnages sélectionnés
	/// </summary>
	Color32 VALIDATED_COLOR = new Color32(0, 255, 0, 255);

	/// <summary>
	/// Couleur par défaut des bordures des personnages
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
			"Attack1Player1",
			"Attack2Player1"
		},
		new string[]
		{
			"Attack1Player2",
			"Attack2Player2"
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
	/// Si les joueurs ont appuyé sur le bouton de gauche
	/// </summary>
	bool[] pressedLeft = new bool[2]
	{
		false,
		false
	};

	/// <summary>
	/// Si les joueurs ont appuyé sur le bouton de droite
	/// </summary>
	bool[] pressedRight = new bool[2]
	{
		false,
		false
	};

	/// <summary>
	/// Index des personnages choisis dans la liste
	/// </summary>
	int[] characterIndex = new int[2]
	{
		0,
		0
	};
	#endregion

	void Start()
	{
		// Initialisation des variables
		players = GameObject.FindGameObjectsWithTag("Selection");
		defaultColor = players[0].GetComponent<Outline>().effectColor;
		countdown = GameObject.Find("Countdown").GetComponent<Animator>();
		chosenCharactersNames = new string[2];

		// Importation des images
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
		// Boucle pour les deux joueurs
		for (int i = 0; i <= 1; i++)
		{
			CharacterChange(i);

			Validation(i);
		}

		Countdown();
	}

	/// <summary>
	/// Change le personnage du joueur
	/// </summary>
	/// <param name="iPlayer">Index du joueur</param>
	void CharacterChange(int iPlayer)
	{
		if (players[iPlayer].GetComponent<Outline>().effectColor != VALIDATED_COLOR)
		{
			if (Input.GetAxis("HorizontalPlayer" + (iPlayer + 1)) < 0)
			{
				if (!pressedLeft[iPlayer])
				{
					pressedLeft[iPlayer] = true;

					if (characterIndex[iPlayer] > 0)
					{
						characterIndex[iPlayer]--;
					}
					else
					{
						characterIndex[iPlayer] = images.Count - 1;
					}

					players[iPlayer].transform.GetChild(1).GetComponent<Animator>().SetTrigger("Move");
					players[iPlayer].transform.GetChild(0).GetComponent<Image>().sprite = images[characterIndex[iPlayer]];
				}
			}
			else if (Input.GetAxis("HorizontalPlayer" + (iPlayer + 1)) > 0)
			{
				if (!pressedRight[iPlayer])
				{
					pressedRight[iPlayer] = true;

					if (characterIndex[iPlayer] < images.Count - 1)
					{
						characterIndex[iPlayer]++;
					}
					else
					{
						characterIndex[iPlayer] = 0;
					}

					players[iPlayer].transform.GetChild(2).GetComponent<Animator>().SetTrigger("Move");
					players[iPlayer].transform.GetChild(0).GetComponent<Image>().sprite = images[characterIndex[iPlayer]];
				}
			}
			else
			{
				pressedLeft[iPlayer] = false;
				pressedRight[iPlayer] = false;
			}
		}
	}

	/// <summary>
	/// Valide le choix du joueur
	/// </summary>
	/// <param name="iPlayer">Index du joueur</param>
	void Validation(int iPlayer)
	{
		bool validationKeyDown = false;

		foreach (string axisName in validationAxisNames[iPlayer])
		{
			if (Input.GetAxis(axisName) > 0)
			{
				validationKeyDown = true;
			}
		}

		if (validationKeyDown)
		{
			if (!pressedValidation[iPlayer])
			{
				pressedValidation[iPlayer] = true;

				if (players[iPlayer].GetComponent<Outline>().effectColor == VALIDATED_COLOR)
				{
					players[iPlayer].GetComponent<Outline>().effectColor = defaultColor;

					for (int iChild = 1; iChild < players[iPlayer].transform.childCount; iChild++)
					{
						players[iPlayer].transform.GetChild(iChild).gameObject.SetActive(true);
					}
				}
				else
				{
					players[iPlayer].GetComponent<Outline>().effectColor = VALIDATED_COLOR;

					for (int iChild = 1; iChild < players[iPlayer].transform.childCount; iChild++)
					{
						players[iPlayer].transform.GetChild(iChild).gameObject.SetActive(false);
					}

					chosenCharactersNames[iPlayer] = images[characterIndex[iPlayer]].name;
				}
			}
		}
		else
		{
			pressedValidation[iPlayer] = false;
		}
	}

	/// <summary>
	/// Lancement du compte à rebours
	/// </summary>
	void Countdown()
	{
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