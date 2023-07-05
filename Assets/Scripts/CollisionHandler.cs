using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // string other.gameObject.tag;
    void OnCollisionEnter(Collision other)
    {
        // switch (variableToCompare)
        switch (other.gameObject.tag)
        {
            // case valueA:
            // ActionToTake();
            // break;

            case "Finish":
                LoadNextLevel();
                break;

            case "Friendly":
                Debug.Log("This object is friendly");
                break;

            case "Fuel":
                Debug.Log("You picked up fuel");
                break;

            default:
                ReloadLevel();
                break;
        }
    }

    void LoadNextLevel()
    {
        Debug.Log("Level Complete");
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentLevelIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("GAME COMPLETE");
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        Debug.Log("Sorry you've blown up");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // return the number that the scene currently is playing
        // SceneManager.LoadScene("Sandbox");
        // SceneManager.LoadScene(0);
    }
}
