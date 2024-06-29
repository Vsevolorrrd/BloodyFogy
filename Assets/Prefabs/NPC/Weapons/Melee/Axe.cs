using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public float damage = 40f;
    public GameObject owner;  // Reference to the owner of the weapon
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // no more self killing
        if (collision.gameObject != owner)
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
