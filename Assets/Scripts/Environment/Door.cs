using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    Animator anim;
    bool isOpen = false;

    void Start() 
    {
        anim = GetComponent<Animator>();
    }

    public void Interact() 
    {
        ChangeOpenState();
    }

    void ChangeOpenState() 
    {
        if (!isOpen)
        {
            isOpen = true;
        }
        else 
        {
            isOpen = false;
        }
        Open(isOpen);
    }

    void Open(bool newOpen) 
    {
        anim.SetBool("isOpen", newOpen);
    }
}
