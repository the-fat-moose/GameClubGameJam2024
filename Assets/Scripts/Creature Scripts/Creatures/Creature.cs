using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public string creatureName { get; private set; } = null;
    public float time { get; set; } = 0f;
    public float grade { get; set; } = 0f;
    public string description { get; private set; } = null;

    [Header("Creature Specific Variables")]
    [SerializeField] private string creatureDescription = "";
    [SerializeField] private string creatureNameText = "";

    private void Start()
    {
        creatureName = creatureNameText;
        description = creatureDescription;
    }

}
