﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Controls enemy 
 */
public class Enemy : MonoBehaviour
{
    // == public fields ==
    //Health of enemy
    [SerializeField] float health = 15f;
    //Amount of points for killing this enemy
    [SerializeField] int scoreWeight = 150;
    // sounds for getting hit by bullet, spawning, shooting
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] public AudioClip shootSound;
    [SerializeField] public AudioClip destroySound;
    //Particle Explosions for when the user gets points, and gameObject dies.
    [SerializeField] GameObject particleEmission;
    [SerializeField] GameObject addScoreParticleEmitter;

    // == private fields ==
    private float sfxVolume;
    //Damage value of hitting enemy
    private float damage = 10f;
    private SoundController sc;
    private ScoreKeeper scoreKeeper;
    private Vector3 sfxPosition;

    // == private methods ==
    private void Start()
    {
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
        // if the player hit, then update health bar and check should player be destroyed.
        //if bullet then destroy gameObject enemy, give player points.

        var player = whatHitMe.GetComponent<Player>();
        var bullet = whatHitMe.GetComponent<Bullet>();

        if(player)
        {
            player.Hit();
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
