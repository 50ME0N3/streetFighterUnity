using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
	Vector2[] spawnPoints = new Vector2[2]
	{
		new Vector2(-2.75f, -0.75f),
		new Vector2(3, -1)
	};

	void Start()
	{
		if (CharactersSelection.chosenCharactersNames == null)
		{
			CharactersSelection.chosenCharactersNames = new string[]
			{
				"HeavyBandit",
				"HeavyBandit"
			};
		}

		for (int i = 0; i < CharactersSelection.chosenCharactersNames.Length; i++)
		{
			GameObject player = Resources.Load<GameObject>(CharactersSelection.chosenCharactersNames[i]);
			player.transform.position = spawnPoints[i];

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
		}
	}
}