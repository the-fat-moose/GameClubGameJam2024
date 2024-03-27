using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class UITransitionManager : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera mainmenuCam;
    public string sceneToLoad;
    public GameObject optionsPanel;
    public GameObject controlsPanel;
    public GameObject audioPanel;
    public GameObject BackButton;

    // Start is called before the first frame update
    void Start()
    {
        currentCamera.Priority++;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!audioPanel.activeInHierarchy && !controlsPanel.activeInHierarchy)
            {
                UpdateCamera(mainmenuCam);
                optionsPanel.SetActive(false);
                audioPanel.SetActive(false);
                controlsPanel.SetActive(false);
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
    }
    public void Options()
    {
        optionsPanel.SetActive(true);
    }
    public void ControlsButton()
    {
        controlsPanel.SetActive(true);
        optionsPanel.SetActive(false);
        audioPanel.SetActive(false);
        BackButton.SetActive(false);
    }
    public void AudioButton()
    {
        audioPanel.SetActive(true);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        BackButton.SetActive(false);
    }
    public void AcceptButton()
    {
        audioPanel.SetActive(false);
        optionsPanel.SetActive(true);
        controlsPanel.SetActive(false);
        BackButton.SetActive(true);
    }

    public void LevelOneStart()
    {
        SceneManager.LoadScene(1);
    }
    public void LevelTwoStart()
    {
        SceneManager.LoadScene(2);
    }
    public void LevelThreeStart()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
