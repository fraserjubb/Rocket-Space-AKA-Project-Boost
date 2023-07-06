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

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem levelCompleteParticles;

    // CACHE - e.g. references in the script for readability or speed
    AudioSource audioSource;
    Movement rocketMovement;

    // STATE - private instance (member) variables e.g. "bool isAlive"
    bool isTransitioning = false;
    // bool ToggleChange;

//START METHOD
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // ToggleChange = true;
        rocketMovement = GetComponent<Movement>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) {return;}
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
        isTransitioning = true;
        rocketMovement.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticles.Play();
        Invoke("ReloadLevel", levelDeathDelay);
        // ReloadLevel();
    }

    void LevelComplete()
    {
        string scene = SceneManager.GetActiveScene().name; // This line adds string interpolation to say which level is complete in the console
        Debug.Log($"{scene} Complete");
        rocketMovement.enabled = false;
        isTransitioning = true;
        audioSource.Stop();        
        audioSource.PlayOneShot(levelCompleteAudio);
        levelCompleteParticles.Play();        
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        Invoke("LoadNextLevel", levelLoadDelay);
    }


    // void StartCrashSequence()
    // {
    //     // To do: Add particle effect upon crash
    //     Debug.Log("Sorry you've blown up");
    //     rocketMovement.enabled = false;
    //     if (isTransitioning == false)
    //     {
    //         audioSource.Stop();
    //     }
    //     // GetComponent<AudioSource>().enabled = false;
    //     if (!audioSource.isPlaying && isTransitioning == false)
    //     {
    //         audioSource.PlayOneShot(crashAudio);
    //         // ToggleChange = false;
    //         isTransitioning = true;
    //     }
    //     Invoke("ReloadLevel", levelDeathDelay);
    //     // ReloadLevel();
    // }
    
    // void LevelComplete()
    // {
    //     // To do: Add particle effect upon crash
    //     string scene = SceneManager.GetActiveScene().name; // This line adds string interpolation to say which level is complete in the console
    //     Debug.Log($"{scene} Complete");
    //     rocketMovement.enabled = false;
    //     if (isTransitioning == false)
    //     {
    //         audioSource.Stop();
    //     }
    //     if (!audioSource.isPlaying && isTransitioning == false)
    //     {
    //         audioSource.PlayOneShot(levelCompleteAudio);
    //         isTransitioning = true;
    //     }        
    //         GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
    //         Invoke("LoadNextLevel", levelLoadDelay);
    // }
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
