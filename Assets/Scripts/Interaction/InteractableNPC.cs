using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    public NPC npc;

    public void Interact()
    {
        npc.Interaction();
    }
    // Gets the dialogue trigger on this object and runs the dialogue if it hasn't been ran already.
    // If the dialogue is ran already, the current line is skipped. 
}