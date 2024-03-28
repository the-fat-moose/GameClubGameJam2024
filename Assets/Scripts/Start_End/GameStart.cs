using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [Header("Items to Spawn")]
    [SerializeField] private GameObject firstPersonControllerPref;
    [SerializeField] private GameObject creatureCagePref;
    [SerializeField] private GameObject gameManagerPref;
    private GameObject enemySpawner;

    private GameObject gameManager; // used to assign the game manager that is instatiated, for use OnTriggerExit
    private GameObject firstPersonController; // used to assign the first person controller that is instantiated, for use on Start
    private GameObject creatureCage; // used to assign the creature cage that is instantiated, for use on Start

    private void Start()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        if (firstPersonControllerPref != null && creatureCagePref != null && gameManagerPref != null && enemySpawner != null) 
        { 
            SpawnStartingItems();
        }
    }

    private void SpawnStartingItems()
    {
        gameManager = GameObject.Instantiate(gameManagerPref, new Vector3(0f, 0f, 0f), Quaternion.identity);
        firstPersonController = GameObject.Instantiate(firstPersonControllerPref, new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + 2f), gameObject.transform.position.z), gameObject.transform.rotation);
        creatureCage = GameObject.Instantiate(creatureCagePref, new Vector3((gameObject.transform.position.x + 10f), (gameObject.transform.position.y + 1f), gameObject.transform.position.z), gameObject.transform.rotation);

        if (enemySpawner.GetComponent<EnemySpawner>() != null) { enemySpawner.GetComponent<EnemySpawner>().SetCreatureCage(creatureCage); }
        if (gameManager.GetComponent<GameManager>() != null) { gameManager.GetComponent<GameManager>().FindPlayerItems(firstPersonController, creatureCage); } // assign first person controller and creature cage to the game manager
        if (firstPersonController != null)
        {
            if (firstPersonController.GetComponent<FirstPersonController>() != null && firstPersonController.GetComponentInChildren<PlayerUIManager>() != null) 
            { 
                firstPersonController.GetComponent<FirstPersonController>().SetCreatureCage(creatureCage);
                firstPersonController.GetComponentInChildren<PlayerUIManager>().InitializeUI();
            }
        }
    }

    private void StartGameTimer(GameObject manager)
    {
        if (manager.GetComponent<GameManager>() != null) { manager.GetComponent<GameManager>().canTimerRun = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartGameTimer(gameManager);
        }
    }
}
