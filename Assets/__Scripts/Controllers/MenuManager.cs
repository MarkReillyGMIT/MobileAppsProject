using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Controls movement between scenes.
 */
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
        //Mutes sound when called.
        AudioListener.pause = !AudioListener.pause;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void PauseToMainMenu()
    {
        //Turns time back to normal after being paused.
        SceneManager.LoadSceneAsync("MainMenu");
        AudioListener.pause = !AudioListener.pause;
        Time.timeScale = 1;
    }

    public void ToLevelTwo()
    {
        SceneManager.LoadSceneAsync("LevelTwo");
    }

    public void ToScores()
    {
        SceneManager.LoadSceneAsync("Scores");
    }

    public void ToTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }
}
