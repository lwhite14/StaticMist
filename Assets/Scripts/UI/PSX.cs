using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSX : MonoBehaviour
{
    public RenderTexture targetTexture;
    bool isOn = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2)) 
        {
            if (isOn)
            {
                TurnOnTVUI(false);
            }
            else
            {
                TurnOnTVUI(true);
            }
            
        }
    }

    public void TurnOnTVUI(bool isOn)
    {
        this.isOn = isOn;
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
