using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public Item item;
    public int count = 1;

    [Header("UI")]
    public Image image;
    public TMP_Text CountText;

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.icon;
        RefreshCount();
    }
    public void RefreshCount()
    {
        CountText.text = count.ToString();
        bool textActive = count > 1;
        CountText.gameObject.SetActive(textActive);
    }
    public void Activate()
    {
        Debug.Log("Activate " + item.name);
        item.UseInHotSlot();
    }
    public void ActivateInInventory()
    {
        item.UseInInventory();
    }

    #region Draggimg
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        Debug.Log("End Drag");
    }
    #endregion

}
