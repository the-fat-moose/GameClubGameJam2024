using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    // References
    private FirstPersonController fpc;
    private Shooting shooting;

    // UI References
    [Header("Health Bar Assets")]
    [SerializeField] private GameObject[] healthBar = new GameObject[4];

    [Header("Ammo Assets")]
    [SerializeField] private TMP_Text ammoText;

    [Header("Time Assets")]
    [SerializeField] private TMP_Text timerText;

    [Header("Scrap Assets")]
    [SerializeField] private TMP_Text scrapText;

    [Header("Ability Cooldown Assets")]
    [SerializeField] private GameObject[] cooldownMeter = new GameObject[3];

    public void InitializeUI()
    {
        fpc = GetComponentInParent<FirstPersonController>();
        shooting = GetComponentInParent<Shooting>();

        GetScrapCount(fpc.cageMaterialPickups);
        GetAmmoCount(shooting.currentBulletCount, shooting.maxBulletCount);
    }

    public void GetCageHealthPercentage(float percentage)
    {
        if (percentage >= 0.76f)
        {
            healthBar[0].SetActive(true);
            healthBar[1].SetActive(false);
            healthBar[2].SetActive(false);
            healthBar[3].SetActive(false);
        }
        else if (percentage <= 0.75f && percentage >= 0.51f)
        {
            healthBar[0].SetActive(false);
            healthBar[1].SetActive(true);
            healthBar[2].SetActive(false);
            healthBar[3].SetActive(false);
        }
        else if (percentage <= 0.5f && percentage >= 0.26f)
        {
            healthBar[0].SetActive(false);
            healthBar[1].SetActive(false);
            healthBar[2].SetActive(true);
            healthBar[3].SetActive(false);
        }
        else if (percentage <= 0.25f)
        {
            healthBar[0].SetActive(false);
            healthBar[1].SetActive(false);
            healthBar[2].SetActive(false);
            healthBar[3].SetActive(true);
        }
        else
        {
            healthBar[0].SetActive(true);
            healthBar[1].SetActive(true);
            healthBar[2].SetActive(true);
            healthBar[3].SetActive(true);
        }
    }

    public void GetAmmoCount(float _currentAmmoCount, float _maxAmmoCount)
    {
        ammoText.text = _currentAmmoCount.ToString() + " / " + _maxAmmoCount.ToString();
    }

    public void DisplayReloading()
    {
        ammoText.text = "reloading";
    }

    public void GetScrapCount(int _scrapCount)
    {
        scrapText.text = _scrapCount.ToString() + " scrap";
    }

    public void GetTimer(string _timerText)
    {
        timerText.text = _timerText;
    }
}
