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

    private void Start()
    {
        isByLocker = false;
    }

    private void Awake()
    {
        lockers = GameObject.FindGameObjectsWithTag("locker");
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
        firstPersonController.isInsideLocker = true;
        closestLocker = GetClosestLocker();

        if (closestLocker != null)
        {
            isOccupied = true;

            // Calculate the offset between player and locker
            Vector3 playerOffset = firstPersonController.transform.position - closestLocker.transform.position;

            // Face the camera towards the locker's forward direction
            Vector3 lockerDirection = closestLocker.transform.forward;
            Quaternion targetRotation = Quaternion.LookRotation(lockerDirection);
            firstPersonController.m_Camera.transform.rotation = targetRotation;

            // Place the player in the middle of the locker
            Vector3 lockerCenter = closestLocker.transform.position;
            firstPersonController.transform.position = lockerCenter + playerOffset;
        }
    }

    public void ExitLocker()
    {
        if (!firstPersonController.isInsideLocker) return;

        firstPersonController.isInsideLocker = false;
        isOccupied = false;

        // Get the position of the exitPos child object
        Transform exitPos = closestLocker.transform.Find("exitPos");

        // Move the player to the exit position
        firstPersonController.transform.position = exitPos.position;
        firstPersonController.transform.rotation = exitPos.rotation;

        // Restore the original camera position and rotation
        // firstPersonController.m_Camera.transform.position = firstPersonController.m_OriginalCameraPosition;
        // firstPersonController.m_Camera.transform.rotation = firstPersonController.m_OriginalCameraRotation;

        // Repel nearby zombies
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        foreach (Zombie zombie in zombies)
        {
            Vector3 repelDirection = zombie.transform.position - transform.position;
            Vector3 repelForce = repelDirection.normalized * repelDistance;

            // Apply repel force to push the zombie away from the locker
            Rigidbody zombieRigidbody = zombie.GetComponent<Rigidbody>();
            if (zombieRigidbody != null)
            {
                zombieRigidbody.AddForce(repelForce, ForceMode.Impulse);
            }
        }
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



