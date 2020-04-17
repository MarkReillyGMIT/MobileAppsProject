using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/*
 * Controls volume levels for music and sound effects.
 */
public class MusicPlayer : MonoBehaviour
{
	// Public Variables

	//Private variables
	private static float musicVolume = 0.75f;
	private static float sfxVolume = 0.65f;

	// Use this for initialization
	void Start()
	{
		gameObject.GetComponent<AudioSource>().volume = musicVolume;
	}

	// Update is called once per frame
	void Update()
	{
		gameObject.GetComponent<AudioSource>().volume = musicVolume;
	}
	//Reference: Damien Costello -https://learnonline.gmit.ie/course/view.php?id=1832
	void Awake()
	{
		SetupSingleton();
	}

	// add a method to setup as a singleton
	private void SetupSingleton()
	{
		// this method gets called on creation
		// check for any other objects of the same type
		// if there is one, then use that one.
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);    // destroy the current object
		}
		else
		{
			// keep the new one
			DontDestroyOnLoad(gameObject);  // persist across scenes
		}
	}

	public static void SetMusicVolume(float volumeMusic)
	{
		musicVolume = volumeMusic;
	}

	public static float GetMusicVolume()
	{
		return musicVolume;
	}

	public static void SetSFXVolume(float volumeSFX)
	{
		sfxVolume = volumeSFX;
	}

	public static float GetSFXVolume()
	{
		return sfxVolume;
	}
}