using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class InteractableMedKit : MonoBehaviour, IInteractable
{
    public MedKit item;
    public GameObject pickUpSound;
    public DialogueTrigger tooManyItemsTrigger;

    public void Interact() 
    {
        FindObjectOfType<PlayerInventory>().Add(item, out bool success);
        if (success)
        {
            PickUpSound();
            if (Application.isPlaying)
            {
                DialogueTrigger.StopAllDialogue();
                AnalyticsFunctions.ItemPickUp("MedKit");
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
