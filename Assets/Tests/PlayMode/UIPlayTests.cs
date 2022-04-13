using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class UIPlayTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Test_WithPlayer");
    }

    [UnityTest]
    public IEnumerator Pause()
    {
        SettingsMenu settingsMenu = GameObject.FindObjectOfType<SettingsMenu>();
        Assert.AreEqual(1.0f, Time.timeScale);
        settingsMenu.SettingsInput();
        Assert.AreEqual(0.0f, Time.timeScale);
        settingsMenu.SettingsInput();
        Assert.AreEqual(1.0f, Time.timeScale);
        yield return null;
    }


    [UnityTest]
    public IEnumerator SettingsMenuSetVolume()
    {
        SettingsMenu settingsMenu = GameObject.FindObjectOfType<SettingsMenu>();
        settingsMenu.SetVolume(0.7f);
        Assert.That(settingsMenu.currentVolume == 0.7f);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SettingsMenuSetBrightness()
    {
        SettingsMenu settingsMenu = GameObject.FindObjectOfType<SettingsMenu>();
        Assert.That(settingsMenu.currentBrightness == 0.0f);
        settingsMenu.SetBrightness(0.7f);
        Assert.That(settingsMenu.currentBrightness == 0.7f);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SettingsMenuSetFullscreen()
    {
        SettingsMenu settingsMenu = GameObject.FindObjectOfType<SettingsMenu>();
        Assert.That(settingsMenu.isFullscreen == false);
        settingsMenu.SetFullscreen(true);
        Assert.That(settingsMenu.isFullscreen == true);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SettingsMenuSetSensitivity()
    {
        SettingsMenu settingsMenu = GameObject.FindObjectOfType<SettingsMenu>();
        settingsMenu.SetSensitivty(3.0f);
        Assert.That(settingsMenu.sensitivty == 3.0f);
        settingsMenu.SetSensitivty(0.34f);
        Assert.That(settingsMenu.sensitivty == 0.34f);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SettingsMenuSetTVEffect()
    {
        SettingsMenu settingsMenu = GameObject.FindObjectOfType<SettingsMenu>();
        settingsMenu.SetTVEffect(false);
        Assert.That(settingsMenu.isTVEffect == false);
        settingsMenu.SetTVEffect(true);
        Assert.That(settingsMenu.isTVEffect == true);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ItemSlotRefresh()
    {
        ItemSlot itemSlot = new GameObject().AddComponent<ItemSlot>();
        Assert.That(itemSlot.currentItem == null);
        itemSlot.currentItem = new GameObject("Item_1");
        itemSlot.currentItem.AddComponent<Key>();
        GameObject childObj = new GameObject();
        childObj.transform.parent = itemSlot.gameObject.transform;
        Assert.That(itemSlot.transform.GetChild(0).childCount == 0);
        itemSlot.Refresh();
        Assert.That(itemSlot.transform.GetChild(0).childCount == 1);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ItemSlotSelect()
    {
        ItemSlot itemSlot = new GameObject().AddComponent<ItemSlot>();
        yield return new WaitForEndOfFrame();
        Assert.That(itemSlot.currentItem == null);
        itemSlot.Select();
        Assert.That(GameObject.FindObjectOfType<InventoryUI>().viewedItem == null);
        itemSlot.currentItem = new GameObject("Item_1");
        itemSlot.currentItem.AddComponent<Key>();
        itemSlot.Select();
        Assert.That(GameObject.FindObjectOfType<InventoryUI>().viewedItem != null);
        Assert.That(GameObject.FindObjectOfType<InventoryUI>().viewedItem.name == "Item_1");
        yield return null;
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]
