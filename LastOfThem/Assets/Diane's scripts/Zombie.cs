using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public float speed = 1.0f;              // zombie movement speed
    public float detectionDistance = 5.0f;  // distance at which the zombie detects the player
    public float attackDistance = 1.5f;     // distance at which the zombie attacks the player
    public float damage = 10.0f;            // amount of damage the zombie inflicts on the player per attack
    public float chaseInterval = 1.0f;      // interval at which the zombie checks if the player is within detection distance

    private Transform player;               // reference to the player's transform component
    private NavMeshAgent navMeshAgent;      // reference to the zombie's NavMeshAgent component
    private bool isChasing = false;         // flag to indicate if the zombie is chasing the player
    private bool isMoving = false;          // flag to indicate if the zombie is moving to a new destination
    private float timeSinceLastCheck = 0f;  // time since the zombie last checked if the player is within detection distance

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        RandomDestination();
    }

    private void Update()
    {
        timeSinceLastCheck += Time.deltaTime;

        // check if it's time to check if the player is within detection distance
        if (timeSinceLastCheck >= chaseInterval)
        {
            timeSinceLastCheck = 0f;

            float distance = Vector3.Distance(transform.position, player.position);

            // if the player is within detection distance, start chasing the player
            if (distance <= detectionDistance)
            {
                StartChasing();
            }
            // otherwise, stop chasing the player
            else
            {
                StopChasing();
            }
        }

        if (isChasing)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            // if the player is within attack distance, attack the player
            if (distance <= attackDistance)
            {
                AttackPlayer();
                navMeshAgent.SetDestination(transform.position);
                isMoving = false;
            }
            // otherwise, continue chasing the player
            else
            {
                navMeshAgent.SetDestination(player.position);
                isMoving = true;
            }
        }
        else if (!isMoving)
        {
            // if the zombie is not already moving, set a new random destination
            RandomDestination();
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // if the zombie has reached its destination, set a new random destination
            RandomDestination();
        }
    }

    private void RandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * detectionDistance;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, detectionDistance, 1);
        navMeshAgent.SetDestination(hit.position);
        isMoving = true;
    }

    private void AttackPlayer()
    {
        // inflict damage on the player
        //player.GetComponent<Health>().TakeDamage(damage);
    }

    public void StartChasing()
    {
        isChasing = true;
        navMeshAgent.SetDestination(player.position);
    }

    public void StopChasing()
    {
        isChasing = false;
        navMeshAgent.SetDestination(transform.position);
        isMoving = false;
    }
}
