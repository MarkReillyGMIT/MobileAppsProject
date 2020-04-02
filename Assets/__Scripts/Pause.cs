using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject Pausemenu, PauseButton;

    public void PauseGame()
    {
        Pausemenu.SetActive(true);
        PauseButton.SetActive(false);
        AudioListener.pause = !AudioListener.pause;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Pausemenu.SetActive(false);
        PauseButton.SetActive(true);
        AudioListener.pause = !AudioListener.pause;
        Time.timeScale = 1;
    }
}
