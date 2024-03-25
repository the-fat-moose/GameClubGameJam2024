using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    public GameObject[] creaturesCaught;

    public void SaveBestiary()
    {
        SaveSystem.SaveUserData(this);
    }

    public void LoadBestiary()
    {
        SaveSystem.LoadData();
    }
}
