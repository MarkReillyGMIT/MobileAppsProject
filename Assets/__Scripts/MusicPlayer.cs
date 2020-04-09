using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        SetupSingleton();
    }

    // add a method to setup as a singleton
    private void SetupSingleton()
    {
        // this method gets called on creation
        // check for any other objects of the same type
        // if there is one, then use that one.
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);    // destroy the current object
        }
        else
        {
            // keep the new one
            DontDestroyOnLoad(gameObject);  // persist across scenes
        }
    }

}
