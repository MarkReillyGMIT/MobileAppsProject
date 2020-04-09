using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// get the number of lives at the start from the GameController
// use this to set up the icons for lives
// in a loop?
// will need a prefab to use for the icons

public class LifeCounter : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private LifeIcon lifeIconPrefab; // coffee cup

    private int startingLives;  // read from the Game Controller
    private GameController gc;

    void Start()
    {
        // get the GameController object
        gc = FindObjectOfType<GameController>();
        if(gc)
        {
            // retrieve the starting lives value
            // set up a read only public property to return
            // the number of starting lives
            startingLives = gc.StartingLives;
            CreateIcons();
        }
    }

    private void CreateIcons()
    {
        // show the appropriate number of coffee cups on the screen
        // use a loop
        for(int i = 0; i < startingLives; i++)
        {
            LifeIcon icon = Instantiate(lifeIconPrefab, transform);
        }
    }

    // == public methods ==
    public void LoseOneLife()
    {

    }

}
