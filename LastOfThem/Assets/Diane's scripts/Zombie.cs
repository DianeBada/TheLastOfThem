using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public float speed = 1.0f;              // zombie movement speed
    public float detectionDistance = 5.0f;  // distance at which the zombie detects the player
    public Transform player;               // reference to the player's transform component

    private bool isChasing = false;        // flag to indicate if the zombie is chasing the player

    void Update()
    {
        // calculate the distance between the zombie and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // if the player is within detection distance and the zombie is not already chasing, start chasing
        if (distance <= detectionDistance && !isChasing)
        {
            isChasing = true;
        }

        // if the zombie is chasing, move towards the player
        if (isChasing)
        {
            transform.LookAt(player);                        // look at the player
            transform.position += transform.forward * speed * Time.deltaTime; // move towards the player
        }
        else
        {
            // if the zombie is not chasing, move randomly
            Vector3 randomDirection = Random.insideUnitSphere * 5.0f;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 5.0f, NavMesh.AllAreas);
            transform.position = Vector3.Lerp(transform.position, hit.position, speed * Time.deltaTime);
        }
    }
}
