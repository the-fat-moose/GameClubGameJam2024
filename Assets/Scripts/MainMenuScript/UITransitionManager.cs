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

    // Start is called before the first frame update
    void Start()
    {
        currentCamera.Priority++;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateCamera(mainmenuCam);
            optionsPanel.SetActive(false);
        }
    }
    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
    }
    public void MainMenuCam()
    {
        UpdateCamera(mainmenuCam);
    }
    public void Options()
    {
        optionsPanel.SetActive(true);
    }
}
