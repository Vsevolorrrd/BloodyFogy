using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    public float speed = 60f;
    public float damage = 40f;
    public float time = 1f;
    public Rigidbody2D rb;
    public GameObject impactEffects;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
        Invoke("Time", time);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().PlayerTakeDamage(damage);
        }
        if (collision.gameObject.tag == "AI")
        {
            collision.GetComponent<EnemyHealth>().DamageAi(damage);
        }

        // Destroy the projectile upon collision
        Destroy(gameObject);
    }
    void Time()
    {
        Destroy(gameObject);
    }
}
