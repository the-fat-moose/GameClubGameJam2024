using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // player save data
    public int bulletDamageLevel;
    public int bulletRangeLevel;
    public int gunReloadSpeedLevel;
    // creature cage save data
    public int creatureCageHealthLevel;

    public SaveData (UpgradeBench upgrade)
    {
        bulletDamageLevel = upgrade.bulletDamageLevel;
        bulletRangeLevel = upgrade.bulletRangeLevel;
        gunReloadSpeedLevel = upgrade.gunReloadSpeedLevel;
        creatureCageHealthLevel = upgrade.creatureCageHealthLevel;
    }
}
