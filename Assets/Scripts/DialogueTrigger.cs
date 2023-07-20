using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    float delayTime = Mathf.Epsilon;

    void Start()
    {
        TriggerDialogue();
        // Invoke("TriggerDialogue", delayTime);
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
