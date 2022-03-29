using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool isGoal = false;
    public bool isLocked = false;
    public GameObject squeekyDoorSound, unlockedSound;
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
            if (!isLocked)
            {
                ChangeOpenState();
            }
            else
            {
                CheckIfKey();
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

    void CheckIfKey()
    {
        bool hasKey = false;
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        foreach (IItem item in playerInventory.inventory.GetAllItems())
        {
            if (item is Key)
            {
                hasKey = true;
                item.Use();
                FindObjectOfType<PlayerInventory>().inventory.RemoveItem(item);
                FindObjectOfType<PlayerInventory>().RefreshUI();
                if (isGoal)
                {
                    Goal();
                }
                else
                {
                    isLocked = false;
                    ChangeOpenState();
                    Instantiate(unlockedSound, transform.GetChild(0).position, Quaternion.identity);
                }
            }
        }
        if (!hasKey)
        {
            DialogueManager.instance.EndDialogue();
            PopUp.StopAllPopUps();
            StopAllCoroutines();
            StartCoroutine(NoKeyDialgoue());
        }
    }

    void Goal()
    {
        GameManager.instance.Goal();
    }

    IEnumerator NoKeyDialgoue() 
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
        yield return new WaitForSeconds(5.0f);
        GetComponent<DialogueTrigger>().TriggerNextSentence();
        yield return null;
    }
}
