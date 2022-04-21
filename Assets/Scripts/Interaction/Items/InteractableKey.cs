using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class InteractableKey : MonoBehaviour, IInteractable
{
    public Key key;
    public GameObject pickUpSound;
    public bool isTutorial = false;
    public string code = ""; 
    public DialogueTrigger tutorialTrigger;
    public DialogueTrigger tooManyItemsTrigger;

    public void Interact()
    {
        Key tempKey = Instantiate(key);
        tempKey.code = code;
        FindObjectOfType<PlayerInventory>().Add(tempKey, out bool success);
        if (success)
        {
            PickUpSound();
            TutorialDialogue();
            if (Application.isPlaying)
            {
                AnalyticsFunctions.ItemPickUp("Key");
                Destroy(gameObject);
            }
        }
        else
        {
            tooManyItemsTrigger.StartPopUp();
        }
    }

    void PickUpSound() 
    {
        Instantiate(pickUpSound, transform.position, Quaternion.identity);
    }

    void TutorialDialogue() 
    {
        if (isTutorial)
        {
            DialogueTrigger.StopAllDialogue();
            GetComponent<DialogueTrigger>().StartPopUp();
        }
    }
}
