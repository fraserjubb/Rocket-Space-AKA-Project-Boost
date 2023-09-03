using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
//VARIABLES 
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationSpeed = 150f;

    private AudioManager mainEngineAudio;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;


    // CACHE - e.g. references in the script for readability or speed
    Rigidbody rb;

    // STATE - private instance (member) variables e.g. "bool isAlive"

//START METHOD
    void Start()
    {        
        rb = GetComponent<Rigidbody>();
        mainEngineAudio = FindObjectOfType<AudioManager>(); // Searches AudioManager script
    }

//UPDATE METHOD
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

// PUBLIC METHODS


// PRIVATE METHODS
    void ProcessThrust()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && PauseMenu.gameIsPaused == false && DialogueManager.initialTutorialIsRunning == false)
        {
            StartThrusting();
        } 
        else // if space bar is not pressed = below is when space bar is not pressed
        {
            StopThrusting();
        }
    }

    void StopThrusting()
    {
        mainEngineAudio.Stop("Main Engine SFX");
        mainEngineParticles.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // Time.deltaTime ensures that it moves at the same speed regardless the framerate of the computer.
        if (!mainEngineParticles.isPlaying) // if audio is not already playing then
        {
            mainEngineAudio.Play("Main Engine SFX");
            mainEngineParticles.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotatingLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotatingRight();
        }
        else
            StoppingRotating();
    }


    void RotatingLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rightEngineParticles.isPlaying)
        {
            rightEngineParticles.Play();
        }
    }

    void RotatingRight()
    {
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

}