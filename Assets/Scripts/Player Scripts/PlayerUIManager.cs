using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    // References
    private FirstPersonController fpc;
    private Shooting shooting;

    // UI References
    [Header("Health Bar Assets")]
    [SerializeField] private GameObject[] healthBar = new GameObject[4];
    [SerializeField] private TMP_Text healthText;

    [Header("Ammo Assets")]
    [SerializeField] private TMP_Text ammoText;

    [Header("Time Assets")]
    [SerializeField] private TMP_Text timerText;

    [Header("Scrap Assets")]
    [SerializeField] private TMP_Text scrapText;

    [Header("Ability Cooldown Assets")]
    [SerializeField] private GameObject[] cooldownMeter = new GameObject[3];

    [Header("Ability Assets")]
    public GameObject dashIcon;
    public GameObject doubleJumpIcon;
    public GameObject rageIcon;
    public GameObject silentStepIcon;
    public GameObject toughSkinIcon;

    public void Update()
    {
        if (fpc != null && shooting != null) { DisplayAbilityCooldown(); }
    }

    public void InitializeUI()
    {
        fpc = GetComponentInParent<FirstPersonController>();
        shooting = GetComponentInParent<Shooting>();

        GetScrapCount(fpc.cageMaterialPickups);
        GetAmmoCount(shooting.currentBulletCount, shooting.maxBulletCount);
    }

    public void GetCageHealthPercentage(float percentage, float _currentHealth, float _maxHealth)
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

        healthText.text = _currentHealth + " / " + _maxHealth;
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

    private void DisplayAbilityCooldown()
    {
        if (fpc.isDashAbility)
        {
            float dashCooldownPercentage = fpc.dashCooldown / fpc.maxDashCooldown;

            if (fpc.canDash)
            {
                cooldownMeter[0].SetActive(true);
                cooldownMeter[1].SetActive(false);
                cooldownMeter[2].SetActive(false);
            }
            else if (dashCooldownPercentage <= 1f && dashCooldownPercentage > 0.5f) 
            {
                cooldownMeter[0].SetActive(false);
                cooldownMeter[1].SetActive(false);
                cooldownMeter[2].SetActive(true);
                
            }
            else if (dashCooldownPercentage <= 0.5f)
            {
                cooldownMeter[0].SetActive(false);
                cooldownMeter[1].SetActive(true);
                cooldownMeter[2].SetActive(false);
            }
            else if (dashCooldownPercentage <= 0.0f)
            {
                cooldownMeter[0].SetActive(true);
                cooldownMeter[1].SetActive(false);
                cooldownMeter[2].SetActive(false);
            }
            
        }
        if (fpc.canDoubleJump)
        {
            if (fpc.remainingJumps <= 0)
            {
                cooldownMeter[0].SetActive(false);
                cooldownMeter[1].SetActive(false);
                cooldownMeter[2].SetActive(true);
            }
            else if (!fpc.characterController.isGrounded && fpc.remainingJumps >= 1)
            {
                cooldownMeter[0].SetActive(false);
                cooldownMeter[1].SetActive(true);
                cooldownMeter[2].SetActive(false);
            }
            else
            {
                cooldownMeter[0].SetActive(true);
                cooldownMeter[1].SetActive(false);
                cooldownMeter[2].SetActive(false);
            }
        }
        if (shooting.canDamageBoost)
        {
            float damageBoostCooldownPercentage = shooting.damageBoostCooldown / shooting.damageBoostMaxCooldown;

            if (shooting.canActivateDamageBoost)
            {
                cooldownMeter[0].SetActive(true);
                cooldownMeter[1].SetActive(false);
                cooldownMeter[2].SetActive(false);
                if (cooldownMeter[0].GetComponent<Image>() != null)
                {
                    cooldownMeter[0].GetComponent<Image>().color = Color.white;
                }
                if (cooldownMeter[1].GetComponent<Image>() != null)
                {
                    cooldownMeter[1].GetComponent<Image>().color = Color.white;
                }
            }
            else if (damageBoostCooldownPercentage <= 1f && damageBoostCooldownPercentage > 0.5f && shooting.damageMultiplierEnabled)
            {
                cooldownMeter[0].SetActive(false);
                cooldownMeter[1].SetActive(false);
                cooldownMeter[2].SetActive(true);
                if (cooldownMeter[0].GetComponent<Image>() != null)
                {
                    cooldownMeter[0].GetComponent<Image>().color = Color.green;
                }
                if (cooldownMeter[1].GetComponent<Image>() != null)
                {
                    cooldownMeter[1].GetComponent<Image>().color = Color.green;
                }

            }
            else if (damageBoostCooldownPercentage <= 0.5f && shooting.damageMultiplierEnabled)
            {
                cooldownMeter[0].SetActive(false);
                cooldownMeter[1].SetActive(true);
                cooldownMeter[2].SetActive(false);
                if (cooldownMeter[0].GetComponent<Image>() != null)
                {
                    cooldownMeter[0].GetComponent<Image>().color = Color.green;
                }
                if (cooldownMeter[1].GetComponent<Image>() != null)
                {
                    cooldownMeter[1].GetComponent<Image>().color = Color.green;
                }
            }
            else if (damageBoostCooldownPercentage <= 0.0f && shooting.damageMultiplierEnabled)
            {
                cooldownMeter[0].SetActive(true);
                cooldownMeter[1].SetActive(false);
                cooldownMeter[2].SetActive(false);
                if (cooldownMeter[0].GetComponent<Image>() != null)
                {
                    cooldownMeter[0].GetComponent<Image>().color = Color.green;
                }
                if (cooldownMeter[1].GetComponent<Image>() != null)
                {
                    cooldownMeter[1].GetComponent<Image>().color = Color.green;
                }
            }
            else if (damageBoostCooldownPercentage <= 1f && damageBoostCooldownPercentage > 0.5f)
            {
                cooldownMeter[0].SetActive(false);
                cooldownMeter[1].SetActive(false);
                cooldownMeter[2].SetActive(true);
                if (cooldownMeter[0].GetComponent<Image>() != null)
                {
                    cooldownMeter[0].GetComponent<Image>().color = Color.white;
                }
                if (cooldownMeter[1].GetComponent<Image>() != null)
                {
                    cooldownMeter[1].GetComponent<Image>().color = Color.white;
                }
            }
            else if (damageBoostCooldownPercentage <= 0.5f)
            {
                cooldownMeter[0].SetActive(false);
                cooldownMeter[1].SetActive(true);
                cooldownMeter[2].SetActive(false);
                if (cooldownMeter[0].GetComponent<Image>() != null)
                {
                    cooldownMeter[0].GetComponent<Image>().color = Color.white;
                }
                if (cooldownMeter[1].GetComponent<Image>() != null)
                {
                    cooldownMeter[1].GetComponent<Image>().color = Color.white;
                }
            }
            else if (damageBoostCooldownPercentage <= 0.0f)
            {
                cooldownMeter[0].SetActive(true);
                cooldownMeter[1].SetActive(false);
                cooldownMeter[2].SetActive(false);
                if (cooldownMeter[0].GetComponent<Image>() != null)
                {
                    cooldownMeter[0].GetComponent<Image>().color = Color.white;
                }
                if (cooldownMeter[1].GetComponent<Image>() != null)
                {
                    cooldownMeter[1].GetComponent<Image>().color = Color.white;
                }
            }


        }
        if (!shooting.canDamageBoost && !fpc.isDashAbility && !fpc.canDoubleJump)
        {
            cooldownMeter[0].SetActive(true);
            if (cooldownMeter[0].GetComponent<Image>() != null)
            {
                cooldownMeter[0].GetComponent<Image>().color = Color.red;
            }
            if (cooldownMeter[1].GetComponent<Image>() != null)
            {
                cooldownMeter[1].GetComponent<Image>().color = Color.red;
            }
        }
    }
}
