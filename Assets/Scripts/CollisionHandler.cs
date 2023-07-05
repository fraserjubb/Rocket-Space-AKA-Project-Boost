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
                Debug.Log("Level Complete");
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

    void ReloadLevel()
    {
        Debug.Log("Sorry you've blown up");
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex); // return the number that the scene currently is playing
        // SceneManager.LoadScene("Sandbox");
        // SceneManager.LoadScene(0);
    }
}
