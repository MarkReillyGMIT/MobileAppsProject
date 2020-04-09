using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent <AudioSource>();
    }

    // == public methods ==
    public void PlayOneShot(AudioClip clip)
    {
        if(clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void ToggleSounds()
    {
        // placeholder
        audioSource.mute = !audioSource.mute;
    }

    public static SoundController FindSoundController()
    {
        SoundController sc = FindObjectOfType<SoundController>();
        if(!sc)
        {
            Debug.LogWarning("Missing Sound Controller");
        }
        return sc;
    }


}
