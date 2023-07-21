using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // INITIAL TUTORIAL VARIABLES, BOOLEANS & COLLECTIONS:
    public TextMeshProUGUI dialogueText;
    public GameObject initialTutorialText;
    public GameObject gameplayTutorialText;


    public static bool initialTutorialIsRunning;
    public static bool gameplayTutorialIsRunning;

    private Queue<string> sentences; //Queue is is a FiFo collection (first in, first out). When we load a new dialogue, all sentences will go in queue. This queue is of type string.


    // GAMEPLAY TUTORIAL VARIABLES, BOOLEANS:
    public GameObject spaceBarText;
    public GameObject rotateRightText;    
    public GameObject rotateLeftText;

    bool spaceBarPressed;
    bool dButtonPressed;
    bool aButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        spaceBarText.SetActive(false);
        rotateRightText.SetActive(false);
        rotateLeftText.SetActive(false);

        initialTutorialIsRunning = true;
        gameplayTutorialIsRunning = true;

        spaceBarPressed = false;
        dButtonPressed = false;
        aButtonPressed = false;


        sentences = new Queue<string>(); //inititate the Queue
    }

    void Update()
    {
        BoostingTutorial();
        RotateRightTutorial();
        RotateLeftTutorial();
        EnablePausingInTutorial();
    }


// METHODS:    
    void BoostingTutorial()
    {
        if (Input.GetKey(KeyCode.Space) && spaceBarPressed == false && DialogueManager.initialTutorialIsRunning == false && PauseMenu.gameIsPaused == false)
        {
            Time.timeScale = 1f;
            spaceBarText.SetActive(false);
            spaceBarPressed = true;
            rotateRightText.SetActive(true);
        }
    }

    void RotateRightTutorial()
    {
        if (Input.GetKey(KeyCode.D) && dButtonPressed == false && spaceBarPressed == true && PauseMenu.gameIsPaused == false)
        {
            rotateRightText.SetActive(false);
            dButtonPressed = true;
            rotateLeftText.SetActive(true);
        }
    }

    void RotateLeftTutorial()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentLevelIndex + 1;
        
        if ((Input.GetKey(KeyCode.A) && aButtonPressed == false && dButtonPressed == true && PauseMenu.gameIsPaused == false) || nextSceneIndex > 2) // If player manages to complete level 1 without pressing A, the tutorial will still end and not cause game to break.
        {
            aButtonPressed = true;
            rotateLeftText.SetActive(false);
            gameplayTutorialIsRunning = false;
        }
    }

    void EnablePausingInTutorial()
    {
        if (PauseMenu.gameIsPaused)
        {
            gameplayTutorialText.SetActive(false);
        } else if (PauseMenu.gameIsPaused == false)
        {
            gameplayTutorialText.SetActive(true);
        }
    }


    public void StartDialogue (Dialogue dialogue) // variable with same name in DialogueTrigger.cs
    {
        sentences.Clear(); // Ensure that any previous dialigue is cleared from the Queue

        Time.timeScale = 0f;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); //Add an object to end of the Queue - this will add all sentences in order to the Queue
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) // if no dialogue left, time to end the conversation
        {
            EndDialogue();
            return;
        } else if (Input.GetKey(KeyCode.Space)) // Space Bar will do nothing whilst initial tutorial is playing.
        {
            return; // DO NOTHING
        }

        string sentence = sentences.Dequeue(); // Remove and return the object at the beginning of the Queue - this will bring the next sentence onto screen.
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        if (Input.GetKey(KeyCode.Space)){return;} // Space Bar will continue to do nothing on final section of initial tutorial
        initialTutorialText.SetActive(false);
        initialTutorialIsRunning = false; //initial tutorial is now over
        spaceBarText.SetActive(true); // Allows for gameplay tutorial gameObject to display       
    }
}
