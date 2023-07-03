using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class LockerFunction : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;
    public GameObject indicator;
    public bool isOccupied = false;
    public static GameObject[] lockers;
    private GameObject closestLocker;
    private bool isByLocker;
    private float repelDuration = 3f;
    private float repelDistance = 10f;
    public Light mainLight;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float originalLightIntensity;
    private float originalFOV;
    private void Start()
    {
        isByLocker = false;
    }

    private void Awake()
    {
        lockers = GameObject.FindGameObjectsWithTag("locker");
        originalLightIntensity = mainLight.intensity;
        originalFOV = 90;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOccupied)
        {
            DisplayPrompt();
            isByLocker = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !isOccupied && isByLocker)
        {
            Debug.Log("hiding the player");
            HidePlayer();
        }
        else if (Input.GetKeyDown(KeyCode.L) && isOccupied)
        {
            Debug.Log("Not hiding the player");

            ExitLocker();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.SetActive(false);
            isByLocker = false;
        }
    }

    public void HidePlayer()
    {
        originalPosition = firstPersonController.transform.position;
        originalRotation = firstPersonController.transform.rotation;

        firstPersonController.isInsideLocker = true;
        closestLocker = GetClosestLocker();

        if (closestLocker != null)
        {
            isOccupied = true;

            // Darken the environment
            mainLight.intensity = 0.1f; // Adjust the intensity as desired
                                        // Disable other lights if necessary

            // Limit the field of view (FOV)
            firstPersonController.m_Camera.fieldOfView = 20f; // Adjust the FOV as desired

            // Face the camera towards the locker's forward direction
            Vector3 lockerDirection = closestLocker.transform.forward;
            Quaternion targetRotation = Quaternion.LookRotation(lockerDirection);
            firstPersonController.m_Camera.transform.rotation = targetRotation;

            // Move the player forward by a few steps
            Vector3 forwardDirection = closestLocker.transform.forward;
            Vector3 newPosition = closestLocker.transform.position + forwardDirection * 5f;
            firstPersonController.transform.position = newPosition;

            // Rotate the player 180 degrees
            Quaternion reverseRotation = Quaternion.LookRotation(-forwardDirection);
            firstPersonController.transform.rotation = reverseRotation;

            // Disable player controls
            firstPersonController.enabled = false;

        }
    }


    public void ExitLocker()
    {
        if (!firstPersonController.isInsideLocker) return;

        firstPersonController.isInsideLocker = false;
        isOccupied = false;

        // Restore the original position and rotation
        firstPersonController.transform.position = originalPosition;
        firstPersonController.transform.rotation = originalRotation;

        // Restore the original camera properties
        mainLight.intensity = originalLightIntensity;
        firstPersonController.m_Camera.fieldOfView = originalFOV;

        // Enable player controls
        firstPersonController.enabled = true;

       
    }

    private IEnumerator ResumeChasing(Zombie zombie)
    {
        yield return new WaitForSeconds(repelDuration);
        zombie.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public void DisplayPrompt()
    {
        indicator.SetActive(true);
    }

    private GameObject GetClosestLocker()
    {
        GameObject closestLocker = null;
        float closestDistance = Mathf.Infinity;
        Vector3 playerPosition = firstPersonController.transform.position;
        Vector3 playerDirection = firstPersonController.transform.forward;

        foreach (GameObject locker in lockers)
        {
            // Calculate the direction from the player to the locker
            Vector3 lockerDirection = locker.transform.position - playerPosition;
            float distance = Vector3.Distance(locker.transform.position, playerPosition);

            // Check if the locker is in front of the player and closer than the current closest locker
            if (Vector3.Dot(playerDirection, lockerDirection) > 0 && distance < closestDistance)
            {
                closestLocker = locker;
                closestDistance = distance;
            }
        }

        return closestLocker;
    }
}



