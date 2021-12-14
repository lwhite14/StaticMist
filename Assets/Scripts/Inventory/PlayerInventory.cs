using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory = new Inventory();

    public void Add(IItem newItem)
    {
        inventory.AddItem(newItem);
    }
}
