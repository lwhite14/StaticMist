using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class InventoryPlayTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Test_Inventory");
    }

    [UnityTest]
    public IEnumerator Bandage_Use()
    {
        Bandage bandage = new GameObject().AddComponent<Bandage>();
        bandage.healSound = new GameObject();
        GameObject player = GameObject.Find("Player");
        Health health = player.GetComponent<Health>();
        Assert.AreEqual(4, health.health);
        health.TakeDamage(1, "WORLD");
        Assert.AreEqual(3, health.health);
        bandage.Use();
        Assert.AreEqual(4, health.health);
        yield return null;
    }

    [UnityTest]
    public IEnumerator MedKit_Use()
    {
        MedKit medKit = new GameObject().AddComponent<MedKit>();
        medKit.healSound = new GameObject();
        GameObject player = GameObject.Find("Player");
        Health health = player.GetComponent<Health>();
        Assert.AreEqual(4, health.health);
        health.TakeDamage(3, "WORLD");
        Assert.AreEqual(1, health.health);
        medKit.Use();
        Assert.AreEqual(3, health.health);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Bat_Equip()
    {
        InteractableBat interactableBat = GameObject.Find("BatWorld").GetComponent<InteractableBat>();
        interactableBat.Interact();
        Bat bat = GameObject.Find("BatInventory(Clone)").GetComponent<Bat>();
        Assert.That(GameObject.FindWithTag("Viewmodel") == null);
        bat.Equip();
        Assert.That(GameObject.FindWithTag("Viewmodel") != null);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Candle_Equip()
    {
        InteractableCandle interactableCandle = GameObject.Find("CandleWorld").GetComponent<InteractableCandle>();
        interactableCandle.Interact();
        Candle candle = GameObject.Find("CandleInventory(Clone)").GetComponent<Candle>();
        Assert.That(GameObject.FindWithTag("Viewmodel") == null);
        candle.Equip();
        Assert.That(GameObject.FindWithTag("Viewmodel") != null);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Flashlight_Equip()
    {
        InteractableFlashlight interactableFlashlight = GameObject.Find("FlashlightWorld").GetComponent<InteractableFlashlight>();
        interactableFlashlight.Interact();
        Flashlight flashlight = GameObject.Find("FlashlightInventory(Clone)").GetComponent<Flashlight>();
        Assert.That(GameObject.FindWithTag("Viewmodel") == null);
        flashlight.Equip();
        Assert.That(GameObject.FindWithTag("Viewmodel") != null);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Map_Use()
    {
        InteractableMap interactableMap = GameObject.Find("MapWorld").GetComponent<InteractableMap>();
        interactableMap.Interact();
        Map map = GameObject.FindObjectOfType<Map>();
        Assert.AreEqual(false, GameObject.FindObjectOfType<MapDisplayer>().tab.activeSelf);
        map.Use();
        Assert.AreEqual(true, GameObject.FindObjectOfType<MapDisplayer>().tab.activeSelf);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerInventory_RefreshUI()
    {
        PlayerInventory playerInventory = GameObject.FindObjectOfType<PlayerInventory>();
        playerInventory.inventory.items = new List<IItem>()
        {
            new GameObject().AddComponent<Bandage>(),
            new GameObject().AddComponent<MedKit>(),
            new GameObject().AddComponent<Bandage>()
        };
        Assert.IsNull(GameObject.Find("ItemSpot1").GetComponent<ItemSlot>().currentItem);
        Assert.IsNull(GameObject.Find("ItemSpot2").GetComponent<ItemSlot>().currentItem);
        Assert.IsNull(GameObject.Find("ItemSpot3").GetComponent<ItemSlot>().currentItem);
        GameObject.FindObjectOfType<InventoryUI>().RefreshUI(playerInventory.inventory.items);
        Assert.IsNotNull(GameObject.Find("ItemSpot1").GetComponent<ItemSlot>().currentItem.GetComponent<Bandage>());
        Assert.IsNotNull(GameObject.Find("ItemSpot2").GetComponent<ItemSlot>().currentItem.GetComponent<MedKit>());
        Assert.IsNotNull(GameObject.Find("ItemSpot3").GetComponent<ItemSlot>().currentItem.GetComponent<Bandage>());
        Assert.AreEqual(typeof(Bandage), GameObject.Find("ItemSpot1").GetComponent<ItemSlot>().currentItem.GetComponent<IItem>().GetType());
        Assert.AreEqual(typeof(MedKit), GameObject.Find("ItemSpot2").GetComponent<ItemSlot>().currentItem.GetComponent<IItem>().GetType());
        Assert.AreEqual(typeof(Bandage), GameObject.Find("ItemSpot3").GetComponent<ItemSlot>().currentItem.GetComponent<IItem>().GetType());
        yield return null;
    }

    [UnityTest]
    public IEnumerator CoroutineHelper()
    {
        CoroutineHelper coroutineHelper = GameObject.FindObjectOfType<CoroutineHelper>();
        InteractableMap interactableMap = GameObject.Find("MapWorld").GetComponent<InteractableMap>();
        interactableMap.Interact();
        Assert.AreEqual(false, coroutineHelper.runningRoutine);
        Map map = GameObject.FindObjectOfType<Map>();
        map.Examine();
        yield return new WaitForEndOfFrame();
        Assert.AreEqual(true, coroutineHelper.runningRoutine);
        yield return null;
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]
