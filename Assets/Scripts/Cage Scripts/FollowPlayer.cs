using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Following Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private float stopDist;
    [SerializeField] private float speed;

    [Header("Bobbing Animation")]
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;

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
