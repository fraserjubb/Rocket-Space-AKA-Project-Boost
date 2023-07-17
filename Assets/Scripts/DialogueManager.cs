using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // public Text nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject initialTutorialText;

    private Queue<string> sentences; //Queue is is a FiFo collection (first in, first out). When we load a new dialogue, all sentences will go in queue. This queue is of type string.

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
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
        Debug.Log("End of conversation");
    }
}
