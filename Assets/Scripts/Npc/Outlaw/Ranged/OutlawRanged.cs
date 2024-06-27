using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlawRanged : Outlaw
{
    Transform player;
    Transform targetCover;
    public LayerMask whatIsCover;
    public bool InCover = false;

    [Header("Numbers")]
    public float speed;
    public float timeToThink;
    public float chaseRange, CoverRange, attackRange;

    [Header("Shooting")]
    public float timeBetweenShots;
    private float nextShootTime;
    public Transform GunPoint;
    public GameObject bulletPrefab;

    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void HandlePatroulState()
    {
        base.HandlePatroulState ();
    }
    public override void HandleSearchState()
    {
        base.HandleSearchState();
    }
    public override void HandleCombatState()
    {
        base.HandleCombatState();
        if(currentTarget != null)
        {
            if (Vector2.Distance(transform.position, currentTarget.transform.position) >= chaseRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
            }
            // buffer zone
            if (Vector2.Distance(transform.position, currentTarget.transform.position) <= attackRange)
            {
                SearchForCover();
            }
        }
    }
    public void SearchForCover()
    {
        if (!InCover)
        {
            Collider2D[] coverColliders = Physics2D.OverlapCircleAll(transform.position, CoverRange, whatIsCover);

            Debug.Log("Search for Cover");

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

                // closest cover = target
                targetCover = closestCover.transform;

                // Move
                transform.position = Vector2.MoveTowards(transform.position, targetCover.position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, targetCover.position) <= 0.5f)
                {
                    Debug.Log("Found Cover");
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
    public void ChangeCover()
    {
        InCover = false;
    }
    public void Attack()
    {
        //Attack
        if (currentTarget != null)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        if (nextShootTime <= 0)
        {
            Instantiate(bulletPrefab, GunPoint.position, GunPoint.rotation);
            nextShootTime = timeBetweenShots;
        }
        nextShootTime -= Time.deltaTime;
    }
}