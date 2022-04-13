using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class UIEditTests
{

    [Test]
    public void OnSelectUIPlaysSound()
    {
        OnSelectUI onSelectUI = new GameObject().AddComponent<OnSelectUI>();
        onSelectUI.selectedSound = new GameObject();
        Assert.AreEqual(0, OnSelectUI.counter);
        onSelectUI.OnSelect(new BaseEventData(EventSystem.current));
        Assert.AreEqual(1, OnSelectUI.counter);
    }

    [Test]
    public void MapDisplayerViewMap()
    {
        MapDisplayer mapDisplayer = new GameObject().AddComponent<MapDisplayer>();
        mapDisplayer.mapImage = new GameObject().AddComponent<Image>();
        mapDisplayer.mapImage.gameObject.name = "Test Map Image";
        mapDisplayer.tab = new GameObject();
        mapDisplayer.tab.SetActive(false);
        Assert.That(mapDisplayer.mapImage.sprite == null);
        Assert.That(mapDisplayer.tab.activeSelf == false);
        Texture2D tex = new Texture2D(256, 256);
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.0f, 0.0f));
        mapDisplayer.ViewMap(sprite);
        Assert.That(mapDisplayer.mapImage.sprite == sprite);
        Assert.That(mapDisplayer.tab.activeSelf == true);
        mapDisplayer.Exit();
        Assert.That(mapDisplayer.tab.activeSelf == false);
    }
}

// The basic naming of a test comprises of three main parts:
// [UnitOfWork_StateUnderTest_ExpectedBehavior]