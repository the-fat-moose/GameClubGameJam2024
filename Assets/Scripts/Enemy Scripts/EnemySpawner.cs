using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private Transform player;

    private int NumberOfEnemiesToSpawn;
    //private float spawnDelay = 1f;
    [SerializeField] private Transform[] spawnPoints;

    public bool sneakStep { get; set; } = false; //<-----------BOOL FOR SNEAK STEP HERE SHELBY

    private void Awake()
    {
        TotalEnemiesToSpawn(); //this is for setting the amount of enemies to spawn based on the sneak step
    }

    public void SetCreatureCage(GameObject _player)
    {
        player = _player.transform;

        for (int i = 0; i < NumberOfEnemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        GameObject _enemy = Instantiate(enemy, spawnPoints[randSpawnPoint].position, transform.rotation);
        if (_enemy.GetComponent<EnemyAi>() != null) { enemy.GetComponent<EnemyAi>().SetCreatureCage(player.gameObject); }
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
