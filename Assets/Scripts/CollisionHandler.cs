using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // string other.gameObject.tag;
    [SerializeField] float levelDeathDelay = 10f;
    [SerializeField] float levelCompleteDelay = 10f;

    void OnCollisionEnter(Collision other)
    {
        // switch (variableToCompare)
        switch (other.gameObject.tag)
        {
            // case valueA:
            // ActionToTake();
            // break;

            case "Finish":
                LevelComplete();
                break;

            case "Friendly":
                Debug.Log("This object is friendly");
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        // To do: Add SFX upon crash
        // To do: Add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelDeathDelay);
        // ReloadLevel();
    }
    
    void LevelComplete()
    {
        // Add string interpolation to say which level is complete in the console
        Debug.Log("Level Complete");
        GetComponent<Movement>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        Invoke("LoadNextLevel", levelCompleteDelay);
    }
    void LoadNextLevel()
    {
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
