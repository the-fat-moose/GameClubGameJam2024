using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestiaryMenu : MonoBehaviour
{
    [SerializeField] GameObject creaturePanel;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text infoText;

    public List<GameObject> creaturesCaughtInfo = new List<GameObject>();

    private void Start()
    {
        creaturePanel.SetActive(false);
    }

    public void OnCaterpillarButtonClick()
    {
        foreach(GameObject creature in creaturesCaughtInfo)
        {
            if (creature.CompareTag("Caterpillar"))
            {
                creaturePanel.SetActive(true);

                nameText.text = "Name: " + creature.GetComponent<Creature>().creatureName;
                timeText.text = "Best Time: " + string.Format("{00}:{1:00}", Mathf.FloorToInt(creature.GetComponent<Creature>().time / 60), Mathf.FloorToInt(creature.GetComponent<Creature>().time % 60));
                scoreText.text = "Best Grade: " + creature.GetComponent<Creature>().grade + "/100";
                infoText.text = creature.GetComponent<Creature>().description;
            }
        }
    }

    public void OnBatButtonClick()
    {
        foreach (GameObject creature in creaturesCaughtInfo)
        {
            if (creature.CompareTag("Bat"))
            {
                creaturePanel.SetActive(true);

                nameText.text = "Name: " + creature.GetComponent<Creature>().creatureName;
                timeText.text = "Best Time: " + string.Format("{00}:{1:00}", Mathf.FloorToInt(creature.GetComponent<Creature>().time / 60), Mathf.FloorToInt(creature.GetComponent<Creature>().time % 60));
                scoreText.text = "Best Grade: " + creature.GetComponent<Creature>().grade + "/100";
                infoText.text = creature.GetComponent<Creature>().description;
            }
        }
    }

    public void OnRockButtonClick()
    {
        foreach (GameObject creature in creaturesCaughtInfo)
        {
            if (creature.CompareTag("Rock"))
            {
                creaturePanel.SetActive(true);

                nameText.text = "Name: " + creature.GetComponent<Creature>().creatureName;
                timeText.text = "Best Time: " + string.Format("{00}:{1:00}", Mathf.FloorToInt(creature.GetComponent<Creature>().time / 60), Mathf.FloorToInt(creature.GetComponent<Creature>().time % 60));
                scoreText.text = "Best Grade: " + creature.GetComponent<Creature>().grade + "/100";
                infoText.text = creature.GetComponent<Creature>().description;
            }
        }
    }

    public void OnDinoButtonClick()
    {
        foreach (GameObject creature in creaturesCaughtInfo)
        {
            if (creature.CompareTag("Dino"))
            {
                creaturePanel.SetActive(true);

                nameText.text = "Name: " + creature.GetComponent<Creature>().creatureName;
                timeText.text = "Best Time: " + string.Format("{00}:{1:00}", Mathf.FloorToInt(creature.GetComponent<Creature>().time / 60), Mathf.FloorToInt(creature.GetComponent<Creature>().time % 60));
                scoreText.text = "Best Grade: " + creature.GetComponent<Creature>().grade + "/100";
                infoText.text = creature.GetComponent<Creature>().description;
            }
        }
    }

    public void OnCrabButtonClick()
    {
        foreach (GameObject creature in creaturesCaughtInfo)
        {
            if (creature.CompareTag("Crab"))
            {
                creaturePanel.SetActive(true);

                nameText.text = "Name: " + creature.GetComponent<Creature>().creatureName;
                timeText.text = "Best Time: " + string.Format("{00}:{1:00}", Mathf.FloorToInt(creature.GetComponent<Creature>().time / 60), Mathf.FloorToInt(creature.GetComponent<Creature>().time % 60));
                scoreText.text = "Best Grade: " + creature.GetComponent<Creature>().grade + "/100";
                infoText.text = creature.GetComponent<Creature>().description;
            }
        }
    }

    public void OnExitButtonClick()
    {
        creaturePanel.SetActive(false);
    }
}
