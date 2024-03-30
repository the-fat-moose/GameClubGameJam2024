using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health { get; private set; } = 50f;
    [SerializeField] private GameObject materialPickupPrefab;

    [Header("Audio Parameters")]
    [SerializeField] private AudioClip enemyDamageSound;
    [SerializeField] private AudioClip enemyDeathSound;
    private AudioSource enemyAudioSource;
    private OptionsManager optionsManager;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        optionsManager = GameObject.FindFirstObjectByType<OptionsManager>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            if (enemyAudioSource != null && optionsManager != null) { enemyAudioSource.PlayOneShot(enemyDeathSound, 1f * (optionsManager.sfxVolume * optionsManager.masterVolume)); }

            if (this.gameObject.CompareTag("Enemy"))
            {
                if (skinnedMeshRenderer != null) { skinnedMeshRenderer.enabled = false; }
                TryForDropChance();
                Destroy(gameObject, 2f);
            }
        }
        else
        {
            if (enemyAudioSource != null && optionsManager != null) { enemyAudioSource.PlayOneShot(enemyDamageSound, 1f * (optionsManager.sfxVolume * optionsManager.masterVolume)); }
        }
    }

    private void TryForDropChance()
    {
        float randomNum = Random.Range(0f, 100f);

        if (randomNum <= 50) { SpawnMaterialPickupPrefab(materialPickupPrefab); } // spawn the material drop
    }

    private void SpawnMaterialPickupPrefab(GameObject prefab)
    {
        if (materialPickupPrefab != null) { Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation); }
    }
}
