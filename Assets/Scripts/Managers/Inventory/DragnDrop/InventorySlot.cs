using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, baseColor;

    private void Awake()
    {
        DeSelect();
    }
    public void Select()
    {
        image.color = selectedColor;
    }
    public void DeSelect()
    {
        image.color = baseColor;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            DraggableItem draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;

        }
    }
    public void UseInInventory()
    {
        DraggableItem ItemInSlot = GetComponentInChildren<DraggableItem>();
        ItemInSlot.ActivateInInventory();
    }
}
