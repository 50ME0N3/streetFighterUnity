/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using System.Collections.Generic;
using UnityEngine;

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
	/// Images des personnages jouables
	/// </summary>
	List<Texture2D> images = new List<Texture2D>();

	/// <summary>
	/// Noms des axes de validation du joueur 1
	/// </summary>
	string[] validationAxisNames1 = new string[1]
	{
		"Attack1Player1"
	};

	/// <summary>
	/// Noms des axes de validation du joueur 2
	/// </summary>
	string[] validationAxisNames2 = new string[1]
	{
		"Attack1Player2"
	};

	/// <summary>
	/// Si le joueur 1 a appuyé sur un bouton
	/// </summary>
	bool pressed1 = false;

	/// <summary>
	/// Si le joueur 2 a appuyé sur un bouton
	/// </summary>
	bool pressed2 = false;

	void Start()
	{
		// Initialisation des variables
		players = GameObject.FindGameObjectsWithTag("Selection");

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
		//foreach (string axisName in validationAxisNames1)
		//{
		//	if (Input.GetAxis(axisName) > )
		//	{

		//	}
		//}

		//foreach (string axisName in validationAxisNames2)
		//{

		//}
	}
}