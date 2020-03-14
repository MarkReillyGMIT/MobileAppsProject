using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use this to manage collisions

public class Enemy : MonoBehaviour
{
    // == set this up to publish an event to the system
    // == when killed

    // == public fields ==
    // used from GameController enemy.ScoreValue
    public int ScoreValue { get { return scoreValue; } }

    // delegate type to use for event
    public delegate void EnemyKilled(Enemy enemy);

    // create static method to be implemented in the listener
    public static EnemyKilled EnemyKilledEvent;

    // == private fields ==
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private AudioClip crashSound;
    // sounds for getting hit by bullet, spawning
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip spawnSound;

    private float explosionDuration = 1.0f;

    private SoundController sc;

    // == private methods ==
    private void Start()
    {
        //sc = FindObjectOfType<SoundController>();
        sc = SoundController.FindSoundController();
        PlaySound(spawnSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (sc)
        {
            sc.PlayOneShot(clip);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        // parameter = what ran into me
        // if the player hit, then destroy the player
        // and the current enemy rectangle

        var player = whatHitMe.GetComponent<PlayerMovement>();
        var bullet = whatHitMe.GetComponent<Bullet>();

        if (player)  // if (player != null)
        {
            // play crash sound here
            PlaySound(crashSound);
            // destroy the player and the rectangle
            // give the player points/coins
            Destroy(player.gameObject);
            Destroy(gameObject);
        }

        if (bullet)
        {
            // play die sound
            PlaySound(dieSound);
            // destroy bullet
            Destroy(bullet.gameObject);
            // publish the event to the system to give the player points
            PublishEnemyKilledEvent();
            // show the explosion
            GameObject explosion = Instantiate(explosionFX,
                                               transform.position,
                                               transform.rotation);
            Destroy(explosion, explosionDuration);
            // destroy this game object
            Destroy(gameObject);
        }
    }

    private void PublishEnemyKilledEvent()
    {
        // make sure somebody is listening
        if (EnemyKilledEvent != null)
        {
            EnemyKilledEvent(this);
        }
    }
}
