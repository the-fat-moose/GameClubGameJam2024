using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    public string name { get; set; } = null;
    public float time { get; set; } = 0f;
    public float grade { get; set; } = 0f;
    public char rarity { get; set; }
    public string description { get; set; } = null;
    public string ability { get; set; } = null;
}
