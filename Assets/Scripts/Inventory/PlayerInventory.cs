using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory = new Inventory();

    public void Add(IItem newItem, out bool success)
    {
        inventory.AddItem(newItem, out success);
        RefreshUI();
    }

    public void RefreshUI() 
    {
        FindObjectOfType<InventoryUI>().RefreshUI(inventory.GetAllItems());
    }
}
