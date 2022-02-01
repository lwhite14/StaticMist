using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsTab : MonoBehaviour
{
    public GameObject controlsTab;
    public Slider sensSlider;
    public float sens = 5;

    MouseLook mouseLook;
    Animator anim;
    bool isOn = true;

    void Start()
    {
        sensSlider.value = sens;
        mouseLook = FindObjectOfType<MouseLook>();
        anim = GetComponent<Animator>();
    }

    public void InstructionsInput()
    {
        if (isOn)
        {
            SetOn(false);
        }
        else
        {
            SetOn(true);
        }
    }

    public void SetOn(bool status) 
    {
        //controlsTab.SetActive(status);
        isOn = status;
        anim.SetBool("isOn", status);
        StateManager.Instructions = status;
    }

    public void SensitivityDown()
    {
        sens--;
        if (sens < 0)
        {
            sens = 0;
        }
        StateManager.Sensitivity = sens;
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
        StateManager.Sensitivity = sens;
        sensSlider.value = sens;
        mouseLook.SetMouseSensitivity((sens + 1) * 5);
    }

    public void SetSens(float newSens) 
    {
        sens = newSens;
        StateManager.Sensitivity = sens;
        sensSlider.value = sens;
        mouseLook.SetMouseSensitivity((sens + 1) * 5);
    }
}
