using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageTarget : MonoBehaviour
{
    public float maxHealth { get; private set; } = 100f;
    public float currentHealth { get; private set; }
    public float healthPercentage { get; private set; } = 1.0f;
    private float damageReductionPercentage = 0.75f;
    public bool hasDamageReduction { get; set; } = false;
    public bool canHeal { get; private set; } = false;

    CageController controller;
    PlayerUIManager playerUIManager;
    private GameObject player;

    private void Start()
    {
        controller = GetComponent<CageController>();
        currentHealth = maxHealth;
    }

    public void SetPlayerObject(GameObject _player)
    {
        player = _player;

        if (player != null) 
        { 
            playerUIManager = player.GetComponentInChildren<PlayerUIManager>(); 
            if (playerUIManager != null ) { SetHealthPercentage(); }
        }
    }

    public void TakeDamage(float amount)
    {
        if (hasDamageReduction) { currentHealth -= amount * damageReductionPercentage; }
        else { currentHealth -= amount; }

        SetHealthPercentage();
        canHeal = true;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;

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

        if (playerUIManager != null) { playerUIManager.GetCageHealthPercentage(healthPercentage); }
    }
}
