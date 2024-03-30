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

    private Image tgImage;

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

        tgImage.sprite = batLocked;
        batButton.targetGraphic = tgImage;
        ss.highlightedSprite = batLocked;
        batButton.spriteState = ss;

        tgImage.sprite = caterpillarLocked;
        caterpillarButton.targetGraphic = tgImage;
        ss.highlightedSprite = caterpillarLocked;
        caterpillarButton.spriteState = ss;

        tgImage.sprite = rockLocked;
        rockButton.targetGraphic = tgImage;
        ss.highlightedSprite = rockLocked;
        rockButton.spriteState = ss;

        tgImage.sprite = dinoLocked;
        dinoButton.targetGraphic = tgImage;
        ss.highlightedSprite = dinoLocked;
        dinoButton.spriteState = ss;

        tgImage.sprite = crabLocked;
        crabButton.targetGraphic = tgImage;
        ss.highlightedSprite = crabLocked;
        crabButton.spriteState = ss;
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
