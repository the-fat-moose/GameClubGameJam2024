using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string nextSceneName;  // The name of the scene to transition to
    public float transitionDelay = 5f;  // Time delay before transitioning to the next scene

    void Start()
    {
        // Invoke the TransitionToNextScene method after the specified delay
        Invoke("TransitionToNextScene", transitionDelay);
    }

    void TransitionToNextScene()
    {
        // Load the next scene by name
        SceneManager.LoadScene(nextSceneName);
    }
}
