using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class InventoryEditTests
{
    Inventory inventory;
    PlayerInventory playerInventory;

    [SetUp]
    public void SetUp()
    {
        playerInventory = new GameObject().AddComponent<PlayerInventory>();
        inventory = playerInventory.inventory;
    }

    [Test]
    public void InventoryGetAllItems_ReturnItems()
    {
        List<IItem> items = inventory.GetAllItems();
        Assert.AreEqual(0, items.Count);
        inventory.items.Add(new GameObject().AddComponent<Key>());
        inventory.items.Add(new GameObject().AddComponent<MedKit>());
        items = inventory.GetAllItems();
        Assert.AreEqual(2, items.Count);
        Assert.AreEqual(typeof(Key), items[0].GetType());
        Assert.AreEqual(typeof(MedKit), items[1].GetType());
    }

    [Test]
    public void InventoryGetItem_ReturnItem()
    {
        List<IItem> items = inventory.GetAllItems();
        Assert.AreEqual(null, inventory.GetItem(0));
        Assert.AreEqual(null, inventory.GetItem(16));
        inventory.items.Add(new GameObject().AddComponent<Key>());
        inventory.items.Add(new GameObject().AddComponent<MedKit>());
        Assert.AreEqual(typeof(Key), inventory.GetItem(0).GetType());
        Assert.AreEqual(typeof(MedKit), inventory.GetItem(1).GetType());
    }

    [Test]
    public void InventorySetAllItems_ReplacesItemsList()
    {
        List<IItem> newItems = new List<IItem>();
        Assert.AreEqual(newItems, inventory.GetAllItems());
        List<IItem> fullItems = new List<IItem>() 
        { 
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>()
        };
        inventory.SetAllItems(new List<IItem>());
        inventory.SetAllItems(fullItems);
        Assert.AreEqual(fullItems, inventory.GetAllItems());
        fullItems = new List<IItem>()
        {
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>()
        };
        inventory.SetAllItems(new List<IItem>());
        inventory.SetAllItems(fullItems);
        Assert.AreEqual(new List<IItem>(), inventory.GetAllItems());
    }

    [Test]
    public void InventorySetItem_ReplacesItem()
    {
        List<IItem> newItems = new List<IItem>()
        {
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>()
        };
        inventory.SetAllItems(newItems);
        Assert.AreEqual(newItems, inventory.GetAllItems());
        Bandage insertedItem_1 = new GameObject().AddComponent<Bandage>();
        Bandage insertedItem_2 = new GameObject().AddComponent<Bandage>();
        inventory.SetItem(insertedItem_1, 0);
        inventory.SetItem(insertedItem_2, 2);
        Assert.AreEqual(typeof(Bandage), inventory.items[0].GetType());
        Assert.AreEqual(typeof(MedKit), inventory.items[1].GetType());
    }

    [Test]
    public void InventoryAddItem_AddsItem()
    {
        inventory.items = new List<IItem>()
        {
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>()
        };
        Assert.AreEqual(typeof(Key), inventory.items[0].GetType());
        Assert.AreEqual(typeof(MedKit), inventory.items[1].GetType());
        inventory.AddItem(new GameObject().AddComponent<Bandage>(), out bool success_1);
        Assert.AreEqual(true, success_1);
        Assert.AreEqual(typeof(Bandage), inventory.items[2].GetType());

        inventory.items = new List<IItem>()
        {
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>()
        };
        inventory.AddItem(new GameObject().AddComponent<Bandage>(), out bool success_2);
        Assert.AreEqual(false, success_2);
    }

    [Test]
    public void InventoryRemoveItem_RemovesItem()
    {
        inventory.items = new List<IItem>()
        {
            new GameObject().AddComponent<Key>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Bandage>()
        };
        inventory.RemoveItem(1);
        Assert.AreEqual(2, inventory.items.Count);
        inventory.RemoveItem(inventory.items[1]);
        Assert.AreEqual(1, inventory.items.Count);
    }

    [Test]
    public void PlayerInventoryAdd_AddsItems()
    {
        playerInventory.Add(new GameObject().AddComponent<Bandage>(), out bool success);
        Assert.AreEqual(true, success);
        Assert.AreEqual(typeof(Bandage), inventory.items[0].GetType());
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]