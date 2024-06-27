using UnityEngine;

public class OutlawBasic : Outlaw
{
    Transform player;
    public Animator anim;

    [Header("Numbers")]
    public float speed =7f;
    public float attackRange = 7f;

    [Header("Attack")]
    private float nextAttackTime;
    public float timeToThink;
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
        base.HandlePatroulState();
    }
    public override void HandleSearchState()
    {
        base.HandleSearchState();
    }
    public override void HandleCombatState()
    {
        base.HandleCombatState();
        if (currentTarget != null)
        {
            if (Vector2.Distance(transform.position, currentTarget.transform.position) > attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, currentTarget.transform.position) <= attackRange)
            {
                Fight();
            }
        }
    }
    public void Fight()
    {
        if (nextAttackTime <= 0)
        {
            Attack();
            nextAttackTime = timeToThink;
        }
        nextAttackTime -= Time.deltaTime;
    }
    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
}