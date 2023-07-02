using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class LockerFunction : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;
    public GameObject indicator;
    public bool isOccupied = false;
    public static GameObject[] lockers;
    private GameObject closestLocker;


    private void Awake()
    {
        lockers = GameObject.FindGameObjectsWithTag("locker");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOccupied)
        {
            DisplayPrompt();
            if(Input.GetKeyDown(KeyCode.L))
            {
                HidePlayer();
            }
            // Display prompt to the player to hide (e.g., "Press E to hide")
            // You can implement this using UI elements or a custom prompt system
            // When the player interacts with the locker, call the HidePlayer method
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.SetActive(false);
        }
    }

    public void HidePlayer()
    {
        firstPersonController.isInsideLocker = true;
        closestLocker = GetClosestLocker();

        if (closestLocker != null)
        {
            // Set the current locker as occupied by the player
            isOccupied = true;
            // Face the camera towards the locker's direction
            Vector3 cameraDirection = closestLocker.transform.forward;
            firstPersonController.m_Camera.transform.forward = cameraDirection;

            // Place the camera in the center of the locker
            Vector3 cameraPosition = closestLocker.transform.position;
            firstPersonController.m_Camera.transform.position = cameraPosition;
        }
        // Hide the player inside the locker
        // Disable player controls or restrict movement
        // Adjust camera view or provide a first-person view from inside the locker
        // Set isOccupied to true
    }

    public void ExitLocker()
    {
        firstPersonController.isInsideLocker = false;
        isOccupied = false;

        firstPersonController.m_Camera.transform.position = firstPersonController.m_OriginalCameraPosition;
        firstPersonController.m_Camera.transform.rotation = firstPersonController.m_OriginalCameraRotation;

        // Allow the player to exit the locker
        // Enable player controls or restore movement
        // Adjust camera view back to normal
        // Set isOccupied to false
    }

    public void DisplayPrompt()
    {
        indicator.SetActive(true);
        Debug.Log("indicator is active");
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
