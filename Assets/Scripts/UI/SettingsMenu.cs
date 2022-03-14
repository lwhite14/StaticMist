using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static bool paused = false;

    [Header("Rebinding Objects")]
    public GameObject bindingsMenu;
    public RebindUI[] rebindings;

    [Header("Other")]
    public GameObject pauseSound;
    public GameObject unpauseSound;
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Slider brightnessSlider;
    public Slider sensitivitySlider;
    public Toggle fullscreenToggle;
    public Toggle tvEffectToggle;

    float sensitivty = 5.0f;
    float currentVolume = 0.0f;
    float currentBrightness = 0.0f;
    bool isTVEffect = true;
    Resolution[] resolutions;
    GameObject toggle;

    void Start()
    {
        toggle = transform.GetChild(0).gameObject;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentRefreshRate = Screen.currentResolution.refreshRate;
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == currentRefreshRate).ToArray();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }        
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }

    public void SettingsInput() 
    {
        if (paused)
        {
            Resume();
        }
        else 
        {
            Pause();
        }
    }

    void Resume() 
    {
        bindingsMenu.SetActive(false);
        toggle.SetActive(false);
        Time.timeScale = 1.0f;
        paused = false;
        if (GameManager.instance.level != 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            FindObjectOfType<InventoryUI>().SetCanUse(true);
        }
        MusicManager.instance.Unpause();
        Instantiate(unpauseSound, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void Pause() 
    {
        toggle.SetActive(true);
        if (GameManager.instance.level != 0)
        {
            Time.timeScale = 0.0f;
            FindObjectOfType<InventoryUI>().SetCanUse(false);
        }
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        MusicManager.instance.Pause();
        Instantiate(pauseSound, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetBrightness(float brightness) 
    {
        currentBrightness = brightness;
        GameObject.Find("Directional Light").GetComponent<Light>().intensity = brightness;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetSensitivty(float newSens) 
    {
        sensitivty = newSens;
        if (FindObjectOfType<MouseLook>() != null)
        {
            FindObjectOfType<MouseLook>().SetMouseSensitivity((sensitivty + 1) * 5);
        }
    }

    public void SetTVEffect(bool newIsTVEffect) 
    {
        isTVEffect = newIsTVEffect;
        FindObjectOfType<PSX>().TurnOnTVUI(isTVEffect);
    }

    public void BindingsMenu() 
    {
        if (bindingsMenu.activeSelf)
        {
            bindingsMenu.SetActive(false);
        }
        else
        {
            bindingsMenu.SetActive(true);
        }
    }

    public void ExitGame()
    {
        GameManager.instance.ExitGame();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference", currentVolume);
        PlayerPrefs.SetFloat("BrightnessPreference", currentBrightness);
        PlayerPrefs.SetFloat("SensitivityPreference", sensitivty);
        PlayerPrefs.SetInt("TVEffectPreference", Convert.ToInt32(isTVEffect));
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("ResolutionPreference"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        }
        else
        {
            resolutionDropdown.value = currentResolutionIndex;
        }
        if (PlayerPrefs.HasKey("FullscreenPreference"))
        {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
            fullscreenToggle.isOn = Screen.fullScreen;
        }
        else
        {
            Screen.fullScreen = true;
            fullscreenToggle.isOn = Screen.fullScreen;
        }
        if (PlayerPrefs.HasKey("TVEffectPreference"))
        {
            isTVEffect = Convert.ToBoolean(PlayerPrefs.GetInt("TVEffectPreference"));
            FindObjectOfType<PSX>().TurnOnTVUI(isTVEffect);
            tvEffectToggle.isOn = isTVEffect;
        }
        else 
        {
            isTVEffect = true;
            FindObjectOfType<PSX>().TurnOnTVUI(isTVEffect);
            tvEffectToggle.isOn = isTVEffect;
        }

        volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
        brightnessSlider.value = PlayerPrefs.GetFloat("BrightnessPreference");   
        sensitivitySlider.value = PlayerPrefs.GetFloat("SensitivityPreference");

        foreach (RebindUI rebinding in rebindings)
        {
            if (rebinding.GetInputActionReference() != null) 
            { 
                rebinding.GetBindingInfo();
                InputManager.LoadBindingOverride(rebinding.GetActionName());
                rebinding.UpdateUI();
            }
        }
    }
}
