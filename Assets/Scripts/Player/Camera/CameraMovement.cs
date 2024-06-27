using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera Cam;
    public Transform player;
    public float threshhold;
    public float ZoomOut;
    public float speed = 5.0f; // Speed of the camera movement
    public Vector3 direction;
    public KeyCode ZoomButton = KeyCode.LeftControl;


    void Update()
    {
        // Get the mouse position in the world
        Vector3 mousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - player.position);
        if (direction.magnitude > threshhold)
        {
            direction = direction.normalized * threshhold;
        }
        
        if (Input.GetKeyDown(ZoomButton))
        {
            threshhold = ZoomOut;
        }
        if (Input.GetKeyUp(ZoomButton))
        {
            threshhold = 4f;
        }
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + direction, speed * Time.deltaTime);
    }
}