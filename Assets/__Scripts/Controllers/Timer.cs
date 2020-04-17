using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * Controls timer in LevelOne and LevelTwo.
 */
public class Timer : MonoBehaviour
{
    // Timer variable
    [SerializeField] float timer = 25f;
    private Text timerSeconds;
    void Start()
    {
        // Start the timer 
        timerSeconds = GetComponent<Text>();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        // Convert timer to text
        timerSeconds.text = timer.ToString("f0");

        //If timer is less than or equal too Zero, go to Failed scene
        if (timer <= 0)
        {
            SceneManager.LoadSceneAsync("Failed");
        }
    }
}
