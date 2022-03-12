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
        }
        else 
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().targetTexture = null;
        }
    }
}
