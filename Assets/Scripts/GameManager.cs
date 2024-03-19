using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float timeElapsed = 0;
    private GameObject player;
    private GameObject creatureCage;

    // called zero
    private void Awake()
    {
        Debug.Log("Awake");
        DontDestroyOnLoad(gameObject);
    }

    // called first
    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log("mode: " + mode);
    }

    // called third
    private void Start()
    {
        Debug.Log("Start");
        player = GameObject.Find("FirstPersonController");
        creatureCage = GameObject.Find("CreatureCage");

        if (player != null && creatureCage != null)
        {
            SelectAbility();
        }
    }

    private void Update()
    {
        /*timeElapsed += Time.deltaTime;
        DisplayTimer(timeElapsed);*/
    }

    private void DisplayTimer(float timer)
    { 
        float min = Mathf.FloorToInt(timer / 60);
        float sec = Mathf.FloorToInt(timer % 60);

        string displayTime = string.Format("{00}:{1:00}", min, sec);

        Debug.Log(displayTime);
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

    // called when game is terminated
    private void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
