﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Controls movement of enemy
 */
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour : MonoBehaviour
{
    // == private fields ==
    //Speed of enemy
    [SerializeField] private float speed = 5.0f;
    private Rigidbody2D rb;

    // == private methods ==
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        //Sets direction and speed of enemy
        rb.velocity = new Vector2(-1 * speed, 0);
    }

    // == public method ==
    //Sets speed of enemy
    public void SetMoveSpeed(float enemySpeed)
    {
        this.speed = enemySpeed;
    }

}
