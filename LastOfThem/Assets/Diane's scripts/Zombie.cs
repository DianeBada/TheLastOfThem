using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{

    public AudioSource audioSource;
    private bool isPlayingSound = false;
    private bool hasAttackedPlayer = false;

    private Animator animator;


    public enum ZombieBehavior
    {
        Patrol,
        Stationary,
        WalkBackAndForth
    }

    public float speed = 1.0f;
    public float detectionDistance = 5.0f;
    public float attackDistance = 1.5f;
    public float damage = 10.0f;
    public float chaseInterval = 1.0f;
    public float maxDetectionDistance = 10f;

    public ZombieBehavior behavior;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private bool isChasing = false;
    private bool isMoving = false;
    private float timeSinceLastCheck = 0f;

    // For WalkBackAndForth behavior
    private Vector3 startPosition;
    private Vector3 patrolPoint;

    private void Start()
    {
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        audioSource = GetComponent<AudioSource>();

        if (behavior == ZombieBehavior.Patrol)
        {
            RandomDestination();
        }
        else if (behavior == ZombieBehavior.WalkBackAndForth)
        {
            startPosition = transform.position;
            patrolPoint = startPosition + new Vector3(5f, 0f, 0f);
        }

    }

    private void Update()
    {
        if (this.behavior == Zombie.ZombieBehavior.Stationary)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", this.isMoving);

        }

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
                isPlayingSound = false;
            }

            if (distance <= detectionDistance && !isPlayingSound)
            {
                PlayZombieSound();
            }
        }

        if (hasAttackedPlayer)
        {
            ReturnToStartPosition();

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

        else if (behavior == ZombieBehavior.WalkBackAndForth && !isMoving)
        {
            WalkBackAndForth();
        }

       
    }

    private void PlayZombieSound()
    {
        audioSource.Play();
        isPlayingSound = true;
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

    private void WalkBackAndForth()
    {
        transform.position = Vector3.Lerp(startPosition, patrolPoint, Mathf.PingPong(Time.time * speed, 1f));
    }

    private void SwapPatrolPoints()
    {
        Vector3 temp = startPosition;
        startPosition = patrolPoint;
        patrolPoint = temp;

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

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("enemy is attacking the player");
        }
    }

    public void AttackPlayer()
    {
        if (!hasAttackedPlayer)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            hasAttackedPlayer = true;
            navMeshAgent.SetDestination(startPosition);
            isChasing = false;
            isMoving = true;
        }
    }

    private void ReturnToStartPosition()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            hasAttackedPlayer = false;
            if (behavior == ZombieBehavior.Patrol)
            {
                RandomDestination();
            }
            else if (behavior == ZombieBehavior.WalkBackAndForth)
            {
                SwapPatrolPoints();
            }
        }
    }
}
