using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.VFX;

public class Syringe : MonoBehaviour
{
    public VideoPlayer explosionVideoPlayer;
    public float vfxDuration = 2.0f; // Duration of the VFX animation in seconds
    private Color syringeColor;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        syringeColor = Color.white;
        Disappear();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            InjectZombie(collision.gameObject);
        }
    }

    public Color GetSyringeColor()
    {
        return syringeColor;
    }

    public void SetSyringeColor(Color newColor)
    {
        this.syringeColor = newColor;
        var syringeRenderer = this.gameObject.GetComponent<MeshRenderer>();
        syringeRenderer.materials[0].color = newColor;
    }

    private void InjectZombie(GameObject zombie)
    {
        Debug.Log("Zombie Injected");
        if (gameManager.HasCure())
        {
            CureInjection(zombie);
        }
        else
        {
            NoCureInjection(zombie);
        }
    }

    private void CureInjection(GameObject zombie)
    {
        Debug.Log("Zombie Cured");
        // TODO: Implement the cure transformation logic
    }

    private void NoCureInjection(GameObject zombie)
    {
        Debug.Log("Zombie Not Cured");
        gameManager.ClearMixture();

        // Play the VFX animation for the zombie
        Animator animator = zombie.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.Play("Gojo Red Hollow Variant Animation");
        }

        // Disable the zombie game object after the VFX animation duration
        StartCoroutine(DisableZombieAfterVFX(zombie));

        // Call a method to handle the end of the explosion video and perform any necessary actions
        StartCoroutine(ExplosionVideoComplete(zombie));
    }

    private IEnumerator DisableZombieAfterVFX(GameObject zombie)
    {
        yield return new WaitForSeconds(vfxDuration);

        // Disable the zombie game object
        zombie.SetActive(false);
    }

    private IEnumerator ExplosionVideoComplete(GameObject zombie)
    {
        yield return new WaitForSeconds(vfxDuration + (float)explosionVideoPlayer.length);

        // Play the explosion video
        explosionVideoPlayer.Play();

        // Disable the explosion video player when it finishes playing
        StartCoroutine(DisableExplosionVideoPlayer());
    }


    private IEnumerator DisableExplosionVideoPlayer()
    {
        yield return new WaitForSeconds((float)explosionVideoPlayer.length);

        // Disable the explosion video player
        explosionVideoPlayer.gameObject.SetActive(false);

        // Disable the VideoPlayer component
        explosionVideoPlayer.enabled = false;

        // TODO: Handle any necessary actions after the explosion video finishes playing
    }
    private void Disappear()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Syringe has now disappeared");
    }
}
