using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public UnityEvent OnShoot;   
    public Transform gunpoint;
    public RaycastHit2D ray;
    public LayerMask objectsToHit;
    public Animator anim;

    [Header("bools")]
    public bool Automatic;
    private bool reloading;

    [Header("Counters")]
    private float CurrentCooldown;
    private int bulletsLeft;

    [Header("Stats")]
    public float damage = 100;
    public float FireCooldown;
    public float weaponRange;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public ParticleSystem GunSmoke;

    [Header("Audio")]
    public AudioSource Shot;
    public AudioClip shot;

    // Start is called before the first frame update
    void Start()
    {
        CurrentCooldown = FireCooldown; 
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            anim.SetBool("Aim", true);

            if (Automatic)
            {
                if (Input.GetMouseButton(0))
                {
                    if (CurrentCooldown <= 0f)
                    {
                        OnShoot?.Invoke();
                        Shoot();
                        CurrentCooldown = FireCooldown;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (CurrentCooldown <= 0f)
                    {
                        OnShoot?.Invoke();
                        Shoot();
                        CurrentCooldown = FireCooldown;
                    }
                }
            }
        }
        else
        {
            anim.SetBool("Aim", false);
        }
        CurrentCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && !reloading) Reload();
    }
    private void Shoot()
    {
        //Raycast
        var ray = Physics2D.Raycast(gunpoint.position, transform.up, weaponRange, objectsToHit);

        if (ray.collider != null)
        {
            if (ray.collider.CompareTag("AI"))
            {
                ray.transform.GetComponent<EnemyHealth>().DamageAi(damage);
            }
            Debug.Log(ray.transform.name);
        }

        //shot sound
        Shot.PlayOneShot(shot);

        //Graphics
        CameraShake.CameraShakeInstance.CamShake();
        muzzleFlash.Play();
        GunSmoke.Play();

        bulletsLeft--;
    }
    private void Reload()
    {

    }
}