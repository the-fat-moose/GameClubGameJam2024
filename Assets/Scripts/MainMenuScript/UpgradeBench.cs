using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeBench : MonoBehaviour
{
    // bench variables
    public int upgradePoints = 0;
    // player save data
    public int bulletDamageLevel = 0;
    public int bulletRangeLevel = 0;
    public int gunReloadSpeedLevel = 0;
    // creature cage save data
    public int creatureCageHealthLevel = 0;

    [SerializeField] private TextMeshProUGUI upgradePointsText;
    [SerializeField] private TextMeshProUGUI bulletDamageLevelText;
    [SerializeField] private TextMeshProUGUI bulletRangeLevelText;
    [SerializeField] private TextMeshProUGUI gunReloadSpeedLevelText;
    [SerializeField] private TextMeshProUGUI creatureCageHealthLevelText;

    private void Start()
    {
        // initialize display of variables in level
        upgradePointsText.text = "Skill Points Left: " + upgradePoints.ToString();
        bulletDamageLevelText.text = "Level: " + bulletDamageLevel.ToString();
        bulletRangeLevelText.text = "Level: " + bulletRangeLevel.ToString();
        gunReloadSpeedLevelText.text = "Level: " + gunReloadSpeedLevel.ToString();
        creatureCageHealthLevelText.text = "Level: " + creatureCageHealthLevel.ToString();
    }

    public void OnUpgradeBulletDamageClick()
    {
        if (upgradePoints > 0) 
        {
            bulletDamageLevel++;
            bulletDamageLevelText.text = "Level: " + bulletDamageLevel.ToString();
            upgradePoints--;
            upgradePointsText.text = "Skill Points Left: " + upgradePoints.ToString();
        }
    }

    public void OnUpgradeBulletRangeClick()
    {
        if (upgradePoints > 0)
        {
            bulletRangeLevel++;
            bulletRangeLevelText.text = "Level: " + bulletRangeLevel.ToString();
            upgradePoints--;
            upgradePointsText.text = "Skill Points Left: " + upgradePoints.ToString();
        }
    }

    public void OnUpgradeGunReloadSpeedClick()
    {
        if (upgradePoints > 0)
        {
            gunReloadSpeedLevel++;
            gunReloadSpeedLevelText.text = "Level: " + gunReloadSpeedLevel.ToString();
            upgradePoints--;
            upgradePointsText.text = "Skill Points Left: " + upgradePoints.ToString();
        }
    }

    public void OnUpgradeCageHealthClick()
    {
        if (upgradePoints > 0)
        {
            creatureCageHealthLevel++;
            creatureCageHealthLevelText.text = "Level: " + creatureCageHealthLevel.ToString();
            upgradePoints--;
            upgradePointsText.text = "Skill Points Left: " + upgradePoints.ToString();
        }
    }
}
