using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlaw : NPC
{
    public State currentState = State.Patroul;
    public List<Transform> detectedEnemies = new List<Transform>(); // List to store detected enemies
    public Transform currentTarget;
    public float detectionRange = 18f;

    public float sixthSense = 40f;
    public float AggressionRange = 10f;

    [Header("Need to see?")]// For optimization
    public bool needToSee;

    [Header("Timers")]
    public float PerfomanceMaxTime = 1.0f;
    float perfomanceTimer;
    public float AssumingTime = 2f;
    float AssumingTimer;
    public float SearchingTime = 10f;
    float SearchingTimer;

    [Header("Patroiling")]
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    public float patrolSpeed = 2f;
    public float RandomWalkPoint = 7f;
    [HideInInspector] public Vector2 walkPoint;

    [Header("Searching")]
    [HideInInspector] public Vector2 LastPoint;
    [HideInInspector] public Vector2 walkPointForSearching;
    private bool search;

    public float fieldOfView = 90f;
    public float rotationSpeed = 100f;

    public virtual void Awake()
    {
        // to generate the first walk point
        RandomPatroul();
        SearchingTimer = SearchingTime;
        AssumingTimer = AssumingTime;
        search = true;
    }
    public enum State
    {
        Patroul,
        Search,
        Combat
    }

    public virtual void Update()
    {
        perfomanceTimer -= Time.deltaTime;
        if (perfomanceTimer <= 0) // For optimization
        {
            DetectEnemies();
            if (currentState == State.Combat)
            {
                SelectTarget();
            }
            perfomanceTimer = PerfomanceMaxTime;
        }
        switch (currentState)
        {
            case State.Patroul:
                HandlePatroulState();
                break;
            case State.Search:
                HandleSearchState();
                break;
            case State.Combat:
                HandleCombatState();
                break;
            default:
                // Handle default behavior or error state
                break;
        }
    }

    public virtual void HandlePatroulState()
    {
        // Example condition to switch to Chase state
        if (CanSeeTheEnemy())
        {
            currentState = State.Combat;
        }
        Patrol();
    }

    public virtual void HandleSearchState()
    {
        if (detectedEnemies.Count != 0)
        {
            Search();
            if (!CanSeeTheEnemy())
            {
                SearchingTimer -= Time.deltaTime;
                if (SearchingTimer <= 0)
                {
                    currentState = State.Patroul;
                    SearchingTimer = SearchingTime;
                    search = true;
                }
            }
            else
            {
                SearchingTimer = SearchingTime;
                currentState = State.Combat;
                search = true;
            }
        }
        else
        {
            currentState = State.Patroul;
        }
    }

    public virtual void HandleCombatState()
    {
        // Example condition to switch back to Chase state
        if (!CanSeeTheEnemy())
        {
            AssumingTimer -= Time.deltaTime;
            if (AssumingTimer <= 0)
            {
                currentState = State.Search;
                AssumingTimer = AssumingTime;
            }          
        }
        else
        {
            AssumingTimer = AssumingTime;
        }

        if (currentTarget != null)
        {
            Aim();
        }
    }
    public virtual bool CanSeeTheEnemy()
    {
        if (needToSee) return true;
        if (detectedEnemies.Count == 0) return false;

        foreach (Transform enemy in detectedEnemies)
        {
            Vector2 direction = (enemy.position - transform.position).normalized;
            float angle = Vector2.Angle(transform.up, direction);

            if (angle >= -fieldOfView && angle <= fieldOfView)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, WhatToSee);
                if (hit.collider != null && hit.transform == enemy)
                {
                    Debug.DrawRay(transform.position, direction * detectionRange, Color.red);
                    return true;
                }
            }
        }

        return false;
    }

    void DetectEnemies()
    {
        // Clear the list
        detectedEnemies.Clear();

        // to detect enemies
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sixthSense, enemyLayers);
        foreach (Collider2D collider in colliders)
        {
            // Add the detected enemy to the list
            detectedEnemies.Add(collider.transform);
        }
    }

    void SelectTarget()
    {
        if (detectedEnemies.Count == 0)
        {
            currentTarget = null;
            return;
        }

        Transform bestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform enemy in detectedEnemies)
        {
            float distance = (enemy.position - transform.position).sqrMagnitude;// Use squared distances for optimisation

            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = enemy;
            }
        }

        currentTarget = bestTarget;
    }
    void Aim()
    {
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;// adjusting rotation by 90
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    public virtual void Patrol()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            Transform targetWaypoint = waypoints[currentWaypoint];
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, patrolSpeed * Time.deltaTime);

            Vector2 direction = (targetWaypoint.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;// adjusting rotation by 90
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.2f)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                    currentWaypoint = 0;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, walkPoint, patrolSpeed * Time.deltaTime);

            Vector2 direction = (walkPoint - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;// adjusting rotation by 90
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, walkPoint) < 0.2f)
            {
                RandomPatroul();
            }
        }

    }
    private void RandomPatroul()
    {
        //Calculate random point in range
        float randomY = Random.Range(-RandomWalkPoint, RandomWalkPoint);
        float randomX = Random.Range(-RandomWalkPoint, RandomWalkPoint);

        walkPoint = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
    }
    private void Search()
    {
        if (search)
        {
            LastPoint = currentTarget.position;
            walkPointForSearching = LastPoint;
            search = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, walkPointForSearching, patrolSpeed * Time.deltaTime);

        Vector2 direction = (walkPointForSearching - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;// adjusting rotation by 90
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, walkPoint) < 0.2f)
        {
            //Calculate random point in range
            float randomY = Random.Range(-RandomWalkPoint, RandomWalkPoint);
            float randomX = Random.Range(-RandomWalkPoint, RandomWalkPoint);

            walkPointForSearching = new Vector2(LastPoint.x + randomX, LastPoint.y + randomY);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AggressionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}