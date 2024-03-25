using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject creatureCage;

    private float finalTime;
    private float finalHealth;
    private float timeScore;
    private float healthScore;
    private float finalScore;

    private Bestiary bestiaryClass;

    [Header("Score Ranges")]
    [SerializeField] private float aRankTime = 60.0f;
    [SerializeField] private float bRankTime = 90.0f;
    [SerializeField] private float cRankTime = 120.0f;
    [SerializeField] private float dRankTime = 150.0f;

    private void Start()
    {
        bestiaryClass = FindFirstObjectByType<Bestiary>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            creatureCage = other.GetComponent<FirstPersonController>().creatureCage;
            gameManager = GameObject.Find("GameManager(Clone)");
            if (gameManager != null && creatureCage != null) 
            {
                GradingSystem();
            }
        }
    }

    private void GradingSystem()
    {
        GameManager gmScript = gameManager.GetComponent<GameManager>();
        CageTarget ctScript = creatureCage.GetComponent<CageTarget>();

        if (gmScript != null && ctScript != null) 
        {
            gmScript.canTimerRun = false;

            finalTime = gmScript.timeElapsed;
            finalHealth = ctScript.healthPercentage;
            
            
            if (finalTime > 0) 
            {
                if (finalTime > 0 && finalTime < aRankTime) //100-90
                {
                    timeScore = ((90.0f + (((aRankTime - finalTime) / aRankTime) * 10.0f)) * 0.7f);
                }
                else if (finalTime >= aRankTime && finalTime < bRankTime) //89-80
                {
                    timeScore = ((80.0f + (((bRankTime - finalTime) / (bRankTime - aRankTime)) * 10.0f)) * 0.7f);
                }
                else if (finalTime >= bRankTime && finalTime < cRankTime) //79-70
                {
                    timeScore = ((70.0f + (((cRankTime - finalTime) / (cRankTime - bRankTime)) * 10.0f)) * 0.7f);
                }
                else if (finalTime >= cRankTime && finalTime < dRankTime) //69-60
                {
                    timeScore = ((60.0f + (((dRankTime - finalTime) / (dRankTime - cRankTime)) * 10.0f)) * 0.7f);
                }
                else if (finalTime >= dRankTime) //0
                {
                    timeScore = 0.0f;
                }

            }

            if(finalHealth > 0)
            {
                healthScore = 30.0f * finalHealth;
            }

            finalScore = timeScore + healthScore;

            finalScore = Mathf.Round(finalScore * 10.0f) * 0.1f;

            if (bestiaryClass != null)
            {
                if (creatureCage.GetComponentInChildren<Creature>() != null) 
                {
                    bool creatureAlreadyAdded = false;
                    int index = 0;

                    GameObject creatureToAdd = creatureCage.GetComponentInChildren<Creature>().transform.gameObject;
                    creatureToAdd.GetComponent<Creature>().grade = finalScore;
                    creatureToAdd.GetComponent<Creature>().time = finalTime;

                    for (int i = 0; i < bestiaryClass.creaturesCaught.Count; i++)
                    {
                        if (creatureToAdd == bestiaryClass.creaturesCaught[i])
                        {
                            creatureAlreadyAdded = true;
                            index = i;
                        }
                    }
                    if (!creatureAlreadyAdded) 
                    {
                        bestiaryClass.creaturesCaught.Add(creatureToAdd);
                    }
                    else
                    {
                        if (bestiaryClass.creaturesCaught[index].GetComponent<Creature>().grade < finalScore)
                        {
                            bestiaryClass.creaturesCaught[index].GetComponent<Creature>().grade = finalScore;
                        }
                        if (bestiaryClass.creaturesCaught[index].GetComponent<Creature>().time > finalTime)
                        {
                            bestiaryClass.creaturesCaught[index].GetComponent<Creature>().time = finalTime;
                        }
                    }
                }
            }

            Debug.Log("Time Score: " + timeScore);
            Debug.Log("Health Score: " + healthScore);
            Debug.Log("Final Score: " + finalScore);
        }
    }
}




