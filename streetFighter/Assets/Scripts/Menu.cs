/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Gabriel
 * Description : Gestion des boutons des menus
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public static bool gameIsPaused = false;

	public GameObject Pause;

	void Update()
	{
		// Pause
		if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			if(gameIsPaused == true)
			{
				Resume();
			}
			else
			{
				Paused();
			}
		}
	}

	/// <summary>
	/// Relance une partie depuis l'écran de sélection des personnages
	/// </summary>
	public void StartButton()
	{
		// Remise à zéro du score et du round
		ShowRound.round = 1;
		ShowRound.score[0] = 0;
		ShowRound.score[1] = 0;

		SceneManager.LoadScene("Character Selection");
	}
	
	/// <summary>
	/// Quitte le jeu
	/// </summary>
	public void QuitButton()
	{
		Application.Quit();
	}

	/// <summary>
	/// Ouvre le menu de pause
	/// </summary>
	public void Paused()
	{
		// Affiche le menu de pause
		Pause.SetActive(true);
		Time.timeScale = 0;
		gameIsPaused = true;
		GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);

		// Le score et le round sont masqués
		foreach (Text text in GameObject.Find("Round").GetComponentsInChildren<Text>())
		{
			text.enabled = false;
		}
	}

	/// <summary>
	/// Commence le round suivant
	/// </summary>
	public void NextRound()
	{
		ShowRound.round++;
		SceneManager.LoadScene("Battle");
	}

	/// <summary>
	/// Ferme le menu de pause
	/// </summary>
	public void Resume()
	{
		// Masque le menu de pause
		Pause.SetActive(false);
		Time.timeScale = 1;
		gameIsPaused = false;
		GameObject.Find("Round").transform.GetChild(1).gameObject.SetActive(true);

		// Le score et le round sont affichés
		foreach (Text text in GameObject.Find("Round").GetComponentsInChildren<Text>())
		{
			text.enabled = true;
		}
	}

	/// <summary>
	/// retour au menu principal
	/// </summary>
	public void BackMainMenu()
	{
		gameIsPaused = false;
		Time.timeScale = 1;
		SceneManager.LoadScene("Title Screen");
	}
}