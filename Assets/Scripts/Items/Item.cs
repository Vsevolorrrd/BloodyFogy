using UnityEngine;

//where the item is created
[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool stackable;

    public virtual void UseInHotSlot()
    {
        //use item in hotslots
        Debug.Log("Using " +  name);
    }
    public virtual void UseInInventory()
    {
        //use item
        Debug.Log("Using " + name + " in inventory");
    }
    public void RemoveFromInventory()
    {
        Inventory.InventoryInstance.Remove(this);
    }
}
