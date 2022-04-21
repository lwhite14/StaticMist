using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class InteractionEditTests
{
    [Test]
    public void IInteractable_Interacted_TriggersLogic()
    {
        InteractableObject interactableObject = (new GameObject()).AddComponent<InteractableObject>();
        IInteractable interactable = interactableObject;
        Assert.AreEqual(interactableObject.hasInteracted, false);
        interactable.Interact();
        Assert.AreEqual(interactableObject.hasInteracted, true);
    }

    [Test]
    public void PassingItemToInventory_NoItems_AddsItemToInventoryList()
    {
        InteractableMedKit interactableMedKit = new GameObject().AddComponent<InteractableMedKit>();
        interactableMedKit.item = new GameObject().AddComponent<MedKit>();
        interactableMedKit.pickUpSound = new GameObject();
        PlayerInventory playerInventory = new GameObject().AddComponent<PlayerInventory>();
        int preCount = playerInventory.inventory.GetAllItems().Count;
        Assert.AreEqual(0, preCount);
        interactableMedKit.Interact();
        int postCount = playerInventory.inventory.GetAllItems().Count;
        Assert.AreEqual(1, postCount);
        List<IItem> items = playerInventory.inventory.GetAllItems();
        Assert.AreEqual(typeof(MedKit), items[0].GetType());
    }

    [Test]
    public void InteractableKey_Interacted_PassesStringToKey()
    {
        InteractableKey interactableKey = new GameObject().AddComponent<InteractableKey>();
        interactableKey.key = new GameObject().AddComponent<Key>();
        interactableKey.pickUpSound = new GameObject();
        IInteractable interactable = interactableKey;
        PlayerInventory playerInventory = new GameObject().AddComponent<PlayerInventory>();
        Assert.AreEqual("", interactableKey.code);
        interactableKey.code = "TEST CODE";
        Assert.AreEqual("TEST CODE", interactableKey.code);
        interactable.Interact();
        List<IItem> items = playerInventory.inventory.GetAllItems();
        Assert.AreEqual(typeof(Key), items[0].GetType());
        Key firstItem = items[0] as Key;
        Assert.AreEqual("TEST CODE", firstItem.code);
    }

    [Test]
    public void InteractableMap_Interacted_PassesSpriteToKey()
    {
        InteractableMap interactableMap = new GameObject().AddComponent<InteractableMap>();
        interactableMap.map = new GameObject().AddComponent<Map>();
        interactableMap.pickUpSound = new GameObject();
        IInteractable interactable = interactableMap;
        PlayerInventory playerInventory = new GameObject().AddComponent<PlayerInventory>();
        Assert.AreEqual(null, interactableMap.mapImage);
        Texture2D tex = new Texture2D(256, 256);
        interactableMap.mapImage = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        Assert.AreEqual(interactableMap.mapImage.texture.width, 256);
        Assert.AreEqual(interactableMap.mapImage.texture.height, 256);
        interactable.Interact();
        List<IItem> items = playerInventory.inventory.GetAllItems();
        Assert.AreEqual(typeof(Map), items[0].GetType());
        Map firstItem = items[0] as Map;
        Assert.AreEqual(256, firstItem.map.texture.width);
        Assert.AreEqual(256, firstItem.map.texture.width);
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]