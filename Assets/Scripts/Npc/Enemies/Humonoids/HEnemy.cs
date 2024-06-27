using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HEnemy : MonoBehaviour
{

    Transform player;
    Transform target;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsCover;
    public bool playerInSightRange, playerInAttackRange, playerInChaseRange, playerInAgresionRange;
    public bool InCover, Agresion = false;

    [Header("Numbers")]
    public float speed;
    public float timeToThink;
    public float stopDistance;
    public float sightRange, attackRange, ChaseRange, AgresionRange, CoverRange;
    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //Check for sight and attack range
        playerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);
        playerInChaseRange = Physics2D.OverlapCircle(transform.position, ChaseRange, whatIsPlayer);
        playerInAgresionRange = Physics2D.OverlapCircle(transform.position, AgresionRange, whatIsPlayer);

        if (!playerInSightRange && !playerInChaseRange && !playerInAttackRange) Patroul();
        if (playerInSightRange && !playerInChaseRange && !playerInAttackRange) PlayerInSight();
        if (playerInAgresionRange)
        {
            Debug.Log("Stop!");
            Agresion = true;
        }
        if (playerInSightRange && playerInChaseRange && !playerInAttackRange && Agresion) Chase();
        if (playerInSightRange && playerInChaseRange && playerInAttackRange && Agresion) SearchForCover();

    }
    public virtual void Patroul()
    {
        
    }
    public virtual void PlayerInSight()
    {
        Agresion = false;
    }
    public virtual void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
    public virtual void SearchForCover()
    {
        if (!InCover)
        {
            Collider2D[] coverColliders = Physics2D.OverlapCircleAll(transform.position, CoverRange, whatIsCover);

            Debug.Log("Search for cover");

            if (coverColliders.Length > 0)
            {
                // Find the closest cover
                Collider2D closestCover = coverColliders[0];
                float closestDistance = Vector2.Distance(transform.position, closestCover.transform.position);

                foreach (Collider2D coverCollider in coverColliders)
                {
                    float distanceToCover = Vector2.Distance(transform.position, coverCollider.transform.position);
                    if (distanceToCover < closestDistance) // compares distance of current cover, to previous one
                    {
                        closestDistance = distanceToCover; // sets new closest distance
                        closestCover = coverCollider;
                    }
                }

                // Set the closest cover as the target
                target = closestCover.transform;

                // Move towards the cover
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, target.position) <= 0.4f)
                {
                    InCover = true;
                    Invoke("ChangeCover", timeToThink);
                }
            }
            else
            {
                InCover = true;
                Invoke("ChangeCover", timeToThink);
            }
        }
        else
        {
            // No cover found
            Attack();
        }
    }
    public virtual void ChangeCover()
    {
        InCover = false;
    }

    public virtual void Attack()
    {
        //Attack
        Debug.Log("Attacking!");
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AgresionRange);
    }
}
