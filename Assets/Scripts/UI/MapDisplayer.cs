using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapDisplayer : MonoBehaviour
{
    public Image mapImage;
    public GameObject tab;

    public void ViewMap(Sprite map)
    {
        mapImage.sprite = map;
        tab.SetActive(true);

        if (Application.isPlaying)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("ExitButton"));
        }
    }

    public void Exit() 
    {
        tab.SetActive(false);

        if (Application.isPlaying)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("ItemSpot1").transform.GetChild(0).gameObject);
        }
    }
}
