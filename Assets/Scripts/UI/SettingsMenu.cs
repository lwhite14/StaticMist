using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public static bool paused { get; set; } = false;

    [Header("Rebinding Objects")]
    public GameObject keyboardBindingsMenuToggle;
    public GameObject controllerBindingsMenuToggle;
    public RebindUI[] rebindings;

    [Header("Selected UI Elements")]
    public GameObject settingsResumeGame;
    public GameObject keyboardBindingsBack;
    public GameObject controllerBindingsBack;

    [Header("Other")]
    public GameObject fullToggle;
    public GameObject settingsMenuToggle;
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

    void Start()
    {
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
        fullToggle.SetActive(false);
        keyboardBindingsMenuToggle.SetActive(false);
        controllerBindingsMenuToggle.SetActive(false);
        settingsMenuToggle.SetActive(false);
        paused = false;

        EventSystem.current.SetSelectedGameObject(null);

        if (GameManager.instance.level != 0)
        {
            Time.timeScale = 1.0f;
            //Cursor.lockState = CursorLockMode.Locked;
            //FindObjectOfType<InventoryUI>().SetCanUse(true);
            InventoryUI.canUse = true;
            MusicManager.instance.Unpause();
            Instantiate(unpauseSound, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else 
        {
            EventSystem.current.SetSelectedGameObject(GameObject.Find("SettingsButton"));
        }

    }

    void Pause() 
    {
        fullToggle.SetActive(true);
        keyboardBindingsMenuToggle.SetActive(false);
        controllerBindingsMenuToggle.SetActive(false);
        settingsMenuToggle.SetActive(true);
        paused = true;

        if (GameManager.instance.level != 0)
        {
            Time.timeScale = 0.0f;
            //Cursor.lockState = CursorLockMode.None;
            //FindObjectOfType<InventoryUI>().SetCanUse(false);
            InventoryUI.canUse = false;
            MusicManager.instance.Pause();
            Instantiate(pauseSound, new Vector3(0, 0, 0), Quaternion.identity);
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsResumeGame);
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

    public void KeyboardBindingsMenu() 
    {
        if (keyboardBindingsMenuToggle.activeSelf)
        {
            keyboardBindingsMenuToggle.SetActive(false);
            settingsMenuToggle.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(settingsResumeGame);
        }
        else
        {
            keyboardBindingsMenuToggle.SetActive(true);
            settingsMenuToggle.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(keyboardBindingsBack);
        }
    }

    public void ControllerBindingsMenu()
    {
        if (controllerBindingsMenuToggle.activeSelf)
        {
            controllerBindingsMenuToggle.SetActive(false);
            settingsMenuToggle.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(settingsResumeGame);
        }
        else
        {
            controllerBindingsMenuToggle.SetActive(true);
            settingsMenuToggle.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(controllerBindingsBack);
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
