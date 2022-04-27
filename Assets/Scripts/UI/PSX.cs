using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSX : MonoBehaviour
{
    public RenderTexture targetTexture;

    public void TurnOnTVUI(bool isOn)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(isOn);
        if (isOn)
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().targetTexture = targetTexture;
            GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            GameObject.Find("Canvas").GetComponent<Canvas>().planeDistance = 1.25f;
            GameObject.Find("Canvas").GetComponent<Canvas>().worldCamera = GameObject.Find("PSXCamera").GetComponent<Camera>();
            GameObject.Find("PostProcessingVolume").layer = 8;
        }
        else 
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().targetTexture = null;
            GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            GameObject.Find("Canvas").GetComponent<Canvas>().planeDistance = 0.05f;          
            GameObject.Find("Canvas").GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            GameObject.Find("PostProcessingVolume").layer = 7;      
        }
    }
}
