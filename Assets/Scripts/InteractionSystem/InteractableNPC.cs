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
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
}