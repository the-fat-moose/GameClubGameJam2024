using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    [Header("Following Player")]
    [SerializeField] private GameObject player;
    private float stopDist = 4f;
    private float speedUpDist = 10f;
    private float speed = 3f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (dist > stopDist + speedUpDist) { PlayerFollow(speed * 3f); }
        else if (dist < speedUpDist + stopDist && dist > stopDist) { PlayerFollow(speed); }
    }

    private void PlayerFollow(float _speed)
    {
        if (player != null)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, _speed * Time.deltaTime);
        }
    }
}
