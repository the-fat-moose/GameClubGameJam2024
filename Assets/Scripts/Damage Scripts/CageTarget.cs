using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageTarget : MonoBehaviour
{
    private float maxHealth = 100f;
    private float currentHealth;
    private float damageReductionPercentage = 0.75f;
    public bool hasDamageReduction { get; set; } = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (hasDamageReduction) { currentHealth -= amount * damageReductionPercentage; }
        else { currentHealth -= amount; }

        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
