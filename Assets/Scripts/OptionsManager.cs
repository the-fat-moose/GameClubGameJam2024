using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
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
