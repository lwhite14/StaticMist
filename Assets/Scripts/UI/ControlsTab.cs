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

    public void InstructionsInput()
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

    public void SensitivityDown()
    {
        sens--;
        if (sens < 0)
        {
            sens = 0;
        }
        sensSlider.value = sens;
        mouseLook.SetMouseSensitivity((sens + 1) * 5);
    }

    public void SensitivityUp() 
    {
        sens++;
        if (sens > 5) 
        {
            sens = 5;
        }
        sensSlider.value = sens;
        mouseLook.SetMouseSensitivity((sens + 1) * 5);
    }
}
