using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class EnvironmentEditTests
{
    PlayerInventory playerInventory;
    Gate gate;
    ElectricalBox electricalBox;

    [SetUp]
    public void SetUp()
    {
        GameObject playerInventoryObj = new GameObject();
        playerInventory = playerInventoryObj.AddComponent<PlayerInventory>();

        GameObject gateObj = new GameObject();
        gate = gateObj.AddComponent<Gate>();
        gate.unlockedSound = new GameObject();
        gate.lockedPopUp = new GameObject();
        gate.openedPopUp = new GameObject();
        GameObject gateChild = new GameObject();
        gateChild.transform.parent = gateObj.transform;

        GameObject electricalBoxObj = new GameObject();
        electricalBox = electricalBoxObj.AddComponent<ElectricalBox>();
        electricalBox.unlockedSound = new GameObject();
        electricalBox.powerUpSound = new GameObject();
        electricalBox.lockedPopUp = new GameObject();
        electricalBox.openedPopUp = new GameObject();
        electricalBox.gate = new GameObject().AddComponent<ElectricalGate>();
        electricalBox.gate.lockedPopUp = new GameObject();
        electricalBox.gate.openedPopUp = new GameObject();
        electricalBoxObj.AddComponent<DialogueTrigger>();
        GameObject electricalBoxChild = new GameObject();
        electricalBoxChild.transform.parent = electricalBoxObj.transform;
    }

    [Test]
    public void Gate_Unlocked_Opens() 
    {
        Assert.AreEqual(false, gate.isOpen);
        gate.Interact();
        Assert.AreEqual(true, gate.isOpen);
    }

    [Test]
    public void Gate_CheckIfKey()
    {
        gate.isLocked = true;

        Key key_1 = new GameObject().AddComponent<Key>();
        key_1.code = "DOES NOT WORK";
        Key key_2 = new GameObject().AddComponent<Key>();
        key_2.code = "WORKS";
        gate.unlockCode = "WORKS";

        playerInventory.inventory.AddItem(key_1, out bool success1);
        playerInventory.inventory.AddItem(key_2, out bool success2);

        List<IItem> items = playerInventory.inventory.GetAllItems();
        Assert.AreEqual(2, items.Count);

        Assert.AreEqual(true, gate.isLocked);
        gate.CheckIfKey(key_1);
        Assert.AreEqual(true, gate.isLocked);
        gate.CheckIfKey(key_2);
        Assert.AreEqual(false, gate.isLocked);

        items = playerInventory.inventory.GetAllItems();
        Assert.AreEqual(1, items.Count);
    }

    [Test]
    public void ElectricalBox_CheckIfKey()
    {
        electricalBox.gate.isLocked = true;

        Key key_1 = new GameObject().AddComponent<Key>();
        key_1.code = "DOES NOT WORK";
        Key key_2 = new GameObject().AddComponent<Key>();
        key_2.code = "WORKS 1";
        Key key_3 = new GameObject().AddComponent<Key>();
        key_3.code = "WORKS 2";
        electricalBox.lockOneCode = "WORKS 1";
        electricalBox.lockTwoCode = "WORKS 2";

        playerInventory.inventory.AddItem(key_1, out bool success1);
        playerInventory.inventory.AddItem(key_2, out bool success2);
        playerInventory.inventory.AddItem(key_3, out bool success3);

        List<IItem> items = playerInventory.inventory.GetAllItems();
        Assert.AreEqual(3, items.Count);


        Assert.AreEqual(true, electricalBox.gate.isLocked);
        electricalBox.CheckIfKey(key_1);
        electricalBox.Interact();
        Assert.AreEqual(true, electricalBox.gate.isLocked);
        electricalBox.CheckIfKey(key_2);
        electricalBox.Interact();
        Assert.AreEqual(true, electricalBox.gate.isLocked);
        electricalBox.CheckIfKey(key_3);
        electricalBox.Interact();
        Assert.AreEqual(false, electricalBox.gate.isLocked);

        items = playerInventory.inventory.GetAllItems();
        Assert.AreEqual(1, items.Count);
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]