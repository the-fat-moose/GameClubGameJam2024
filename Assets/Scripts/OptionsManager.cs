using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    private static OptionsManager instance;

    Scene currentScene;

    private GameObject optionsUIManager;
    public float masterVolume { get; set; }
    public float sfxVolume { get; set; }
    public float musicVolume { get; set; }
    public float mouseSensX { get; set; }
    public float mouseSensY { get; set; }

    public int timesVisitedMainMenu { get; private set; } = 0;

    // called zero
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

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
        if (scene != null) { currentScene = scene; }

        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log("mode: " + mode);

        if (scene.name == "HubWorldScene")
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            timesVisitedMainMenu += 1;
        }

        optionsUIManager = GameObject.FindFirstObjectByType<OptionsUIManager>().transform.gameObject;
    }

    // called third
    private void Start()
    {
        Debug.Log("Start");
    }

    // called when game is terminated
    private void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
