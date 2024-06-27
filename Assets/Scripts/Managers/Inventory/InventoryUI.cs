using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region Singleton

    public static InventoryUI InventoryUIinstance;

    private void Awake()
    {
        if (InventoryUIinstance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        InventoryUIinstance = this;
    }
    #endregion
    public GameObject inventoryUI;
    public KeyCode inventory;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inventory))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
    public void OpenInventoryInteractionMenu1()
    {

    }
}
