using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float HP;
    public float currentHp;
    public ParticleSystem blood;
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
            Destroy(gameObject);
        }
        blood.Play();
    }
}
