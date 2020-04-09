using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponsController : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float bulletSpeed = 6.0f;
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
            // one way to fire
            //FireBullet();
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
            //AudioSource.PlayClipAtPoint(shootClip, Camera.main.transform.position,shootVolume);
            // use a local AudioSource
            audioSource.PlayOneShot(shootClip, shootVolume);

            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            rbb.velocity = Vector2.right * bulletSpeed;
            // sleep for short time
            yield return new WaitForSeconds(firingRate); // pick a number!!!
        }
    }

    //private void FireBullet()
    //{
    //    // instantiate bullet
    //    Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
    //    // give it the same position as the player
    //    bullet.transform.position = transform.position;
    //    // give it velocity and move right
    //    Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
    //    //rbb.velocity = new Vector2(1 * speed, 0);
    //    rbb.velocity = Vector2.right * bulletSpeed;
    //}
}
