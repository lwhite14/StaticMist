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
        isTriggered = true;
    }

    public void SetIsTriggered(bool newIsTriggered) 
    {
        isTriggered = newIsTriggered;
    }
}
