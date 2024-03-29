using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [Header("Sound Parameters")]
    [SerializeField] private AudioClip hubWorldMusic;
    [SerializeField] private AudioClip ForestMusic;
    [SerializeField] private AudioClip SandyMusic;
    [SerializeField] private AudioClip RockyMusic;
    private AudioSource audioSource;
    private OptionsManager optionsManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        optionsManager = GameObject.FindFirstObjectByType<OptionsManager>();

        if (optionsManager != null && audioSource != null) 
        {
            audioSource.loop = true;
            if (currentScene.name == "HubWorldScene") { audioSource.clip = hubWorldMusic; }
            else if (currentScene.name == "Rocky-1") { audioSource.clip = RockyMusic; }
            else if (currentScene.name == "Forest-1") { audioSource.clip = ForestMusic;  }
            else if (currentScene.name == "Sandy-1") { audioSource.clip = SandyMusic; }
            audioSource.volume = 1f * (optionsManager.masterVolume * optionsManager.musicVolume);
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (optionsManager != null && audioSource != null) { audioSource.volume = 1f * (optionsManager.masterVolume * optionsManager.musicVolume); }
    }
}
