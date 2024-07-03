using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float HP;
    public float currentHp;
    public ParticleSystem blood;
    public GameObject Remains;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hit;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = HP;
    }

    public void DamageAi(float damage)
    {
        audioSource.PlayOneShot(hit);
        currentHp -= damage;
        if (currentHp <= 0)
        {
            AIDeath();
        }
        blood.Play();
    }
    public void AIDeath()
    {
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Instantiate(Remains, transform.position, randomRotation);
        Destroy(gameObject, 0.2f);
    }
}
