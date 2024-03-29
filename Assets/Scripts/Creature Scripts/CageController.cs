using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{ 
    public GameObject player { get; private set; }
    private float stopDist = 4f;
    private float speedUpDist = 10f;
    private float speed = 3f;
    private float inStopDistRange = 2f;

    [SerializeField] private GameObject[] creatures;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) { gameObject.GetComponent<CageTarget>().SetPlayerObject(player); }        
    }

    private void Start()
    {
        // Spawn Creatures
        SpawnCreature();
    }

    private void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (dist > stopDist + speedUpDist) { PlayerFollow(speed * 3f); }
        else if (dist < speedUpDist + stopDist && dist > stopDist && !(dist <= stopDist + inStopDistRange)) { PlayerFollow(speed); }
    }

    private void PlayerFollow(float _speed)
    {
        if (player != null)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, _speed * Time.deltaTime);
        }
    }

    private void SpawnCreature()
    {
        int randomInt = Random.Range(0, 5);

        GameObject creature = Instantiate(creatures[randomInt]);
        creature.transform.parent = gameObject.transform;
        creature.transform.position = gameObject.transform.position;
    }
}
