using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class LockerFunction : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;
    public GameObject indicator;
    public bool isOccupied = false;

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
        // Hide the player inside the locker
        // Disable player controls or restrict movement
        // Adjust camera view or provide a first-person view from inside the locker
        // Set isOccupied to true
    }

    public void ExitLocker()
    {
        // Allow the player to exit the locker
        // Enable player controls or restore movement
        // Adjust camera view back to normal
        // Set isOccupied to false
    }

    public void DisplayPrompt()
    {
        indicator.SetActive(true);
    }
}
