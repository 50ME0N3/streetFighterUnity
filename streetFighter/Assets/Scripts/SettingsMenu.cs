using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer audioMixer;



	public void Start()
	{

	}
	public void SetVolume(float volume)
	{
		audioMixer.SetFloat("volume", volume);
	}

	public void SetFullScreen(bool isFullScreen)
	{
		Screen.fullScreen = isFullScreen;
	}
}
