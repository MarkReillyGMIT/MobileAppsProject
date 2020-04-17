using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Controls the bullet the enemyAI shoots at the player.
 */

public class BulletAI : MonoBehaviour
{
    //Public Fields
    //Speed of the bullet
    [SerializeField] float speed;
    [SerializeField] GameObject particleEmission;

    //Private Fields
    private Transform player;
    private Vector2 target;
    //Damage of the bullet
    private float damage = 10f;

    void Start()
    {
        //Find Game object with player tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Sets target to the position of the player
        target = new Vector2(player.position.x, player.position.y);
    }

    void Update()
    {
        //Bullet moves towards the last known position of the player for that frame.
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //When the bullet reaches the position of the target call Hit()
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Hit();
        }
    }
    //Method to get the value of the bullets damage
    public float GetDamage()
    {
        return damage;
    }
    //Bullet explodes using particleEmmission and then destroys itself
    public void Hit()
    {
        if (particleEmission)
        {
            GameObject particleColor = Instantiate(particleEmission, gameObject.transform.position, Quaternion.identity) as GameObject;
            ParticleSystem ps = particleColor.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = ps.main;
            psmain.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        }

        Destroy(gameObject);
    }
}
