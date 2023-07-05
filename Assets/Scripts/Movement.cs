using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
//VARIABLES 
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationSpeed = 150f;
    [SerializeField] AudioClip mainEngine;

    // CACHE - e.g. references in the script for readability or speed
    Rigidbody rb;
    AudioSource audioSource;
    
    // STATE - private instance (member) variables e.g. "bool isAlive"


//START METHOD
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        // audioSource.Play(0);
        // Debug.Log("started");
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
        if (Input.GetKey(KeyCode.Space))
        {
            // Debug.Log("Pressed SPACE - Thrusting");
            // rb.AddRelativeForce(0, mainThrust *Time.deltaTime, 0);
            // Alternative code:
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying) // if audio is not already playing then
            {
                audioSource.PlayOneShot(mainEngine);
            }
        } // Make sure to close bracket here as a seperate block = above is if space bar is pressed
        else // if space bar is not pressed = below is when space bar is not pressed
        {
            audioSource.Stop();
        }
        
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // Debug.Log("Pressed A - Rotate Left");
            ApplyRotation(rotationSpeed);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            // Debug.Log("Pressed D - Rotate Right");
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so that the physics system can take over.
    }
}