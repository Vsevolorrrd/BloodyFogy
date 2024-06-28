using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float HP;
    public float currentHp;
    public ParticleSystem blood;
    public GameObject Remains;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = HP;
    }

    public void DamageAi(float damage)
    {
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
