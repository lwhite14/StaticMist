using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public float popUpTime = 7.5f;
    bool isTriggered = false;

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue, this);
    }
    // Dialgoue manager runs the dialogue. 

    public void TriggerNextSentence() 
    {
        DialogueManager.instance.DisplayNextSentence();
    }
    // Dialgoue manager runs the next line of dialogue. 

    public void StartPopUp() 
    {
        StartCoroutine(PopUp());
    }

    IEnumerator PopUp() 
    {
        TriggerDialogue();
        yield return new WaitForSeconds(popUpTime);
        TriggerNextSentence();
        yield return null;
    }

    public static void StopAllDialogue() 
    {
        foreach (DialogueTrigger trigger in FindObjectsOfType<DialogueTrigger>()) 
        {
            trigger.StopAllCoroutines();
        }
        DialogueManager.instance.EndDialogue();
    }

    public void SetIsTriggered(bool newIsTriggered)
    {
        isTriggered = newIsTriggered;
    }
    // Changes the isTriggered value, useful if the player wants to skip the current line. 

    public bool GetIsTriggered() 
    {
        return isTriggered;
    }
}
