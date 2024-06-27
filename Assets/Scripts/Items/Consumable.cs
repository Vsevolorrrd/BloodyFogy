using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "New consumables", menuName = "Scriptable object/Consumables")]
public class Consumable : Item
{
    public GameObject HotslotItemPrefab;
    public override void UseInHotSlot()
    {
        HotSlotsManager.HotSlotsInstance.SpawnNewItemInHotSlots(HotslotItemPrefab);
        base.UseInHotSlot();
    }
    public override void UseInInventory()
    {
        base.UseInInventory();
        InventoryUI.InventoryUIinstance.OpenInventoryInteractionMenu1();
    }
}
