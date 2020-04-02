using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;    // scene names

// sceneManageent library - load, unload scenes
using UnityEngine.SceneManagement;



public class SceneController : MonoBehaviour
{

    public GameObject heart1, heart2, heart3, gameOver;
    public static int health;
    private void Start()
    {
        health = 3;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
    }
    // == onclick Events ==
    public void Start_OnClick()
    {
        SceneManager.LoadSceneAsync(SceneNames.GAME_LEVEL);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(SceneNames.MAIN_MENU);
    }

    public void Options_OnClick()
    {
        SceneManager.LoadSceneAsync(SceneNames.OPTIONS_MENU,
                                    LoadSceneMode.Additive);
    }

    public void OptionsBack_OnClick()
    {
        // this unloads the options menu
        SceneManager.UnloadSceneAsync(SceneNames.OPTIONS_MENU);
    }

    void Update()
    {
        if (health > 3)
            health = 3;

        switch (health)
        {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(true);
                //Load Menu when player dies.
                SceneManager.LoadSceneAsync("MainMenu");
                break;
        }

    }
}
