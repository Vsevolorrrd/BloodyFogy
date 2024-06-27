using UnityEngine;

public class Interactble : MonoBehaviour
{
    public float InteractionDistance = 5f;

    Transform player;
    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }
    public virtual void Interact()
    {
        //Can be overwriten
        Debug.Log("Interacting with " + transform.name);
    }
    //radious for editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, InteractionDistance);
    }
    //Check for interaction
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
        {
            float distance = Vector2.Distance(player.position, transform.position);
            if (distance <= InteractionDistance)
            {
                Interact();
            }
        }
    }
}
