using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Controls EnemyAI 
 * 
 * Reference: https://www.youtube.com/watch?v=kOzhE3_P2Mk
 */
public class EnemyAI : MonoBehaviour
{
    // == public fields ==
    //Health of Enemy
    [SerializeField] float health = 15f;
    //Score Value of enemy
    [SerializeField] int scoreWeight = 250;
    //Shooting and destroyed sound of enemy
    [SerializeField] public AudioClip shootSound;
    [SerializeField] AudioClip destroySound;
    [SerializeField] AudioClip shootClip;
    //Particle effects
    [SerializeField] GameObject particleEmission;
    [SerializeField] GameObject addScoreParticleEmitter;
    //Bullet Gameobject
    [SerializeField] GameObject projectile;
    //Get player transform
    [SerializeField] Transform player;
    //Time between each shot
    [SerializeField] float startTimeBtwShots;

    // == private fields ==
    [SerializeField] [Range(0f, 1.0f)] private float shootVolume = 0.5f;
    private AudioSource audioSource;
    private float timeBtwShots;
    private float sfxVolume;
    //Damage of enemy if collided into.
    private float damage = 10f;
    private SoundController sc;
    private ScoreKeeper scoreKeeper;

    // == private methods ==
    private void Start()
    {
        //Get soundcontroller
        sc = SoundController.FindSoundController();
        //Find the player gameobject
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        sfxVolume = MusicPlayer.GetSFXVolume();
        audioSource = GetComponent<AudioSource>();
        //Gets current score value
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void Update()
    {
        //Shoot at the player when condition is true,else wait
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
            audioSource.PlayOneShot(shootClip, shootVolume);
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    //Get Damge value of gameObject
    public float GetDamage()
    {
        return damage;
    }
    //Takes in damage value and subtracts it from the current health value
    //Checks if health is greater than or equal to zero , if not call Die()
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
        // if the player hit, then update health bar and check should player be destroyed.
        //if bullet then update health bar destroy gameObject enemy, give player points.
        var player = whatHitMe.GetComponent<Player>();
        var bullet = whatHitMe.GetComponent<Bullet>();

        if (player)  // if (player != null)
        {
            player.Hit();
            Destroy(gameObject);
        }

        if (bullet)
        {
            bullet.Hit();
            Hit(bullet.GetDamage());
        }
    }
    //Method to destroy current gameObject
    public void Hit()
    {
        Destroy(gameObject);
    }
    //Activates particle explosions, add score particle , destroysound
    //Adds score to the players current score
    //Destroys gameobject 
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

