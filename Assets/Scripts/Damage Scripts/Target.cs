using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private float health = 100f;
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
                Destroy(gameObject);
            }
        }
    }
}
