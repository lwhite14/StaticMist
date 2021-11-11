using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsTab : MonoBehaviour
{
    public GameObject controlsTab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (controlsTab.activeSelf)
            {
                controlsTab.SetActive(false);
            }
            else 
            { 
                controlsTab.SetActive(true);
            }
        }
    }

}
