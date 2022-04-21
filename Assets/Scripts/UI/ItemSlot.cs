using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [HideInInspector]
    public GameObject currentItem = null;
    public int itemIndex;
    InventoryUI inventoryUI;

    void Start() 
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void Refresh() 
    {
        if (transform.GetChild(0).childCount > 0) 
        {
            Destroy(transform.GetChild(0).GetChild(0).gameObject);
        }
        if (currentItem != null)
        {
            Instantiate(currentItem, transform.GetChild(0));
        }
    }

    public void Select() 
    {
        if (currentItem != null)
        {
            inventoryUI.SetViewedItem(currentItem);
        }
    }


}
