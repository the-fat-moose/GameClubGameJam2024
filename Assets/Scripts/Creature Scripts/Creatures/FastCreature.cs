using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastCreature : Creature
{
    private void Start()
    {
        name = "SampleName";
        time = 0f;
        grade = 70f; // on a 0/100 grading scale
        rarity = 'A'; // using F, D, C, B, A, S - Standard Tierlist types not including S+ cause its a Char not a String variable
        description = "This is definitely one of the creatures of all time.";
        ability = "This creature has the dash ability. The dash ability allows the player to rapidly move forward. It appears as if the player is teleporting.";
    }
}
