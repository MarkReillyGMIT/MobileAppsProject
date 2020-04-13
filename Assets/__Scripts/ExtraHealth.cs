using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use this to manage collisions

public class ExtraHealth : MonoBehaviour
{
    // == public fields ==
  
    // delegate type to use for event
    public delegate void HealthGained(ExtraHealth extraHealth);

    // create static method to be implemented in the listener
    public static HealthGained HealthGainedEvent;



    // == private fields ==
   

    // == private methods ==
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        // parameter = what ran into me
        // if the player hit, then destroy the player
        // and the current enemy rectangle

        var player = whatHitMe.GetComponent<PlayerMovement>();

        Debug.Log("Hit Something");

        if (player)  // if (player != null)
        {
            Debug.Log("It was the player");
            // play crash sound here
            // destroy the player and the rectangle
            // give the player points/coins
            GameController.health += 1;
            Destroy(gameObject);
        }
    }

    private void PublishEnemyKilledEvent()
    {
        // make sure somebody is listening
        if (HealthGainedEvent != null)
        {
            HealthGainedEvent(this);
        }
    }

}




