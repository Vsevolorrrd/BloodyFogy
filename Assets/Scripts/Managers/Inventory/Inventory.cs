using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public int space = 12;

    #region Singleton

    public static Inventory InventoryInstance;

    private void Awake()
    {
        if (InventoryInstance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        InventoryInstance = this;
    }
    #endregion

    public delegate void OnItemChange();
    public  OnItemChange onItemChangeCallback;
    public List<Item> items = new List<Item>();

    [Header("Hot slot")]
    int selectedslot = -1;

    [Header("Inventoryslots")]
    public InventorySlot[] inventoryslots;
    //public Transform ItemsParent;
    //public GameObject ItemsParentObject;

    [Header("Adding Items")]
    public GameObject inventoryItemPrefab;
    public int maxStack = 10;
    DraggableItem ItemInSlot;

    /*void Awake ()
    {
        inventoryslots = new InventorySlot[ItemsParent.childCount];

        // Loop through each child of ItemsParent and add components to inventoryslots
        for (int i = 0; i < ItemsParent.childCount; i++)
        {
            // Assuming InventorySlot is a component you want to add to each child
            inventoryslots[i] = ItemsParent.GetChild(i).GetComponent<InventorySlot>();
        }
    }*/
    private void Start()
    {
        ChangeSelectedSlot(0);
    }
    public bool Add(Item item)
    {
        //Adding stackable item 
        for (int i = 0; i < inventoryslots.Length; i++)
        {
            InventorySlot slot = inventoryslots[i];
            DraggableItem ItemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (ItemInSlot != null && ItemInSlot.item == item && ItemInSlot.count < maxStack && ItemInSlot.item.stackable == true)
            {
                ItemInSlot.count++;
                ItemInSlot.RefreshCount();
                return true;
            }
        }

        //Adding single item 
        for (int i = 0; i < inventoryslots.Length; i++)
        {
            InventorySlot slot = inventoryslots[i];
            DraggableItem ItemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (ItemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
            return false;
    }
    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        DraggableItem dragItem = newItemGo.GetComponent<DraggableItem>();
        dragItem.InitialiseItem(item);
    }
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedslot >= 0)
        {
            inventoryslots[selectedslot].DeSelect();
        }
        inventoryslots[newValue].Select();
        selectedslot = newValue;
    }
    private void Update()
    {
        //Hot slots controls
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString,out int number);
            if (isNumber && number > 0 && number < 7)
            {
                ChangeSelectedSlot(number - 1);
                HotSlotsManager.HotSlotsInstance.DeleteItemInHotSlots();//OPTIMISE LATER!!!
                InventorySlot slot = inventoryslots[selectedslot];
                ItemInSlot = slot.GetComponentInChildren<DraggableItem>();
                if (ItemInSlot != null)
                {
                    ItemInSlot.Activate();                   
                }
                else
                {
                    Debug.Log("No item");
                }
            }
        }
    }
    public void itemUsedOnce()
    {
        ItemInSlot.count--;
        if(ItemInSlot.count <= 0)
        {
            Destroy(ItemInSlot.gameObject);
            HotSlotsManager.HotSlotsInstance.DeleteItemInHotSlots();

        }
        else
        {
            ItemInSlot.RefreshCount();
        }
    }
    public void Remove(Item item)
    {
        Destroy(item);
    }
}
