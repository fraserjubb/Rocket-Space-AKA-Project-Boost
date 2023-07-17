using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // INITIAL TUTORIAL VARIABLES, BOOLEANS & COLLECTIONS:
    // public Text nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject initialTutorialText;

    public static bool initialTutorialIsRunning = true;

    private Queue<string> sentences; //Queue is is a FiFo collection (first in, first out). When we load a new dialogue, all sentences will go in queue. This queue is of type string.


    // GAMEPLAY TUTORIAL VARIABLES, BOOLEANS:
    public GameObject spaceBarText;
    public GameObject rotateRightText;    
    public GameObject rotateLeftText;

    bool spaceBarPressed = false;
    bool dButtonPressed = false;
    bool aButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        spaceBarText.SetActive(false);
        rotateRightText.SetActive(false);
        rotateLeftText.SetActive(false);
        
        sentences = new Queue<string>();
    }

    void Update()
    {
        BoostingTutorial();
        RotateRightTutorial();
        RotateLeftTutorial();
    }


// METHODS:
    void BoostingTutorial()
    {
        if (Input.GetKey(KeyCode.Space) && spaceBarPressed == false && DialogueManager.initialTutorialIsRunning == false)
        {
            spaceBarText.SetActive(false);
            spaceBarPressed = true;
            rotateRightText.SetActive(true);
        }
    }

    void RotateRightTutorial()
    {
        if (Input.GetKey(KeyCode.D) && dButtonPressed == false && spaceBarPressed == true)
        {
            rotateRightText.SetActive(false);
            dButtonPressed = true;
            rotateLeftText.SetActive(true);
        }
    }

    void RotateLeftTutorial()
    {
        if (Input.GetKey(KeyCode.A) && aButtonPressed == false && dButtonPressed == true)
        {
            Debug.Log("I am being reached");
            aButtonPressed = true;
            rotateLeftText.SetActive(false);
        }
    }


    public void StartDialogue (Dialogue dialogue)
    {
        // Debug.Log("Starting conversation with" + dialogue.name);

        // nameText.text = dialogue.name;

        Time.timeScale = 0f;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) //no dialogue left so time to end the conversation
        {
            EndDialogue();
            return;
        } else if (Input.GetKey(KeyCode.Space))
        {
            return; // Space Bar will do nothing whilst initial tutorial is playing.
        }
        
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        initialTutorialText.SetActive(false);
        Time.timeScale = 1f;
        initialTutorialIsRunning = false;
        spaceBarText.SetActive(true);        
        Debug.Log("End of conversation");
    }
}
