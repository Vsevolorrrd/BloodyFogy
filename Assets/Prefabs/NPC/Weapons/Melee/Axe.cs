using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public float damage = 40f;
    public LayerMask Enemy;
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Enemy)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.GetComponent<PlayerHealth>().PlayerTakeDamage(damage);
            }
            if (collision.gameObject.tag == "AI")
            {
                collision.GetComponent<EnemyHealth>().DamageAi(damage);
            }
        }
    }
}
