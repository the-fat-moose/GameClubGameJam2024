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
    private GameObject enemySpawner;
    private GameObject pauseMenu;
    public bool isGamePaused { get; set; } = false;

    private void Start()
    {
        pauseMenu = GameObject.Find("PauseCanvas");
        if (pauseMenu != null ) { pauseMenu.SetActive(false); }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !isGamePaused) 
        {
            pauseMenu.SetActive(true);
            if (player != null) 
            { 
                player.transform.GetChild(0).gameObject.SetActive(false);
                if (player.GetComponent<FirstPersonController>() != null) { player.GetComponent<FirstPersonController>().CanMove = false; }
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            isGamePaused = true;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && isGamePaused)
        {
            Time.timeScale = 1f;
            if (player != null)
            {
                player.transform.GetChild(0).gameObject.SetActive(true);
                if (player.GetComponent<FirstPersonController>() != null) { player.GetComponent<FirstPersonController>().CanMove = true; }
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.SetActive(false);
            isGamePaused = false;
        }

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

    public void SetEnemySpawner(GameObject _spawner)
    {
        if (_spawner != null)
        {
            enemySpawner = _spawner;
        }
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
                player.GetComponentInChildren<PlayerUIManager>().doubleJumpIcon.SetActive(true);
                break;
            case 1: // Dash
                player.GetComponent<FirstPersonController>().canDash = true;
                player.GetComponent<FirstPersonController>().isDashAbility = true;
                player.GetComponentInChildren<PlayerUIManager>().dashIcon.SetActive(true);
                break;
            case 2: // Bullet Damage Increase
                player.GetComponent<Shooting>().canDamageBoost = true;
                StartCoroutine(player.GetComponent<Shooting>().DamageMultiplierCooldown());
                player.GetComponentInChildren<PlayerUIManager>().rageIcon.SetActive(true);
                break;
            case 3: // Cage Damage Reduction
                creatureCage.GetComponent<CageTarget>().hasDamageReduction = true;
                player.GetComponentInChildren<PlayerUIManager>().toughSkinIcon.SetActive(true);
                break;
            case 4: // Decreased Enemy Spawnrate
                enemySpawner.GetComponent<EnemySpawner>().sneakStep = true;
                player.GetComponentInChildren<PlayerUIManager>().silentStepIcon.SetActive(true);
                break;
            default: // Dash in event of coding no worky
                player.GetComponent<FirstPersonController>().canDash = true;
                player.GetComponentInChildren<PlayerUIManager>().dashIcon.SetActive(true);
                break;
        }
    }
}
