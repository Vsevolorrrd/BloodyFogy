using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unarmed : MonoBehaviour
{
    public GameObject unarmed;
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public LayerMask Enemy;
    public float damage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    void Attack()
    {
        damage = 3 * PlayerStats.PlayerStatsInstance.Strength.GetValue();
        // animator.SetTrigger("Attack");

        Collider2D[] hitedEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Enemy);

        foreach(Collider2D enemy in hitedEnemies)
        {
            enemy.GetComponent<EnemyHealth>().DamageAi(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
