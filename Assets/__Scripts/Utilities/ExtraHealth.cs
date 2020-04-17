using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Meant to be able to add health to player not working.
 */
public class ExtraHealth : MonoBehaviour
{
    // == public fields ==
  
    // delegate type to use for event
    public delegate void HealthGained(ExtraHealth extraHealth);

    // create static method to be implemented in the listener
    public static HealthGained HealthGainedEvent;

    // == private fields ==
    private float addHealth = 10f;

    // == private methods ==
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var player = whatHitMe.GetComponent<Player>();

        Debug.Log("Hit Something");

        if (player)  // if (player != null)
        {
            Debug.Log("It was the player");
            Destroy(gameObject);
        }
    }
    public float GetAddHealth()
    {
        return addHealth;
    }
    
}




