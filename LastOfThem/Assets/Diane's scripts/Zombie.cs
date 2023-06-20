using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

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
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceLastCheck >= chaseInterval)
        {
            timeSinceLastCheck = 0f;

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= detectionDistance)
            {
                StartChasing();
            }
            else
            {
                StopChasing();
            }
        }

        if (isChasing)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= attackDistance)
            {
                AttackPlayer();
                navMeshAgent.SetDestination(transform.position);
                isMoving = false;
            }
            else
            {
                navMeshAgent.SetDestination(player.position);
                isMoving = true;
            }
        }
        else if (behavior == ZombieBehavior.Patrol && !isMoving)
        {
            RandomDestination();
        }
        else if (behavior == ZombieBehavior.WalkBackAndForth && !isMoving)
        {
            WalkBackAndForth();
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
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

    private void AttackPlayer()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
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
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enemy is attacking the player");
        }
    }
}
