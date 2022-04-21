using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class InteractableBat : MonoBehaviour, IInteractable
{
    public Bat bat;
    public GameObject pickUpSound;
    public DialogueTrigger tooManyItemsTrigger;

    public void Interact()
    {
        FindObjectOfType<PlayerInventory>().Add(bat, out bool success);
        if (success)
        {
            PickUpSound();
            if (Application.isPlaying)
            {
                AnalyticsFunctions.ItemPickUp("Bat");
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
