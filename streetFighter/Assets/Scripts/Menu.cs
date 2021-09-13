using UnityEngine;

public class Menu : MonoBehaviour
{
	public void StartButton()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
	}

	public void QuitButton()
	{
		Application.Quit();
	}
}