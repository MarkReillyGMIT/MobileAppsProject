using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
/*
 * Controls the spawning of Enemy gameObjects
 */
//Reference - Damien Costello LearnOnline- https://learnonline.gmit.ie/mod/resource/view.php?id=94253
public class PointSpawners : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    //Spawn delay and interval time
    [SerializeField] private float spawnDelay = 1.5f;
    [SerializeField] private float spawnInterval = 1.5f;

    private const string SPAWN_ENEMY_METHOD = "SpawnOneEnemy";

    private IList<SpawnPoint> spawnPoints;

    private Stack<SpawnPoint> spawnStack;

    private GameObject enemyParent;

    // == private methods ==
    private void Start()
    {
        enemyParent = GameObject.Find("EnemyParent");
        if (!enemyParent)
        {
            enemyParent = new GameObject("EnemyParent");
        }

        // Get the spawn points 
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        SpawnEnemyWaves();
    }

    // Spawn enemies
    private void SpawnEnemyWaves()
    {
        // Create the stack of points
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    }

    // Stack version
    private void SpawnOneEnemy()
    {
        if (spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }
        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        var sp = spawnStack.Pop();
        enemy.transform.position = sp.transform.position;
    }
}