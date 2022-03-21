using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGoal : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        CheckIfKey();
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
                Goal();
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
