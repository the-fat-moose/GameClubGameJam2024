using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    [Header("Following Player")]
    [SerializeField] private GameObject player;
    private float stopDist = 2f;
    private float speed = 3f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (dist > stopDist)
        {
            PlayerFollow();
        }
    }

    private void PlayerFollow()
    {
        if (player != null)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
