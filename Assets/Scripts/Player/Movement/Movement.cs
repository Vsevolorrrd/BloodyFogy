using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;
    public CameraMovement CamMove;
    public KeyCode SprintKey;
    float StartingSpeed;
    float x;
    float modifier;

    private void Start()
    {
        StartingSpeed = moveSpeed;
    }
    void Update()
    {
        // Get the horizontal and vertical input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        if ( Input.GetKey(SprintKey))
        {
            modifier = 1f + PlayerStats.PlayerStatsInstance.Dexterity.GetValue() / 10f;
        }
        else
        {
            modifier = 1f;
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * modifier * Time.fixedDeltaTime);
        float angle = Mathf.Atan2(CamMove.direction.y, CamMove.direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
