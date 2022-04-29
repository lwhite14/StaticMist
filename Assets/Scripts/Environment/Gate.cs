using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool isGoal = false;
    public bool isLocked = false;
    public string unlockCode = "";
    public GameObject squeekyDoorSound, unlockedSound;
    Animator anim;
    public bool isOpen { get; private set; } = false;
    bool canInteract = true;

    public GameObject lockedPopUp;
    public GameObject openedPopUp;

    void Start() 
    {
        anim = GetComponentInChildren<Animator>();

        if (isLocked)
        {
            lockedPopUp.SetActive(true);
        }
        else 
        {
            openedPopUp.SetActive(true);
        }
    }

    public void Interact() 
    {
        if (canInteract)
        {
            if (!isLocked)
            {
                if (isGoal)
                {
                    Goal();
                }
                else
                {
                    ChangeOpenState();
                }
            }
            else 
            {
                DialogueTrigger.StopAllDialogue();
                GetComponent<DialogueTrigger>().StartPopUp();
            }
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
        if (Application.isPlaying)
        {
            anim.SetBool("isOpen", newOpen);
            Instantiate(squeekyDoorSound, transform.GetChild(0).position, Quaternion.identity);
        }
    }

    void SetCanInteract(bool newCanInteract) 
    {
        canInteract = newCanInteract;
    }

    void EndOfAnimation() 
    {
        SetCanInteract(true);
    }

    public void CheckIfKey(Key key)
    {
        if (key.code == unlockCode)
        {
            if (Application.isPlaying)
                AnalyticsFunctions.ItemUtilise("Key");     
            FindObjectOfType<PlayerInventory>().inventory.RemoveItem(key);
            FindObjectOfType<PlayerInventory>().RefreshUI();
            if (Application.isPlaying)
                FindObjectOfType<InventoryUI>().SetViewedItem(null);
            Instantiate(unlockedSound, transform.GetChild(0).position, Quaternion.identity);
            isLocked = false;

            lockedPopUp.SetActive(false);
            openedPopUp.SetActive(true);
        }
        else 
        {
            if (Application.isPlaying)
            {
                FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
                FindObjectOfType<CoroutineHelper>().HelperStartExamining("THIS IS THE WRONG KEY...");
            }
        }
    }

    void Goal()
    {
        GameManager.instance.Goal();
    }
}
