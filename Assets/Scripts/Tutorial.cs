using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject spaceBarText;
    public GameObject rotateDirectionText;

    bool spaceBarPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rotateDirectionText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && spaceBarPressed == false)
        {
            spaceBarText.SetActive(false);
            spaceBarPressed = true;
            rotateDirectionText.SetActive(true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("I am being reached");
            rotateDirectionText.SetActive(false);
        }
    }

}
