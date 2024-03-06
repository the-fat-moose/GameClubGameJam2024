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
        upgradePointsText.text = upgradePoints.ToString();
        bulletDamageLevelText.text = bulletDamageLevel.ToString();
        bulletRangeLevelText.text = bulletRangeLevel.ToString();
        gunReloadSpeedLevelText.text = gunReloadSpeedLevel.ToString();
        creatureCageHealthLevelText.text = creatureCageHealthLevel.ToString();
    }

    private void OnUpgradeBulletDamageClick()
    {
        if (upgradePoints > 0) 
        {
            bulletDamageLevel++;
            upgradePoints--;
        }
    }

    private void OnUpgradeBulletRangeClick()
    {
        if (upgradePoints > 0)
        {
            bulletRangeLevel++;
            upgradePoints--;
        }
    }

    private void OnUpgradeGunReloadSpeedClick()
    {
        if (upgradePoints > 0)
        {
            gunReloadSpeedLevel++;
            upgradePoints--;
        }
    }

    private void OnUpgradeCageHealthClick()
    {
        if (upgradePoints > 0)
        {
            creatureCageHealthLevel++;
            upgradePoints--;
        }
    }
}
