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
        if (currentItem != null) 
        {
            Instantiate(currentItem, gameObject.transform.GetChild(0));
        }
    }

    public void Select() 
    {
        inventoryUI.SetViewedItem(currentItem);  
    }


}
