using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        DialogueTrigger tempDialogueTrigger;
        tempDialogueTrigger = GetComponent<DialogueTrigger>();
        if (!tempDialogueTrigger.isTriggered)
        {
            tempDialogueTrigger.TriggerDialogue();
        }
        else 
        {
            tempDialogueTrigger.TriggerNextSentence();
        }
    }
    // Gets the dialogue trigger on this object and runs the dialogue if it hasn't been ran already.
    // If the dialogue is ran already, the current line is skipped. 
}