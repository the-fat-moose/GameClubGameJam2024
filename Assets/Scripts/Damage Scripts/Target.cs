using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private float health = 100f;
    [SerializeField] private GameObject materialPickupPrefab;
    [SerializeField] Slider healthBar;

    private void Start()
    {
        //healthBar.value = health;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        //healthBar.value = health;
        if (health <= 0f)
        {
            if (this.gameObject.CompareTag("Enemy"))
            {
                TryForDropChance();
                Destroy(gameObject);
            }
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
