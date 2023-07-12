using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition; // This will never change therfore no SerializeField required
    [SerializeField] Vector3 movementVector;
    // [SerializeField] [Range(0,1)] - if wanting a bar to edit in unity ranging between two numbers
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; // the current position
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) {return; }
        float cycles = Time.time / period; // how much time has elapsed divided by a period of time. This is continually growing over time.
        
        const float tau = Mathf.PI *2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) /2f; // recalculated to go from 0 to 1 so it's cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
