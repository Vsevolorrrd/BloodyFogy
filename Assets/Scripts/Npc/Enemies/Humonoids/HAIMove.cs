using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAIMove : MonoBehaviour
{
    public float speed;
    public float stopDistance;
    [Header("Numbers")]
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
