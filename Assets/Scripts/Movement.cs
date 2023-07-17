using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
//VARIABLES 
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationSpeed = 150f;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;


    // CACHE - e.g. references in the script for readability or speed
    Rigidbody rb;
    AudioSource audioSource;
    // Physics physics;
    // STATE - private instance (member) variables e.g. "bool isAlive"

//START METHOD
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        // physics = GetComponent<Physics>();
        // audioSource.Play(0);
        // Debug.Log("started");
    }

//UPDATE METHOD
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        // ReturnToMenu();
    }

// PUBLIC METHODS


// PRIVATE METHODS
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) && PauseMenu.gameIsPaused == false) // && Tutorial.initialTutorialIsRunning == false)
        {
            StartThrusting();
        } // Make sure to close bracket here as a seperate block = above is if space bar is pressed
        else // if space bar is not pressed = below is when space bar is not pressed
        {
            StopThrusting();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void StartThrusting()
    {
        // Debug.Log("Pressed SPACE - Thrusting");
        // rb.AddRelativeForce(0, mainThrust *Time.deltaTime, 0);
        // Alternative code:
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying && !mainEngineParticles.isPlaying) // if audio is not already playing then
        {
            audioSource.PlayOneShot(mainEngine);
            mainEngineParticles.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotatingLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotatingRight();
        }
        else
            StoppingRotating();
    }


    void RotatingLeft()
    {
        // Debug.Log("Pressed A - Rotate Left");        
        ApplyRotation(rotationSpeed);
        if (!rightEngineParticles.isPlaying)
        {
            rightEngineParticles.Play();
        }
    }

    void RotatingRight()
    {
        // Debug.Log("Pressed D - Rotate Right");
        ApplyRotation(-rotationSpeed);
        if (!leftEngineParticles.isPlaying)
        {
            leftEngineParticles.Play();
        }
    }

    void StoppingRotating()
    {
        rightEngineParticles.Stop();
        leftEngineParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so that the physics system can take over.
    }

    // // void ReturnToMenu()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         SceneManager.LoadScene("Main Menu");
    //     }
    // }

    // void TurnOffCollisions()
    // {
    //     if (Input.GetKey(KeyCode.C))
    //     {
    //         physics.IgnoreCollision();
    //     }
    // }

}