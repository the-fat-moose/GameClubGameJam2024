using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bestiary : MonoBehaviour
{
    public List<CreatureData> creaturesCaught = new List<CreatureData>();
    private static int index = 0;
    private static Bestiary instance = null;
    // called zero
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != this)
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

        BestiaryMenu BMenu = GameObject.FindFirstObjectByType<BestiaryMenu>().GetComponent<BestiaryMenu>();

        BMenu.creaturesCaughtInfo.Clear();

        foreach(CreatureData creature in creaturesCaught)
        {
            BMenu.creaturesCaughtInfo.Add(creature);
        }
    }
}
