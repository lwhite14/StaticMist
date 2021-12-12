using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public GameObject squeekyDoorSound;
    Animator anim;
    bool isOpen = false;
    bool canInteract = true;

    void Start() 
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Interact() 
    {
        if (canInteract)
        {
            ChangeOpenState();
        }
    }

    void ChangeOpenState() 
    {
        SetCanInteract(false);
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
        Instantiate(squeekyDoorSound, transform.GetChild(0).position, Quaternion.identity);
    }

    void SetCanInteract(bool newCanInteract) 
    {
        canInteract = newCanInteract;
    }

    void EndOfAnimation() 
    {
        SetCanInteract(true);
    }
}
