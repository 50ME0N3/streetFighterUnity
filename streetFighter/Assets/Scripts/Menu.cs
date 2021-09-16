using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public GameObject settingsWindow;

	public static bool gameIsPaused = false;

	public GameObject Pause;

	void Update()
	{
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

	public void StartButton()
	{
		
		UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
	}

	public void SettingsButton()
	{
		settingsWindow.SetActive(true);
	}

	public void ClosedSettingsWindows()
	{
		settingsWindow.SetActive(false);
	}
	

	public void QuitButton()
	{
		Application.Quit();
	}

	public void ReturnToGame()
	{
		Pause.SetActive(false);
		Time.timeScale = 1;
		gameIsPaused = false;
	}

	public void Paused()
	{
		Pause.SetActive(true);
		Time.timeScale = 0;
		gameIsPaused = true;
	}

	public void BackMainMenu()
	{
		//DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnload();
		SceneManager.LoadScene("Title Screen");
		Pause.SetActive(false);
		Time.timeScale = 1;
		gameIsPaused = false;
	}
}