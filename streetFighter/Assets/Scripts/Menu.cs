/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public static bool gameIsPaused = false;

	public GameObject Pause;

	void Update()
	{
		// Pause
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(gameIsPaused == true)
			{
				ReturnToGame();
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
		Pause.SetActive(true);
		Time.timeScale = 0;
		gameIsPaused = true;
		GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
	}

	/// <summary>
	/// Ferme le menu de pause
	/// </summary>
	public void ReturnToGame()
	{
		Pause.SetActive(false);
		Time.timeScale = 1;
		gameIsPaused = false;
	}

	/// <summary>
	/// retour au menu principal
	/// </summary>
	public void BackMainMenu()
	{
		Pause.SetActive(false);
		Time.timeScale = 1;
		gameIsPaused = false;
		SceneManager.LoadScene("Title Screen");
	}
}