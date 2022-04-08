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
            DialogueTrigger.StopAllDialogue();
            GetComponent<DialogueTrigger>().StartPopUp();
        }
    }

    void Goal()
    {
        GameManager.instance.Goal();
    }
}
