using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnResumeButtonClick()
    {
        Time.timeScale = 1.0f;
        GameObject player = GameObject.FindFirstObjectByType<FirstPersonController>().gameObject;
        GameObject gameManager = GameObject.FindFirstObjectByType<GameManager>().gameObject;
        if (player != null)
        {
            player.transform.GetChild(0).gameObject.SetActive(true);
        }
        gameObject.SetActive(false);

        if (gameManager != null) 
        {
            gameManager.GetComponent<GameManager>().isGamePaused = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("HubWorldScene");
    }
}
