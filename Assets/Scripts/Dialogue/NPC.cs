using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public Animator anim;
    bool isCowering = false;

    public void Interaction() 
    {
        if (!dialogueTrigger.GetIsTriggered())
        {
            if (!isCowering)
            {
                DialogueTrigger.StopAllDialogue();
                DialogueManager.instance.isTalkingNPC = true;
                dialogueTrigger.TriggerDialogue();
            }
        }
        else
        {
            dialogueTrigger.TriggerNextSentence();
        }
    }
    // Gets the dialogue trigger on this object and runs the dialogue if it hasn't been ran already.
    // If the dialogue is ran already, the current line is skipped. 


    public void Cower() 
    {
        isCowering = true;
        anim.SetBool("isCowering", true);
    }

    public void Idle() 
    {
        isCowering = false;
        anim.SetBool("isCowering", false);
    }

    public bool GetCowering() 
    {
        return isCowering;
    }

    public static void PlayerChased() 
    {
        foreach (NPC npc in FindObjectsOfType<NPC>()) 
        {
            if (!npc.GetCowering())
            {
                npc.Cower();
                DialogueManager.instance.EndDialogue();
            }
        }
    }

    public static void PlayerEscaped()
    {
        foreach (NPC npc in FindObjectsOfType<NPC>())
        {
            if (npc.GetCowering())
            {
                npc.Idle();
            }
        }
    }
}
