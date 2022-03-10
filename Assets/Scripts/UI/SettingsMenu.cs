using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    GameObject toggle;

    void Start()
    {
        toggle = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3)) 
        {
            if (toggle.activeSelf)
            {
                toggle.SetActive(false);
                FindObjectOfType<MouseLook>().SetCursorMode(true);
                FindObjectOfType<MouseLook>().SetIsInMenu(false);
                FindObjectOfType<PlayerMovement>().SetIsInMenu(false);
            }
            else 
            {
                toggle.SetActive(true);
                FindObjectOfType<MouseLook>().SetCursorMode(false);
                FindObjectOfType<MouseLook>().SetIsInMenu(true);
                FindObjectOfType<PlayerMovement>().SetIsInMenu(true);
            }
        }
    }

    public void SetVolume(float volume) 
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
