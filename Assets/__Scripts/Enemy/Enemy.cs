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
    public int ScoreValue {
        set { scoreValue = value; }
        get { return scoreValue; } }
    // used in PLayerHealth
    public int DamageValue {
        set { damageValue = value; }
        get { return damageValue; } }

    // delegate type to use for event
    public delegate void EnemyKilled(Enemy enemy);

    // create static method to be implemented in the listener
    public static EnemyKilled EnemyKilledEvent;

    // == private fields ==
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private int damageValue = 15;
    [SerializeField] float health = 15f;
    [SerializeField] int scoreWeight = 150;



    [SerializeField] private GameObject explosionFX;
    [SerializeField] private AudioClip crashSound;
    // sounds for getting hit by bullet, spawning
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] public AudioClip shootSound;
    [SerializeField] AudioClip destroySound;
    [SerializeField] GameObject particleEmission;
    [SerializeField] GameObject addScoreParticleEmitter;


    private float sfxVolume;
    private float damage = 10f;
    private SoundController sc;
    private ScoreKeeper scoreKeeper;
    private Vector3 sfxPosition;



    // == private methods ==
    private void Start()
    {
        //sc = FindObjectOfType<SoundController>();
        sc = SoundController.FindSoundController();
        PlaySound(spawnSound);
        sfxVolume = MusicPlayer.GetSFXVolume();
        sfxPosition = new Vector3(this.transform.position.x, this.transform.position.y, -50); //for 3-D Sound
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void PlaySound(AudioClip clip)
    {
        if (sc)
        {
            sc.PlayOneShot(clip);
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    void Hit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
   private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        // parameter = what ran into me
        // if the player hit, then destroy the player
        // and the current enemy rectangle

        var player = whatHitMe.GetComponent<PlayerMovement>();
        var bullet = whatHitMe.GetComponent<Bullet>();

        if(player)  // if (player != null)
        {
            Destroy(gameObject);
        }

        if(bullet)
        {
            bullet.Hit();
            Hit(bullet.GetDamage());
        }
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    void Die()
    {
        GameObject particleColor = Instantiate(particleEmission, gameObject.transform.position, Quaternion.identity) as GameObject;
        ParticleSystem ps = particleColor.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        psmain.startColor = gameObject.GetComponent<SpriteRenderer>().color;

        if (destroySound)
        {
            AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, Mathf.Clamp(sfxVolume + 0.45f, 0.0f, 1.0f));
        }

        if (addScoreParticleEmitter)
        {
            Instantiate(addScoreParticleEmitter);
        }

        scoreKeeper.Score(scoreWeight);
        Destroy(this.gameObject);
    }
}
