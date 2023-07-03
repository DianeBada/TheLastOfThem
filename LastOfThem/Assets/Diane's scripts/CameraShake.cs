using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;     // Duration of the shake effect
    public float shakeIntensity = 0.1f;    // Intensity of the shake effect

    private Vector3 originalPosition;      // Original position of the camera
    private float shakeTimer = 0f;


    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void ShakeCamera()
    {
        shakeTimer = shakeDuration;
    }


    // Update is called once per frame
    void Update()
    { 
   if (shakeTimer > 0f)
    {
        // Generate a random offset within a sphere and apply it to the camera's position
        Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
    transform.localPosition = originalPosition + shakeOffset;

        shakeTimer -= Time.deltaTime;
    }
    else
{
    // Reset the camera position
    transform.localPosition = originalPosition;
}
}
    }
