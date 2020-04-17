﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Controls the weapon for player
 */
[RequireComponent(typeof(AudioSource))]
public class WeaponsController : MonoBehaviour
{
    // == private fields ==
    //Bullet speed
    [SerializeField] private float bulletSpeed = 6.0f;
    //Bullet gameObject
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float firingRate = 0.25f;
    [SerializeField] private AudioClip shootClip;
    [SerializeField][Range(0f, 1.0f)] private float shootVolume = 0.5f;

    private Coroutine firingCoroutine;
    private GameObject bulletParent;
    private AudioSource audioSource;

    // == private methods ==
    private void Start()
    {
        bulletParent = GameObject.Find("BulletParent");
        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Fire bullet
            // implement a coroutine to fire
            firingCoroutine = StartCoroutine(FireCoroutine());
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            //StopAllCoroutines();    // not good, sledgehammer approach
            StopCoroutine(firingCoroutine);
        }
    }

    // coroutine returns an IEnumerator type
    private IEnumerator FireCoroutine()
    {
        while(true)
        {
            // create a bullet
            Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
            bullet.transform.position = transform.position;
            // play sound - AudioClip, Volume between 0 and 1
            // use a local AudioSource
            audioSource.PlayOneShot(shootClip, shootVolume);
            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            rbb.velocity = Vector2.right * bulletSpeed;
            // sleep for short time
            yield return new WaitForSeconds(firingRate);
        }
    }
}
