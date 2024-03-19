using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageTarget : MonoBehaviour
{
    public float maxHealth { get; private set; } = 100f;
    public float currentHealth { get; private set; }
    private float damageReductionPercentage = 0.75f;
    public bool hasDamageReduction { get; set; } = false;
    public bool canHeal { get; private set; } = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (hasDamageReduction) { currentHealth -= amount * damageReductionPercentage; }
        else { currentHealth -= amount; }

        canHeal = true;

        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;

        if (currentHealth >= maxHealth) 
        {
            currentHealth = maxHealth;
            canHeal = false;
        }
    }
}
