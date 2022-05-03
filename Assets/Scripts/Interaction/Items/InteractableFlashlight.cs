using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class InteractableFlashlight : MonoBehaviour, IInteractable
{
    public Flashlight flashlight;
    public GameObject pickUpSound;
    public DialogueTrigger tooManyItemsTrigger;

    public void Interact()
    {
        FindObjectOfType<PlayerInventory>().Add(flashlight, out bool success);
        if (success)
        {
            DialogueTrigger.StopAllDialogue();
            PickUpSound();
            if (Application.isPlaying)
            {
                AnalyticsFunctions.ItemPickUp("Flashlight");
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
}
