using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.VFX;

public class Syringe : MonoBehaviour
{
    public Animator transformAnimator;
    private bool isEffectPlaying = false;
    private Color syringeColor;
    private GameManager gameManager;
    private GameObject gojoRedHollowVariant;
    private Zombie zombie;
    public GameObject zombieAvatar;
    public GameObject curedAvatar;
    public VideoPlayer explosionVideoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        zombie = FindObjectOfType<Zombie>();
        gameManager = FindObjectOfType<GameManager>();
        syringeColor = Color.white;
        Disappear();
        gojoRedHollowVariant = zombie.transform.Find("Gojo Red Hollow Variant").gameObject;
        transformAnimator = gojoRedHollowVariant.GetComponent<Animator>();
        transformAnimator.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Stabbed Zombie");
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
        transformAnimator.enabled = true;
        transformAnimator.Play("transformation_animation");
        StartCoroutine(DisableZombieAndShowHumanAvatar(zombie));
    }

    private void NoCureInjection(GameObject zombie)
    {
        Debug.Log("Zombie Not Cured");
        gameManager.ClearMixture();

        transformAnimator.enabled = true;
        isEffectPlaying = true;

        // Play the VFX animation for the zombie
        transformAnimator.Play("transformation_animation");

        // Disable the zombie game object after the VFX animation duration
        StartCoroutine(DisableZombieAfterVFX(zombie));

        // Call a method to handle the explosion video playback
        StartCoroutine(PlayExplosionVideo());
    }

    private IEnumerator DisableZombieAfterVFX(GameObject zombie)
    {
        yield return new WaitForSeconds(transformAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Disable the zombie game object
        zombie.SetActive(false);
    }

    private IEnumerator PlayExplosionVideo()
    {
        // Wait for the VFX animation to finish
        yield return new WaitForSeconds(transformAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Enable the explosion video player and play the video
        explosionVideoPlayer.gameObject.SetActive(true);
        explosionVideoPlayer.Play();

        // Wait for the video to finish playing
        yield return new WaitForSeconds((float)explosionVideoPlayer.length);

        // Disable the explosion video player
        explosionVideoPlayer.gameObject.SetActive(false);

        // Perform any necessary actions after the explosion video finishes playing
        isEffectPlaying = false;
        transformAnimator.enabled = false;
    }

    private IEnumerator DisableZombieAndShowHumanAvatar(GameObject zombie)
    {
        // Wait for the VFX animation to finish
        yield return new WaitForSeconds(transformAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Disable the zombie game object
        zombie.SetActive(false);

        // Instantiate the cured human game object at the same position and rotation
        GameObject curedHuman = Instantiate(curedAvatar, zombie.transform.position, zombie.transform.rotation);

        // Enable the cured human game object
        curedHuman.SetActive(true);
    }

    private void Disappear()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Syringe has now disappeared");
    }
}
