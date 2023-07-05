using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float levelDeathDelay = 10f;
    [SerializeField] float levelLoadDelay = 10f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip levelCompleteAudio;

    // CACHE - e.g. references in the script for readability or speed
    AudioSource audioSource;
    Movement rocketMovement;

    // STATE - private instance (member) variables e.g. "bool isAlive"
    bool ToggleChange;

//START METHOD
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ToggleChange = true;
        rocketMovement = GetComponent<Movement>();
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
        // To do: Add particle effect upon crash
        Debug.Log("Sorry you've blown up");
        rocketMovement.enabled = false;
        // if (audioSource.isPlaying != playerCrash)
        // {
            audioSource.Stop();
        // }
        // GetComponent<AudioSource>().enabled = false;
        if (!audioSource.isPlaying && ToggleChange == true)
        {
            audioSource.PlayOneShot(crashAudio);
            ToggleChange = false;
        }
        Invoke("ReloadLevel", levelDeathDelay);
        // ReloadLevel();
    }
    
    void LevelComplete()
    {
        // To do: Add particle effect upon crash
        string scene = SceneManager.GetActiveScene().name; // This line adds string interpolation to say which level is complete in the console
        Debug.Log($"{scene} Complete");
        rocketMovement.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(levelCompleteAudio);        
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        Invoke("LoadNextLevel", levelLoadDelay);
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
