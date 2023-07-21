using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float levelDeathDelay = 10f;
    [SerializeField] float levelLoadDelay = 10f;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem levelCompleteParticles;

    private AudioManager crashAudio;
    private AudioManager levelCompleteAudio;
    private AudioManager mainEngineAudio;


    // CACHE - e.g. references in the script for readability or speed
    AudioSource audioSource;
    Movement rocketMovement;


    // STATE - private instance (member) variables e.g. "bool isAlive"
    bool isTransitioning = false;
    bool collisionDisabled = false;

    public static bool playerHasDied = false;


//START METHOD
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rocketMovement = GetComponent<Movement>();
        crashAudio = FindObjectOfType<AudioManager>();
        levelCompleteAudio = FindObjectOfType<AudioManager>();
        mainEngineAudio = FindObjectOfType<AudioManager>();
    }

    // void Update()
    // {
    //     RespondToDebugKeys();
    // }

// CHEAT CODES: Make sure to COMMENT OUT BEFORE FINAL BUILD!!!
    // void RespondToDebugKeys()
    // {
    //     if (Input.GetKeyDown(KeyCode.L) && PauseMenu.gameIsPaused == false)
    //     {
    //         LoadNextLevel();
    //     }
    //     else if (Input.GetKeyDown(KeyCode.C) && PauseMenu.gameIsPaused == false)
    //     {
    //         collisionDisabled = !collisionDisabled;
    //     }
    // }
    
    void OnCollisionEnter(Collision objectBeingHit)
    {
        if (isTransitioning || collisionDisabled) {return;} // If already transitioning between variable switches OR collisions have been turned off... It will skip code below. 
        
        switch (objectBeingHit.gameObject.tag) // switch (variableToCompare)
        {
            case "Friendly": // LaunchPad/Player Spawning Point --- case ValueA:
                // DO NOTHING --- ActionToTake();
                // Debug.Log("This object is friendly"); --- break;
                break;            

            case "Finish": // LandingPad/Level Complete
                LevelComplete();
                break;

            default: //Anything else without a tag will destroy player
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

    void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentLevelIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            // Debug.Log("GAME COMPLETE");
            nextSceneIndex = 1;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // return the scene/level number that the player is currently playing
    }

}
