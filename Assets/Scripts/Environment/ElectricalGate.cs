using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalGate : MonoBehaviour
{
    public bool isLocked = true;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interactation() 
    {
        if (!isLocked)
        {
            anim.Play("Open");
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
