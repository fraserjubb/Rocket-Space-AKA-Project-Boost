using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject spaceBarText;
    public GameObject rotateRightText;
    public GameObject rotateLeftText;

    bool spaceBarPressed = false;
    bool dButtonPressed = false;
    bool aButtonPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rotateRightText.SetActive(false);
        rotateLeftText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && spaceBarPressed == false)
        {
            spaceBarText.SetActive(false);
            spaceBarPressed = true;
            rotateRightText.SetActive(true);
        }
        if (Input.GetKey(KeyCode.D) && dButtonPressed == false && spaceBarPressed == true)
        {
            rotateRightText.SetActive(false);
            dButtonPressed = true;
            rotateLeftText.SetActive(true);
        }
        if (Input.GetKey(KeyCode.A) && aButtonPressed == false && dButtonPressed == true)
        {
            Debug.Log("I am being reached");
            aButtonPressed = true;
            rotateLeftText.SetActive(false);
        }
    }

}
