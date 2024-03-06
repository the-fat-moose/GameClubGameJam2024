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
    // bestiary save data
    public string[] name;
    public float[] time;
    public float[] grade;
    public char[] rarity;
    public string[] description;
    public string[] ability;

    public SaveData (UpgradeBench upgrade, Bestiary bestiary)
    {
        // upgrade save data
        bulletDamageLevel = upgrade.bulletDamageLevel;
        bulletRangeLevel = upgrade.bulletRangeLevel;
        gunReloadSpeedLevel = upgrade.gunReloadSpeedLevel;
        creatureCageHealthLevel = upgrade.creatureCageHealthLevel;

        // bestiary save data
        for (int i = 0; i < bestiary.creaturesCaught.Length; i++)
        {
            name[i] = bestiary.creaturesCaught[i].GetComponent<Creature>().name;
            time[i] = bestiary.creaturesCaught[i].GetComponent<Creature>().time;
            grade[i] = bestiary.creaturesCaught[i].GetComponent<Creature>().grade;
            rarity[i] = bestiary.creaturesCaught[i].GetComponent<Creature>().rarity;
            description[i] = bestiary.creaturesCaught[i].GetComponent<Creature>().description;
            ability[i] = bestiary.creaturesCaught[i].GetComponent<Creature>().ability;
        }
    }
}
