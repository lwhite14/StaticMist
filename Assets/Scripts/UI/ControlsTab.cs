using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsTab : MonoBehaviour
{
    public GameObject controlsTab;
    public Slider sensSlider;
    public float sens = 5;
    public MouseLook mouseLook;

    void Start()
    {
        sensSlider.value = sens;
    }

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

        if (Input.GetKeyDown(KeyCode.LeftBracket)) 
        {
            SensitivityDown();
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            SensitivityUp();
        }
    }

    void SensitivityDown()
    {
        sens--;
        if (sens < 0)
        {
            sens = 0;
        }
        sensSlider.value = sens;
        mouseLook.SetMouseSensitivity((sens + 1) * 50);
    }

    void SensitivityUp() 
    {
        sens++;
        if (sens > 5) 
        {
            sens = 5;
        }
        sensSlider.value = sens;
        mouseLook.SetMouseSensitivity((sens + 1) * 50);
    }
}
