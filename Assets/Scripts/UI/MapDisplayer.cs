using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDisplayer : MonoBehaviour
{
    public Image mapImage;
    public GameObject tab;

    public void ViewMap(Sprite map)
    {
        mapImage.sprite = map;
        tab.SetActive(true);
    }

    public void Exit() 
    {
        tab.SetActive(false);
    }
}
