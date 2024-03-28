using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsUIManager : MonoBehaviour
{
    [Header("UI Functions")]
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;

    [Header("Player Functions")]
    [SerializeField] Slider mouseSensitivityXSlider;
    [SerializeField] Slider mouseSensitivityYSlider;

    [Header("Fullscreen")]
    [SerializeField] Toggle fullscreenCheckbox;

    public float masterVolume { get; private set; }
    public float sfxVolume { get; private set; }
    public float musicVolume { get; private set; }

    public float mouseSensitivityX { get; private set; }
    public float mouseSensitivityY { get; private set; }

    public bool fullscreen { get; private set; } = false;

    OptionsManager optionsManager;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        optionsManager = GameObject.FindFirstObjectByType<OptionsManager>();

        masterVolume = masterVolumeSlider.value;
        sfxVolume = sfxVolumeSlider.value;
        musicVolume = musicVolumeSlider.value;
        mouseSensitivityX = mouseSensitivityXSlider.value;
        mouseSensitivityY = mouseSensitivityYSlider.value;
        OnFullScreenCheckBoxUpdate(fullscreenCheckbox.isOn);

        if (optionsManager != null && currentScene.name == "HubWorldScene") 
        {
            optionsManager.masterVolume = masterVolume;
            optionsManager.sfxVolume = sfxVolume;
            optionsManager.musicVolume = musicVolume;
            optionsManager.mouseSensX = mouseSensitivityX;
            optionsManager.mouseSensY = mouseSensitivityY;
        }
        else if (optionsManager != null && currentScene.name != "HubWorldScene")
        {
            masterVolumeSlider.value = optionsManager.masterVolume;
            sfxVolumeSlider.value = optionsManager.sfxVolume;
            musicVolumeSlider.value = optionsManager.musicVolume;
            mouseSensitivityXSlider.value = optionsManager.mouseSensX;
            mouseSensitivityYSlider.value = optionsManager.mouseSensY;
        }
    }

    public void OnMasterVolumeSliderUpdate()
    {
        masterVolume = masterVolumeSlider.value;
        if (optionsManager != null) { optionsManager.masterVolume = masterVolume; }
    }

    public void OnSFXVolumeSliderUpdate()
    {
        sfxVolume = sfxVolumeSlider.value;
        if (optionsManager != null) { optionsManager.sfxVolume = sfxVolume; }
    }

    public void OnMusicVolumeSliderUpdate()
    {
        musicVolume = musicVolumeSlider.value;
        if (optionsManager != null) { optionsManager.musicVolume = musicVolume; }
    }

    public void OnMouseSensitivityXSliderUpdate() 
    { 
        mouseSensitivityX = mouseSensitivityXSlider.value;
        if (optionsManager != null) { optionsManager.mouseSensX = mouseSensitivityX; }
    }

    public void OnMouseSensitivityYSliderUpdate()
    {
        mouseSensitivityY = mouseSensitivityYSlider.value;
        if (optionsManager != null) { optionsManager.mouseSensY = mouseSensitivityY; }
    }

    public void OnFullScreenCheckBoxUpdate(bool toggle)
    {
        fullscreen = toggle;
        Debug.Log(fullscreenCheckbox.isOn);

        if (fullscreen) 
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}