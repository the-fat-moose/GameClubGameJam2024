using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITransitionManager : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera mainmenuCam;
    public string sceneToLoad;
    public GameObject optionsPanel;
    public GameObject controlsPanel;
    public GameObject audioPanel;
    public GameObject BackButton;
    public GameObject BackAcceptButton;
    public GameObject CreaturePanel;
    public Image fade;
    public Image fadeout;

    public GameObject HelpMenu;
    public GameObject helpPanel1;
    public GameObject helpPanel2;
    public GameObject helpPanel3;
    public GameObject helpPanel4;
    public GameObject NextArrowButton;
    public GameObject BackArrowButton;

    private void Awake()
    {
        StartCoroutine(FadeIn(0.5f));
        fadeout.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentCamera.Priority++;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!audioPanel.activeInHierarchy && !controlsPanel.activeInHierarchy && !HelpMenu.activeInHierarchy)
            {
                UpdateCamera(mainmenuCam);
                optionsPanel.SetActive(false);
                audioPanel.SetActive(false);
                controlsPanel.SetActive(false);
                BackAcceptButton.SetActive(false);
                BackButton.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Escape) && BackAcceptButton.activeInHierarchy)
            {
                audioPanel.SetActive(false);
                optionsPanel.SetActive(true);
                controlsPanel.SetActive(false);
                BackButton.SetActive(true);
                BackAcceptButton.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Escape) && CreaturePanel.activeInHierarchy)
            {
                CreaturePanel.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Escape) && HelpMenu.activeInHierarchy)
            {
                optionsPanel.SetActive(true);
                BackButton.SetActive(true);
                HelpMenu.SetActive(false);
                BackAcceptButton.SetActive(false);
                helpPanel2.SetActive(false);
                helpPanel3.SetActive(false);
                helpPanel4.SetActive(false);
            }
        }
    }
    public void UpdateCamera(CinemachineVirtualCamera target) // this causes the camera to move around the scene at the click of a button.
    {
        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
    }
    public void MainMenuCam() // This is the back button.
    {
        UpdateCamera(mainmenuCam);
        optionsPanel.SetActive(false);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(false);
        BackAcceptButton.SetActive(false);
        BackButton.SetActive(false);
    }
    public void Options()
    {
        optionsPanel.SetActive(true);
        //BackAcceptButton.SetActive(true);
    }
    public void ControlsButton()
    {
        controlsPanel.SetActive(true);
        optionsPanel.SetActive(false);
        audioPanel.SetActive(false);
        BackButton.SetActive(false);
        BackAcceptButton.SetActive(true);
    }
    public void AudioButton()
    {
        audioPanel.SetActive(true);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        BackButton.SetActive(false);
        BackAcceptButton.SetActive(true);
    }
    public void AcceptButton()
    {
        audioPanel.SetActive(false);
        optionsPanel.SetActive(true);
        controlsPanel.SetActive(false);
        HelpMenu.SetActive(false);
        BackButton.SetActive(true);
        BackAcceptButton.SetActive(false);

        helpPanel2.SetActive(false);
        helpPanel3.SetActive(false);
        helpPanel4.SetActive(false);
    }
    public void BackButtonActive()
    {
        BackButton.SetActive(true);
    }

    public void HelpMenuActive()
    {
        HelpMenu.SetActive(true);
        helpPanel1.SetActive(true);
        NextArrowButton.SetActive(true);
        BackArrowButton.SetActive(false);
        optionsPanel.SetActive(false);
        BackButton.SetActive(false);
        BackAcceptButton.SetActive(true);
    }
    public void NextArrow()
    {
        if (helpPanel1.activeInHierarchy)
        {
            helpPanel1.SetActive(false);
            helpPanel2.SetActive(true);
            BackArrowButton.SetActive(true);
        }
        else if (helpPanel2.activeInHierarchy)
        {
            helpPanel2.SetActive(false);
            helpPanel3.SetActive(true);
        }
        else if (helpPanel3.activeInHierarchy)
        {
            helpPanel3.SetActive(false);
            helpPanel4.SetActive(true);
            NextArrowButton.SetActive(false);
        }
    }
    public void BackArrow()
    {
        if (helpPanel4.activeInHierarchy)
        {
            helpPanel4.SetActive(false);
            helpPanel3.SetActive(true);
            NextArrowButton.SetActive(true);
        }
        else if (helpPanel3.activeInHierarchy)
        {
            helpPanel3.SetActive(false);
            helpPanel2.SetActive(true);
        }
        else if (helpPanel2.activeInHierarchy)
        {
            helpPanel2.SetActive(false);
            helpPanel1.SetActive(true);
            BackArrowButton.SetActive(false);
        }
    }




    public void LevelOneStart()
    {
        StartCoroutine(FadeOut(1f, 2));
    }
    public void LevelTwoStart()
    {
        StartCoroutine(FadeOut(1f, 3));
    }
    public void LevelThreeStart()
    {
        StartCoroutine(FadeOut(1f, 4));
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public IEnumerator FadeIn(float duration)
    {
        fade.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(1f);
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);

            fade.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        fade.enabled = false;
    }
    public IEnumerator FadeOut(float duration, int levelNum)
    {
        fadeout.enabled = true;
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, counter / duration);
            fadeout.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        SceneManager.LoadScene(levelNum);
    }
}
