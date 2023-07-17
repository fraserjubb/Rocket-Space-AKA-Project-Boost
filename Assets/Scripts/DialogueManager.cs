using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    bool spaceBarPressed = false;


    // Start is called before the first frame update
    void Start()
    {
        spaceBarText.SetActive(false);
        rotateRightText.SetActive(false);
        
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && spaceBarPressed == false && DialogueManager.initialTutorialIsRunning == false)
        {
            spaceBarText.SetActive(false);
            spaceBarPressed = true;
            rotateRightText.SetActive(true);
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
