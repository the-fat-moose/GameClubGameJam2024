using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestiaryMenu : MonoBehaviour
{
    [Header("Creature Panel Parameters")]
    [SerializeField] GameObject creaturePanel;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text infoText;

    [Header("Creature Button Parameters")]
    [SerializeField] private Button batButton;
    [SerializeField] private Button caterpillarButton;
    [SerializeField] private Button rockButton;
    [SerializeField] private Button crabButton;
    [SerializeField] private Button dinoButton;

    [Header("Creature Sprites Locked")]
    [SerializeField] private Sprite batLocked;
    [SerializeField] private Sprite caterpillarLocked;
    [SerializeField] private Sprite rockLocked;
    [SerializeField] private Sprite crabLocked;
    [SerializeField] private Sprite dinoLocked;

    [Header("Creature Sprites Unlocked")]
    [SerializeField] private Sprite batUnlocked;
    [SerializeField] private Sprite caterpillarUnlocked;
    [SerializeField] private Sprite rockUnlocked;
    [SerializeField] private Sprite crabUnlocked;
    [SerializeField] private Sprite dinoUnlocked;

    [Header("Creature Sprites Highlighted")]
    [SerializeField] private Sprite batHighlighted;
    [SerializeField] private Sprite caterpillarHighlighted;
    [SerializeField] private Sprite rockHighlighted;
    [SerializeField] private Sprite crabHighlighted;
    [SerializeField] private Sprite dinoHighlighted;

    public List<CreatureData> creaturesCaughtInfo = new List<CreatureData>();

    private void Start()
    {
        creaturePanel.SetActive(false);

        SpriteState ss = new SpriteState();

        batButton.image.sprite = batLocked;
        ss.highlightedSprite = batLocked;
        batButton.spriteState = ss;

        caterpillarButton.image.sprite = caterpillarLocked;
        ss.highlightedSprite = caterpillarLocked;
        caterpillarButton.spriteState = ss;

        rockButton.image.sprite = rockLocked;
        ss.highlightedSprite = rockLocked;
        rockButton.spriteState = ss;

        dinoButton.image.sprite = dinoLocked;
        ss.highlightedSprite = dinoLocked;
        dinoButton.spriteState = ss;

        crabButton.image.sprite = crabLocked;
        ss.highlightedSprite = crabLocked;
        crabButton.spriteState = ss;

        foreach (CreatureData creature in creaturesCaughtInfo) 
        {  
            if (creature != null) 
            {
                if (creature.creatureName == "Caterpillar")
                {
                    caterpillarButton.image.sprite = caterpillarUnlocked;
                    ss.highlightedSprite = caterpillarHighlighted;
                    caterpillarButton.spriteState = ss;
                }
                else if (creature.creatureName == "Bat")
                {
                    batButton.image.sprite = batUnlocked;
                    ss.highlightedSprite = batHighlighted;
                    batButton.spriteState = ss;
                }
                else if (creature.creatureName == "Dino")
                {
                    dinoButton.image.sprite = dinoUnlocked;
                    ss.highlightedSprite = dinoHighlighted;
                    dinoButton.spriteState = ss;
                }
                else if (creature.creatureName == "Geode")
                {
                    rockButton.image.sprite = rockUnlocked;
                    ss.highlightedSprite = rockHighlighted;
                    rockButton.spriteState = ss;
                }
                else if (creature.creatureName == "Crabber")
                {
                    crabButton.image.sprite = crabUnlocked;
                    ss.highlightedSprite = crabHighlighted;
                    crabButton.spriteState = ss;

                }
            }
        }
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
            if (creature.creatureName == "Geode")
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
                if (creature.creatureName == "Crabber")
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
