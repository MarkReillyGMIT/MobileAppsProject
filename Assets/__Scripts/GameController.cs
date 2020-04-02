using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    // text mesh pro library
using UnityEngine.SceneManagement;


// currently have a single wave config
// want to be take in multiple waves and 
// loop through them.
// store multiple waves in a list - dynamic


public class GameController : MonoBehaviour
{
    // == public fields ==
    

    // == private fields ==
    private int playerScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    // for the enemy waves
    //[SerializeField] private WaveConfig waveConfig;
    [SerializeField] private List<WaveConfig> waveConfigList;
    private int startingWave = 0;

    //private int waveNumber = 0;

    //[SerializeField] private int enemyCountPerWave = 20;
    [SerializeField] private TextMeshProUGUI remainingEnemyText;
    private int remainingEnemyCount;
    // audio sound to indicate "between wave" moment
    [SerializeField] private AudioClip waveReadySound;
    private SoundController sc;
    // == private methods ==

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
        
        sc = SoundController.FindSoundController();
        StartCoroutine(SpawnAllWaves());
    }

    // set up a method to spawn all the waves.
    // make this a coroutine, wait for the SetupNextWave coroutine
    // to run, and then go to the next element in the list.

    private IEnumerator SpawnAllWaves()
    {
        // use a loop through the list.
        for (int waveIndex = startingWave; waveIndex < waveConfigList.Count; waveIndex++)
        {
            var waveConfig = waveConfigList[waveIndex];
            yield return StartCoroutine(SetupNextWave(waveConfig));
        }
    }

    private IEnumerator SetupNextWave(WaveConfig currentWave)
    {
        yield return new WaitForSeconds(5.0f);
        FindObjectOfType<PointSpawners>().SetWaveConfig(currentWave);
        remainingEnemyCount = currentWave.GetEnemiesPerWave();
        EnableSpawning();
    }

    private void PointSpawners_OnEnemySpawnedEvent()
    {
        remainingEnemyCount--;
        UpdateEnemyRemainingText();
        if (remainingEnemyCount == 0)
        {
            DisableSpawning();
        }
    }

    private void DisableSpawning()
    {
        // find each PointSpawner, call a public method to disable spawning
        foreach (var spawner in FindObjectsOfType<PointSpawners>())
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

    private void UpdateEnemyRemainingText()
    {
        remainingEnemyText.text = remainingEnemyCount.ToString("000");
    }

    private void UpdateScore()
    {
        //Debug.Log("Score: " + playerScore);
        // display on screen
        scoreText.text = playerScore.ToString();
    }

}
