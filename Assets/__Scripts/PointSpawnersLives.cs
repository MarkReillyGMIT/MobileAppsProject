using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;

//        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);

public class PointSpawnersLives : MonoBehaviour
{
    // == private fields ==

    [SerializeField] private ExtraHealth extraHealthPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    [SerializeField] private float spawnInterval = 0.35f;

    private const string SPAWN_ENEMY_METHOD = "SpawnOneEnemy";

    private IList<SpawnPoint> spawnPoints;

    private Stack<SpawnPoint> spawnStack;

    private GameObject liveParent;

    //private ListUtils listUtils = new ListUtils();

    // == private methods ==
    private void Start()
    {
        liveParent = GameObject.Find("LiveParent");
        if (!liveParent)
        {
            liveParent = new GameObject("LiveParent");
        }
        // get the spawn points here
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        SpawnLiveWaves();
    }

    private void SpawnLiveWaves()
    {
        // create the stack of points
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        //InvokeRepeating("SpawnOneEnemy", 0f, 0.25f);
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    }

    // stack version
    private void SpawnOneEnemy()
    {
        if (spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }
        var enemy = Instantiate(extraHealthPrefab, liveParent.transform);
        var sp = spawnStack.Pop();
        enemy.transform.position = sp.transform.position;
    }

}
