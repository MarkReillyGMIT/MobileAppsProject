using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Controls the ingame pause menu
 */
public class Pause : MonoBehaviour
{
    public GameObject Pausemenu, PauseButton;
    //Calls the pasuemenu to be active and deactivate's the pause button
    //Stops the music
    //Stops the game
    public void PauseGame()
    {
        Pausemenu.SetActive(true);
        PauseButton.SetActive(false);
        AudioListener.pause = !AudioListener.pause;
        Time.timeScale = 0;
    }
    //Sets pauseMenu to false which hides it from the screen
    //Shows the pause button
    //Plays music
    //Runs game from current position
    public void Resume()
    {
        Pausemenu.SetActive(false);
        PauseButton.SetActive(true);
        AudioListener.pause = !AudioListener.pause;
        Time.timeScale = 1;
    }
}
