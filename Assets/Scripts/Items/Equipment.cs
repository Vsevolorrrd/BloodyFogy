using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Scriptable object/Equipment")]
public class Equipment : Item
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
    }
}
