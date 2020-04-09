using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    // text mesh pro library

// currently have a single wave config
// want to be take in multiple waves and 
// loop through them.
// store multiple waves in a list - dynamic
// sceneManageent library - load, unload scenes
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    // == public fields ==
    public int StartingLives { get { return startingLives; } }

    public int RemainingLives { get { return remainingLives; } }

    // == private fields ==
    private int playerScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int startingLives = 3;

    private int remainingLives = 0;

    // for the enemy waves
    //[SerializeField] private WaveConfig waveConfig;
    [SerializeField] private List<WaveConfig> waveConfigList;
    private int startingWave = 0;

    private int waveNumber = 0;

    //[SerializeField] private int enemyCountPerWave = 20;
    private int remainingEnemyCount;
    // audio sound to indicate "between wave" moment
    [SerializeField] private AudioClip waveReadySound;
    private SoundController sc;

    // == private methods ==

    public GameObject heart1, heart2, heart3, gameOver;
    public static int health;
    #region Awake, OnEnable, OnDisable Methods
    private void Awake()
    {
        SetupSingleton();
    }

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

    private void OnEnable()
    {
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
        PointSpawners.EnemySpawnedEvent += PointSpawners_OnEnemySpawnedEvent;
    }

    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
        PointSpawners.EnemySpawnedEvent -= PointSpawners_OnEnemySpawnedEvent;
    }
    #endregion

    private void Start()
    {
        UpdateScore();
        remainingLives = startingLives;
        sc = SoundController.FindSoundController();
        health = 3;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
        //StartCoroutine(SetupNextWave());
        StartCoroutine(SpawnAllWaves());
    }

    // set up a method to spawn all the waves.
    // make this a coroutine, wait for the SetupNextWave coroutine
    // to run, and then go to the next element in the list.

    private IEnumerator SpawnAllWaves()
    {
        // use a loop through the list.
        for(int waveIndex = startingWave; waveIndex < waveConfigList.Count; waveIndex++)
        {
            var waveConfig = waveConfigList[waveIndex];
            yield return StartCoroutine(SetupNextWave(waveConfig));
        }
    }

    private IEnumerator SetupNextWave(WaveConfig currentWave)
    {
        yield return new WaitForSeconds(5.0f);
        sc.PlayOneShot(waveReadySound);
        FindObjectOfType<PointSpawners>().SetWaveConfig(currentWave);
        remainingEnemyCount = currentWave.GetEnemiesPerWave();
        EnableSpawning();
    }

    private void PointSpawners_OnEnemySpawnedEvent()
    {
        remainingEnemyCount--;
        if (remainingEnemyCount == 0)
        {
            DisableSpawning();
        }
    }

    private void DisableSpawning()
    {
        // find each PointSpawner, call a public method to disable spawning
        foreach(var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.DisableSpawning();
        }
    }

    private void EnableSpawning()
    {
        // find each PointSpawner, call a public method to disable spawning
        foreach (var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.EnableSpawning();
        }
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        // add the score value for the enemy to the player score
        playerScore += enemy.ScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        //Debug.Log("Score: " + playerScore);
        // display on screen
        scoreText.text = playerScore.ToString();
    }

    // == public methods ==
    public void LoseOneLife()
    {
        remainingLives--;
    }

    private void Update()
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
