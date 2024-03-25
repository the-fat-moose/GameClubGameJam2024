using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bestiary : MonoBehaviour
{
    public List<GameObject> creaturesCaught = new List<GameObject>();

    // called zero
    private void Awake()
    {
        DontDestroyOnLoad(transform.root);
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
            LoadBestiaryData();
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

    private void LoadBestiaryData()
    {
        // Find the game object with the BestiaryMenu.cs script
        // Load data from the bestiary.cs script to the BestiaryMenu.cs script
    }
}
