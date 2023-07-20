using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float levelDeathDelay = 10f;
    [SerializeField] float levelLoadDelay = 10f;

    private AudioManager crashAudio;
    private AudioManager levelCompleteAudio;
    public AudioManager mainEngineAudio;


    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem levelCompleteParticles;

    // CACHE - e.g. references in the script for readability or speed
    AudioSource audioSource;
    Movement rocketMovement;

    // STATE - private instance (member) variables e.g. "bool isAlive"
    bool isTransitioning = false;
    bool collisionDisabled = false;

    public static bool playerHasDied = false;

    // bool ToggleChange;

//START METHOD
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // ToggleChange = true;
        rocketMovement = GetComponent<Movement>();
        crashAudio = FindObjectOfType<AudioManager>();
        levelCompleteAudio = FindObjectOfType<AudioManager>();
        mainEngineAudio = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L) && PauseMenu.gameIsPaused == false)
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C) && PauseMenu.gameIsPaused == false)
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) {return;} // || Means or
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
        playerHasDied = true;
        mainEngineAudio.Stop("Main Engine SFX");
        crashAudio.Play("Crash SFX");
        crashParticles.Play();        
        Invoke("ReloadLevel", levelDeathDelay);

        // if (crashAudio == null)
        // {
        // Invoke("ReloadLevel", levelDeathDelay);
        // } else     
        // {

        // }


        
        // ReloadLevel();
    }

    void LevelComplete()
    {
        string scene = SceneManager.GetActiveScene().name; // This line adds string interpolation to say which level is complete in the console
        Debug.Log($"{scene} Complete");
        rocketMovement.enabled = false;
        isTransitioning = true;
        mainEngineAudio.Stop("Main Engine SFX");
        levelCompleteAudio.Play("Level Complete SFX");
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
            nextSceneIndex = 1;
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
