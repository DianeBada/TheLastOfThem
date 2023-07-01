using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSound : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 0f; // Set spatial blend to 0 for non-spatialized sound
        audioSource.Play(); // Play the radio sound
    }
}
