using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        masterVolume = masterVolumeSlider.value;
        sfxVolume = sfxVolumeSlider.value;
        musicVolume = musicVolumeSlider.value;
        mouseSensitivityX = mouseSensitivityXSlider.value;
        mouseSensitivityY = mouseSensitivityYSlider.value;
        OnFullScreenCheckBoxUpdate(fullscreenCheckbox.isOn);
    }

    public void OnMasterVolumeSliderUpdate()
    {
        masterVolume = masterVolumeSlider.value;
    }

    public void OnSFXVolumeSliderUpdate()
    {
        sfxVolume = sfxVolumeSlider.value;
    }

    public void OnMusicVolumeSliderUpdate()
    {
        musicVolume = musicVolumeSlider.value;
    }

    public void OnMouseSensitivityXSliderUpdate() 
    { 
        mouseSensitivityX = mouseSensitivityXSlider.value;
    }

    public void OnMouseSensitivityYSliderUpdate()
    {
        mouseSensitivityY = mouseSensitivityYSlider.value;
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