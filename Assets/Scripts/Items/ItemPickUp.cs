using UnityEngine;

public class ItemPickUp : Interactble
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp(item);
    }
    void PickUp(Item item)
    {
        Debug.Log("Picking up " + item.name);
        //add to inventory
        bool wasPickedUp = Inventory.InventoryInstance.Add(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("NO SPACE");
        }
    }
}
