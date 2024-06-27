using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class HotSlotsManager : MonoBehaviour
{
    #region Singleton

    public static HotSlotsManager HotSlotsInstance;

    private void Awake()
    {
        if (HotSlotsInstance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        HotSlotsInstance = this;
    }
    #endregion
    public Transform Hotslots;
    GameObject newItem;
    // Start is called before the first frame update

    public void SpawnNewItemInHotSlots(GameObject HotslotItemPrefab)
    {
        newItem = Instantiate(HotslotItemPrefab, Hotslots.transform);
    }
    public void DeleteItemInHotSlots()
    {
        Destroy(newItem);
    }
}