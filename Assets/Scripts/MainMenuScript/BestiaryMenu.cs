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

    public List<CreatureData> creaturesCaughtInfo = new List<CreatureData>();

    private void Start()
    {
        creaturePanel.SetActive(false);
    }

    public void OnCaterpillarButtonClick()
    {
        foreach(CreatureData creature in creaturesCaughtInfo)
        {
            if (creature.creatureName == "Caterpillar")
            {
                creaturePanel.SetActive(true);

                nameText.text = "Name: " + creature.creatureName;
                timeText.text = "Best Time: " + string.Format("{00}:{1:00}", Mathf.FloorToInt(creature.time / 60), Mathf.FloorToInt(creature.time % 60));
                scoreText.text = "Best Grade: " + creature.grade + "/100";
                infoText.text = creature.description;
            }
        }
    }

    public void OnBatButtonClick()
    {
        foreach (CreatureData creature in creaturesCaughtInfo)
        {
            if (creature.creatureName == "Bat")
            {
                creaturePanel.SetActive(true);

                nameText.text = "Name: " + creature.creatureName;
                timeText.text = "Best Time: " + string.Format("{00}:{1:00}", Mathf.FloorToInt(creature.time / 60), Mathf.FloorToInt(creature.time % 60));
                scoreText.text = "Best Grade: " + creature.grade + "/100";
                infoText.text = creature.description;
            }
        }
    }

    public void OnRockButtonClick()
    {
        foreach (CreatureData creature in creaturesCaughtInfo)
        {
            if (creature.creatureName == "Rock")
            {
                creaturePanel.SetActive(true);

                nameText.text = "Name: " + creature.creatureName;
                timeText.text = "Best Time: " + string.Format("{00}:{1:00}", Mathf.FloorToInt(creature.time / 60), Mathf.FloorToInt(creature.time % 60));
                scoreText.text = "Best Grade: " + creature.grade + "/100";
                infoText.text = creature.description;
            }
        }
    }

    public void OnDinoButtonClick()
    {
        foreach (CreatureData creature in creaturesCaughtInfo)
        {
            if (creature.creatureName == "Dino")
            {
                creaturePanel.SetActive(true);

                nameText.text = "Name: " + creature.creatureName;
                timeText.text = "Best Time: " + string.Format("{00}:{1:00}", Mathf.FloorToInt(creature  .time / 60), Mathf.FloorToInt(creature.time % 60));
                scoreText.text = "Best Grade: " + creature.grade + "/100";
                infoText.text = creature.description;
            }
            }
        }

        public void OnCrabButtonClick()
        {
            foreach (CreatureData creature in creaturesCaughtInfo)
            {
                if (creature.creatureName == "Crab")
            {
                creaturePanel.SetActive(true);

                nameText.text = "Name: " + creature.creatureName;
                timeText.text = "Best Time: " + string.Format("{00}:{1:00}", Mathf.FloorToInt(creature.time / 60), Mathf.FloorToInt(creature.time % 60));
                scoreText.text = "Best Grade: " + creature.grade + "/100";
                infoText.text = creature.description;
            }
        }
    }

    public void OnExitButtonClick()
    {
        creaturePanel.SetActive(false);
    }
}
