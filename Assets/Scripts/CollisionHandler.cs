using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float levelDeathDelay = 10f;
    [SerializeField] float levelCompleteDelay = 10f;
    [SerializeField] AudioClip playerCrash;
    [SerializeField] AudioClip levelComplete;

    // CACHE - e.g. references in the script for readability or speed
    AudioSource audioSource;

    // STATE - private instance (member) variables e.g. "bool isAlive"
    bool ToggleChange;

//START METHOD
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ToggleChange = true;
    }

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
        Debug.Log("Sorry you've blown up");
        GetComponent<Movement>().enabled = false;
        // if (audioSource.isPlaying != playerCrash)
        // {
            audioSource.Stop();
        // }
        // GetComponent<AudioSource>().enabled = false;
        if (!audioSource.isPlaying && ToggleChange == true)
        {
            audioSource.PlayOneShot(playerCrash);
            ToggleChange = false;
        }
        Invoke("ReloadLevel", levelDeathDelay);
        // ReloadLevel();
    }
    
    void LevelComplete()
    {
        // To do: Add SFX upon level complete
        // To do: Add particle effect upon crash
        string scene = SceneManager.GetActiveScene().name; // This line adds string interpolation to say which level is complete in the console
        Debug.Log($"{scene} Complete");
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(levelComplete);        
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // return the number that the scene currently is playing
        // SceneManager.LoadScene("Sandbox");
        // SceneManager.LoadScene(0);
    }
}
