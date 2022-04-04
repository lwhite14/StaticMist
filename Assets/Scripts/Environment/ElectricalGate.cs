using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalGate : MonoBehaviour, IInteractable
{
    public bool isLocked = true;

    public void Interact() 
    {
        if (!isLocked)
        {
            Goal();
        }
        else 
        {
            DialogueManager.instance.EndDialogue();
            PopUp.StopAllPopUps();
            StopAllCoroutines();
            StartCoroutine(LockedDialogue());
        }
    }

    void Goal()
    {
        GameManager.instance.Goal();
    }

    IEnumerator LockedDialogue()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
        yield return new WaitForSeconds(5.0f);
        GetComponent<DialogueTrigger>().TriggerNextSentence();
        yield return null;
    }
}
