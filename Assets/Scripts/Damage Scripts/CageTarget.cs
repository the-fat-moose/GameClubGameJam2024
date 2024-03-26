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

    private void Start()
    {
        controller = GetComponent<CageController>();
        currentHealth = maxHealth;
        SetHealthPercentage();
    }

    public void SetPlayerObject(GameObject _player)
    {
        player = _player;

        if (player != null) 
        { 
            playerUIManager = player.GetComponentInChildren<PlayerUIManager>();
            if (playerUIManager != null) { playerUIManager.GetCageHealthPercentage(healthPercentage); }
        }
    }

    public void TakeDamage(float amount)
    {
        if (hasDamageReduction) { currentHealth -= amount * damageReductionPercentage; }
        else { currentHealth -= amount; }

        SetHealthPercentage();
        if (playerUIManager != null) { playerUIManager.GetCageHealthPercentage(healthPercentage); }
        canHeal = true;

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        if (playerUIManager != null) { playerUIManager.GetCageHealthPercentage(healthPercentage); }

        SetHealthPercentage();
        if (currentHealth >= maxHealth) 
        {
            currentHealth = maxHealth;
            canHeal = false;
        }
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
