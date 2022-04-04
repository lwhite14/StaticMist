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
                DialogueManager.instance.EndDialogue();
                PopUp.StopAllPopUps();
                StopAllCoroutines();
                StartCoroutine(NoKeyDialgoue());
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

    public void CheckIfKey(Key key)
    {
        if (key.code == unlockCode)
        {
            key.SendDataToAnalytics();
            FindObjectOfType<PlayerInventory>().inventory.RemoveItem(key);
            FindObjectOfType<PlayerInventory>().RefreshUI();
            FindObjectOfType<InventoryUI>().SetViewedItem(null);
            Instantiate(unlockedSound, transform.GetChild(0).position, Quaternion.identity);
            isLocked = false;          
        }
        else 
        {
            FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
            FindObjectOfType<CoroutineHelper>().HelperStartExamining("THIS IS THE WRONG KEY...");
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
