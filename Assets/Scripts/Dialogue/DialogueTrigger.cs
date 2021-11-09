using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [HideInInspector] public bool isTriggered = false;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
    }
    // Dialgoue manager runs the dialogue. 

    public void TriggerNextSentence() 
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }
    // Dialgoue manager runs the next line of dialogue. 

    public void SetIsTriggered(bool newIsTriggered) 
    {
        isTriggered = newIsTriggered;
    }
    // Changes the isTriggered value, useful if the player wants to skip the current line. 
}
