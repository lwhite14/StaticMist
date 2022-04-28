using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalGate : MonoBehaviour
{
    public bool isLocked { get; set; } = true;
    Animator anim;

    public GameObject lockedPopUp;
    public GameObject openedPopUp;

    private void Start()
    {
        anim = GetComponent<Animator>();

        lockedPopUp.SetActive(true);
        openedPopUp.SetActive(false);
    }

    public void Interactation() 
    {
        if (!isLocked)
        {
            anim.Play("Open");
            openedPopUp.GetComponent<ItemPopUp>().promptOn = false;
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

    public void Unlock() 
    {
        isLocked = false;
        lockedPopUp.SetActive(false);
        openedPopUp.SetActive(true);
    }
}
