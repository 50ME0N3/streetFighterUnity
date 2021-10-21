/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan
 * Description : Fait apparaitre les joueurs sélectionnés
 */

using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayers : MonoBehaviour
{
	/// <summary>
	/// Positions d'apparition des personnages
	/// </summary>
	Vector2[] SPAWN_POINTS = new Vector2[2]
	{
		new Vector2(-2.75f, -0.75f),
		new Vector2(3, -1)
	};

	void Start()
	{
		// Assignation des noms des personnages choisis
		if (CharactersSelection.chosenCharactersNames == null)
		{
			CharactersSelection.chosenCharactersNames = new string[]
			{
				"Ken",
				"Chun-Li"
			};
		}

		for (int i = 0; i < CharactersSelection.chosenCharactersNames.Length; i++)
		{
			#region Player Instantiation
			GameObject player = Resources.Load<GameObject>(CharactersSelection.chosenCharactersNames[i]);
			player.transform.position = SPAWN_POINTS[i];

			player.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PlayerTag" + (i + 1));
			
			if (i == 0)
			{
				player.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color32(255, 64, 64, 255);
			}
			else
			{
				player.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color32(64, 64, 255, 255);
			}

			Instantiate(player).name = "Player" + (i + 1);
			#endregion

			// Images des barres de vie
			foreach (Texture2D image in Resources.LoadAll<Texture2D>("Choices"))
			{
				if (image.name.Replace("Choice", string.Empty) == CharactersSelection.chosenCharactersNames[i])
				{
					GameObject.Find("healthbarPlayer" + (i + 1)).transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Sprite.Create(image, new Rect(Vector2.zero, new Vector2(image.width, image.height)), new Vector2(0.5f, 0.5f));
				}
			}
		}
	}
}