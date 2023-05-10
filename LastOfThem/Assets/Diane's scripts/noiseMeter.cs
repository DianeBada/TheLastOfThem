using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class noiseMeter : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fp;
    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController rb;

    public float noiseIncreaseRate = 0.2f; // the rate at which the noise level increases per second
    public float noiseThreshold = 1.0f; // the noise level at which zombies become alerted
    public float detectionRange = 10.0f; // the maximum distance at which zombies can be detected
    public LayerMask detectionLayers; // the layers to consider when detecting zombies

    private float currentNoiseLevel = 0.0f; // the current noise level
    //private bool isWalking = false; // whether the player is currently walking
    private int zombiesInRange = 0; // the number of zombies within detection range

    public Slider noiseSlider;

    private void Update()
    {
        if (rb.isWalking && zombiesInRange > 0)
        {
            Debug.Log("the character is walking and there are zombies here");
            currentNoiseLevel += noiseIncreaseRate * Time.deltaTime;
        }
        else
        {
            currentNoiseLevel = Mathf.Max(0.0f, currentNoiseLevel - noiseIncreaseRate * Time.deltaTime);
        }

        // update the slider value based on the current noise level
        noiseSlider.value = currentNoiseLevel / noiseThreshold;

        if(fp.isMoving)
        {
            Debug.Log("the player is walking - fps script");

        }
    }

    public void SetWalking(bool walking)
    {
        fp.isMoving = walking;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (detectionLayers == (detectionLayers | (1 << other.gameObject.layer))) // check if the collider is on one of the detection layers
        {
            Zombie zombie = other.gameObject.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombiesInRange++; // increase the count of zombies in range
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (detectionLayers == (detectionLayers | (1 << other.gameObject.layer))) // check if the collider is on one of the detection layers
        {
            Zombie zombie = other.gameObject.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombiesInRange--; // decrease the count of zombies in range
            }
        }
    }

    public bool IsAlerted()
    {
        return currentNoiseLevel >= noiseThreshold;
    }
}
