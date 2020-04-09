using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;

//        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);

public class PointSpawners : MonoBehaviour
{
    // == private fields ==
    private WaveConfig waveConfig;

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    //[SerializeField] private float spawnInterval = 0.35f;

    private const string SPAWN_ENEMY_METHOD = "SpawnOneEnemy";
    private IList<SpawnPoint> spawnPoints;
    private Stack<SpawnPoint> spawnStack;
    private GameObject enemyParent;

    // events for telling the system enemy spawned
    public delegate void EnemySpawned();
    public static event EnemySpawned EnemySpawnedEvent;

    // == private methods ==
    private void Start()
    {
        enemyParent = GameObject.Find("EnemyParent");
        if(!enemyParent) { enemyParent = new GameObject("EnemyParent"); }
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        //EnableSpawning();
    }

    // stack version
    private void SpawnOneEnemy()
    {
        if(spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }
        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        var sp = spawnStack.Pop();
        enemy.transform.position = sp.transform.position;
        // set the enemy parameters.
        enemy.GetComponent<EnemyBehaviour>().SetMoveSpeed(waveConfig.GetEnemySpeed());
        enemy.GetComponent<Enemy>().ScoreValue = waveConfig.GetScoreValue();
        enemy.GetComponent<Enemy>().DamageValue = waveConfig.GetDamageValue();
        // use the new sprite
        enemy.GetComponent<SpriteRenderer>().sprite = waveConfig.GetEnemySprite();
        PublishOnEnemySpawnedEvent();   // tell the system
    }

    // == public methods ==

    // create my event to publish the fact that enemt spawned
    public void PublishOnEnemySpawnedEvent()
    {
        EnemySpawnedEvent?.Invoke();
    }

    public void EnableSpawning()
    {
        //InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay,
                        waveConfig.GetSpawnInterval());
    }

    public void DisableSpawning()
    {
        CancelInvoke(SPAWN_ENEMY_METHOD);
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

}
