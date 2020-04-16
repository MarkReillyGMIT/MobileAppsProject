﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderEvent : MonoBehaviour
{
	// Public Variables
	#region
	public Slider musicSlider;
	public Slider sfxSlider;
	#endregion

	// Use this for initialization
	void Start()
	{
		GameObject.DontDestroyOnLoad(gameObject);
		musicSlider.value = MusicPlayer.GetMusicVolume();
		sfxSlider.value = MusicPlayer.GetSFXVolume();
		GameObject.DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		SetSFXVolume();
		SetMusicVolume();
	}

	void SetSFXVolume()
	{
		if (sfxSlider == null)
		{
			return;
		}

		MusicPlayer.SetSFXVolume(sfxSlider.value);
	}

	void SetMusicVolume()
	{
		if (musicSlider == null)
		{
			return;
		}

		MusicPlayer.SetMusicVolume(musicSlider.value);
	}
}