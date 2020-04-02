﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ToGame()
    {
        SceneManager.LoadSceneAsync("LevelOne");
        
    }

    public void ToOptions()
    {
        SceneManager.LoadSceneAsync("Options");
    }

    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        AudioListener.pause = !AudioListener.pause;
    }
}
