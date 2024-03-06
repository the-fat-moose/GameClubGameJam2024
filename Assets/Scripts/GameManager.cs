using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float timeElapsed = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        DisplayTimer(timeElapsed);
    }

    private void DisplayTimer(float timer)
    { 
        float min = Mathf.FloorToInt(timer / 60);
        float sec = Mathf.FloorToInt(timer % 60);

        string displayTime = string.Format("{00}:{1:00}", min, sec);

        Debug.Log(displayTime);
    }
}
