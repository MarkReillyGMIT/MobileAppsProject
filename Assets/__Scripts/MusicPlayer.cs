using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
	//To not create duplicate music player and to prevent a click sound during loading a scene (solution - put in awake)
	static MusicPlayer instance = null;

	// Public Variables
	#region
	public AudioClip[] startClip;
	public AudioClip[] gameClip;
	public AudioClip[] endClip;
	#endregion

	private static float musicVolume = 0.75f;
	private static float sfxVolume = 0.65f;
	private bool persistMusic = false;
	private int musicType; // 0 - Slow ambient music, 1 - Upbeat Music 
	private AudioSource music;

	void Awake()
	{
		Debug.Log("Music Player Awake " + GetInstanceID());

		musicType = Random.Range(0, startClip.Length);

		if ((instance != null) && (instance != this))
		{
			Destroy(gameObject);
			print("Duplicate Music Player Destroyed");
		}

		else
		{
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip[musicType];
			music.loop = true;
			music.Play();
		}
	}

	// Use this for initialization
	void Start()
	{
		Debug.Log("Music Player Start " + GetInstanceID());
		gameObject.GetComponent<AudioSource>().volume = musicVolume;
	}

	// Update is called once per frame
	void Update()
	{
		gameObject.GetComponent<AudioSource>().volume = musicVolume;
	}

	void OnLevelWasLoaded(int level)
	{
		//Debug.Log ("In OnLevelWasLoaded " + level);

		if (music && (level < 3) && !persistMusic)
		{
			music.Stop();

			if (level == 0)
			{
				music.clip = startClip[musicType];
			}

			else if (level == 1)
			{
				music.clip = gameClip[musicType];
			}

			else if (level == 2)
			{
				music.clip = endClip[musicType];
			}

			music.loop = true;
			music.Play();

			if (SceneManager.sceneCount < 3)
			{
				persistMusic = false;
			}
		}

		else
		{
			persistMusic = true;

			if (level == 0)
			{
				persistMusic = false;
			}
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