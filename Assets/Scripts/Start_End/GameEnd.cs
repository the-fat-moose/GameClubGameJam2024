using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool gameIsPaused { get; private set; } = false;

    [Header("Score Ranges")]
    [SerializeField] private float aRankTime = 60.0f;
    [SerializeField] private float bRankTime = 90.0f;
    [SerializeField] private float cRankTime = 120.0f;
    [SerializeField] private float dRankTime = 150.0f;

    [Header("UI Elements")]
    [SerializeField] TMP_Text gradeText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] GameObject gradingCanvas;

    private void Start()
    {
        bestiaryClass = FindFirstObjectByType<Bestiary>();
        gradingCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            creatureCage = other.GetComponent<FirstPersonController>().creatureCage;
            other.GetComponent<FirstPersonController>().playerAudioSourceWalk.enabled = false;
            other.GetComponent<FirstPersonController>().playerAudioSourceOneShots.enabled = false;
            other.GetComponent<FirstPersonController>().CanMove = false;
            gameManager = GameObject.Find("GameManager(Clone)");
            if (gameManager != null && creatureCage != null) 
            {
                GradingSystem();
            }
        }
    }

    private void GradingSystem()
    {
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager gmScript = gameManager.GetComponent<GameManager>();
        CageTarget ctScript = creatureCage.GetComponent<CageTarget>();

        if (gmScript != null && ctScript != null) 
        {
            gradingCanvas.SetActive(true);
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

                    GameObject cageObject = creatureCage.GetComponentInChildren<Creature>().transform.gameObject;
                    CreatureData creatureToAdd = new CreatureData(creatureCage.GetComponentInChildren<Creature>().creatureName, finalTime, finalScore, creatureCage.GetComponentInChildren<Creature>().description);


                    for (int i = 0; i < bestiaryClass.creaturesCaught.Count; i++)
                    {
                        if (creatureToAdd.creatureName == bestiaryClass.creaturesCaught[i].creatureName)
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
                        if (bestiaryClass.creaturesCaught[index].grade < finalScore)
                        {
                            bestiaryClass.creaturesCaught[index].grade = finalScore;
                        }
                        if (bestiaryClass.creaturesCaught[index].time > finalTime)
                        {
                            bestiaryClass.creaturesCaught[index].time = finalTime;
                        }
                    }
                   
                }
            }

            timeText.text = string.Format("Time: {00}:{1:00}", Mathf.FloorToInt(finalTime / 60), Mathf.FloorToInt(finalTime % 60));
            healthText.text = "Health Remaining: " + creatureCage.GetComponent<CageTarget>().currentHealth + "/" + creatureCage.GetComponent<CageTarget>().maxHealth;
            gradeText.text = "Grade: " + finalScore + "/100";
        }
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("HubWorldScene");
    }
}
[Serializable]
public class CreatureData 
{
    public string creatureName { get; private set; } = null;
    public float time { get; set; } = 0f;
    public float grade { get; set; } = 0f;
    public string description { get; private set; } = null;


    public CreatureData(string nameInfo, float timeInfo, float gradeInfo, string descriptionInfo)
    {
        creatureName = nameInfo;
        time = timeInfo;
        grade = gradeInfo;
        description = descriptionInfo;


    }
        
}




