using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // INITIAL TUTORIAL VARIABLES, BOOLEANS & COLLECTIONS:
    // public Text nameText;
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


        sentences = new Queue<string>();
    }

    void Update()
    {
        BoostingTutorial();
        RotateRightTutorial();
        RotateLeftTutorial();
        PausingTutorial();
    }


// METHODS:
    void PausingTutorial()
    {
            if (PauseMenu.gameIsPaused)
        {
            gameplayTutorialText.SetActive(false);
        } else if (PauseMenu.gameIsPaused == false)
        {
            gameplayTutorialText.SetActive(true);
        }

    }
    
    
    
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
        
        if ((Input.GetKey(KeyCode.A) && aButtonPressed == false && dButtonPressed == true && PauseMenu.gameIsPaused == false) || nextSceneIndex > 2)
        {
            Debug.Log("I am being reached");
            aButtonPressed = true;
            rotateLeftText.SetActive(false);
            gameplayTutorialIsRunning = false;
        }
    }


    public void StartDialogue (Dialogue dialogue)
    {
        // Debug.Log("Starting conversation with" + dialogue.name);

        // nameText.text = dialogue.name;

        sentences.Clear();

        Time.timeScale = 0f;

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
        if (Input.GetKey(KeyCode.Space)){return;}
        initialTutorialText.SetActive(false);
        initialTutorialIsRunning = false;
        spaceBarText.SetActive(true);        
        Debug.Log("End of conversation");
    }
}
