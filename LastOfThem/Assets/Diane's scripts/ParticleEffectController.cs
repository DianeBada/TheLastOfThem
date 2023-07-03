using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectController : MonoBehaviour
{
    public CameraShake cameraShaker; // Reference to the CameraShake script
    public float startDelay = 0.001f; // Delay before starting the particle system
    public float stopDelay = 0.001f; // Delay before stopping the particle system

    private ParticleSystem[] particleSystems; // Array of child particle systems
    private bool isPlaying = false;

    private void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        isPlaying = false;
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }

    }

    public void PlayParticleSystem()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            Debug.Log("blood spluttering");
            StartCoroutine(PlayDelayed(startDelay));
        }
    }

    public void StopParticleSystem()
    {
        if (isPlaying)
        {
            isPlaying = false;
            StartCoroutine(StopDelayed(stopDelay));
        }
    }

    private IEnumerator PlayDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }

    private IEnumerator StopDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
    }
}
