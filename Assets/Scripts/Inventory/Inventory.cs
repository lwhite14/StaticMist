using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<IItem> items = new List<IItem>();

    public Inventory() { }

    public Inventory(List<IItem> newItems)
    {
        items.AddRange(newItems);
    }

    public List<IItem> GetAllItems()
    {
        return items;
    }

    public IItem GetItem(int index)
    {
        if ((index >= 0) && (index < items.Count))
        {
            return items[index];
        }
        return null;
    }

    public void SetAllItems(List<IItem> newItems)
    {
        items = newItems;
    }

    public void SetItem(IItem newItem, int index)
    {
        if ((index >= 0) && (index < items.Count))
        {
            items[index] = newItem;
        }
    }

    public void AddItem(IItem newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
    }

    public void RemoveItem(IItem item)
    {
        items.Remove(item);
    }
}
