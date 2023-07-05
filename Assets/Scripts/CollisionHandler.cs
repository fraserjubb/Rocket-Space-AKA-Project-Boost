using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Debug.Log("Sorry you've blown up");
                break;
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
