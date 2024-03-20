using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private Transform Player;

    private int NumberOfEnemiesToSpawn;
    //private float spawnDelay = 1f;
    [SerializeField] private Transform[] spawnPoints;

    private bool sneakStep; //<-----------BOOL FOR SNEAK STEP HERE SHELBY

    private void Awake()
    {
        TotalEnemiesToSpawn(); //this is for seetting the amount of enemies to spawn based on the sneak step
    }

    private void Start()
    {
        for (int i = 0; i < NumberOfEnemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(Enemy, spawnPoints[randSpawnPoint].position, transform.rotation);
    }

    private void TotalEnemiesToSpawn()//this is going to be called multiple times and going to check for the bool even though it isnt going to change                                      
    {                                 //because I still want it to randomize the amount spawned multiple times
        if (sneakStep == true)
        {
            NumberOfEnemiesToSpawn = Random.Range(3, 6);
        }
        else
        {
            NumberOfEnemiesToSpawn = Random.Range(6, 9);
        }
    }

    
}
