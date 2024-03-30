using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CageTarget : MonoBehaviour
{
    public float maxHealth { get; private set; } = 100f;
    public float currentHealth { get; private set; }
    public float healthPercentage { get; private set; } = 1.0f;
    private float damageReductionPercentage = 0.75f;
    public bool hasDamageReduction { get; set; } = false;
    public bool canHeal { get; private set; } = false;
    public bool isDead { get; private set; } = false;

    CageController controller;
    PlayerUIManager playerUIManager;
    private GameObject player;

    [Header("Audio Parameters")]
    [SerializeField] private AudioClip cageDamagedSound;
    private AudioSource cageAudioSource;
    private OptionsManager optionsManager;

    private void Awake()
    {
        controller = GetComponent<CageController>();
        cageAudioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        SetHealthPercentage();
    }

    private void Start()
    {
        optionsManager = GameObject.FindFirstObjectByType<OptionsManager>();
    }

    public void SetPlayerObject(GameObject _player)
    {
        player = _player;

        if (player != null) 
        {
            playerUIManager = player.GetComponentInChildren<PlayerUIManager>();
            if (playerUIManager != null) { playerUIManager.GetCageHealthPercentage(healthPercentage, currentHealth, maxHealth); }
        }
    }

    public void TakeDamage(float amount)
    {
        if (hasDamageReduction) { currentHealth -= amount * damageReductionPercentage; }
        else { currentHealth -= amount; }

        if (cageAudioSource != null && optionsManager != null) { cageAudioSource.PlayOneShot(cageDamagedSound, 1f * (optionsManager.sfxVolume * optionsManager.masterVolume)); }

        SetHealthPercentage();
        if (playerUIManager != null) { playerUIManager.GetCageHealthPercentage(healthPercentage, currentHealth, maxHealth); }
        canHeal = true;

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        if (playerUIManager != null) { playerUIManager.GetCageHealthPercentage(healthPercentage, currentHealth, maxHealth); }

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            canHeal = false;
        }
        SetHealthPercentage();
    }

    private void SetHealthPercentage()
    {
        healthPercentage = currentHealth / maxHealth;
    }

    private void GameOver()
    {
        isDead = true;
        Time.timeScale = 0;
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("HubWorldScene");
    }
}
