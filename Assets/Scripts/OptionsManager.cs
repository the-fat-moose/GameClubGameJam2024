using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    private GameObject optionsUIManager;
    public float masterVolume { get; private set; }
    public float sfxVolume { get; private set; }
    public float musicVolume { get; private set; }
    public float mouseSensX { get; private set; }
    public float mouseSensY { get; private set; }

    // called zero
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("Awake");
    }

    // called first
    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log("mode: " + mode);

        if (scene.name == "HubWorldScene")
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        optionsUIManager = GameObject.FindFirstObjectByType<OptionsUIManager>().transform.gameObject;
    }

    // called third
    private void Start()
    {
        Debug.Log("Start");
    }

    // called fourth
    private void Update()
    {
        if (optionsUIManager != null)
        {
            masterVolume = optionsUIManager.GetComponent<OptionsUIManager>().masterVolume;
            sfxVolume = optionsUIManager.GetComponent<OptionsUIManager>().sfxVolume;
            musicVolume = optionsUIManager.GetComponent<OptionsUIManager>().musicVolume;
            mouseSensX = optionsUIManager.GetComponent<OptionsUIManager>().mouseSensitivityX;
            mouseSensY = optionsUIManager.GetComponent<OptionsUIManager>().mouseSensitivityY;
        }
    }

    // called when game is terminated
    private void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
