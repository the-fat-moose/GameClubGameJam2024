using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeElapsed { get; private set; } = 0f;
    private GameObject player;
    private GameObject creatureCage;
    public bool canTimerRun { get; set; } = false;

    private void Update()
    {
        if (canTimerRun)
        {
            timeElapsed += Time.deltaTime;
        }
        SetTimerString(timeElapsed);
    }

    public void FindPlayerItems(GameObject _player, GameObject _creatureCage)
    {
        player = _player;
        creatureCage = _creatureCage;

        if (player.GetComponent<FirstPersonController>() != null && creatureCage.GetComponent<CageTarget>() != null) { SelectAbility(); }
    }

    private void SetTimerString(float timer)
    { 
        float min = Mathf.FloorToInt(timer / 60);
        float sec = Mathf.FloorToInt(timer % 60);

        string displayTime = string.Format("{00}:{1:00}", min, sec);

        if (player.GetComponentInChildren<PlayerUIManager>() != null) { player.GetComponentInChildren<PlayerUIManager>().GetTimer(displayTime); }
    }

    private void SelectAbility()
    {
        int selectInt = Random.Range(1, 101);
        Debug.Log(selectInt);
        selectInt = selectInt % 5;
        Debug.Log(selectInt);

        switch (selectInt) 
        {
            case 0: // Double Jump
                player.GetComponent<FirstPersonController>().canDoubleJump = true;
                break;
            case 1: // Dash
                player.GetComponent<FirstPersonController>().canDash = true;
                player.GetComponent<FirstPersonController>().isDashAbility = true;
                break;
            case 2: // Bullet Damage Increase
                player.GetComponent<Shooting>().canDamageBoost = true;
                StartCoroutine(player.GetComponent<Shooting>().DamageMultiplierCooldown());
                break;
            case 3: // Cage Damage Reduction
                creatureCage.GetComponent<CageTarget>().hasDamageReduction = true;
                break;
            case 4: // Decreased Enemy Spawnrate
                break;
            default: // Dash in event of coding no worky
                player.GetComponent<FirstPersonController>().canDash = true;
                break;
        }
    }
}
