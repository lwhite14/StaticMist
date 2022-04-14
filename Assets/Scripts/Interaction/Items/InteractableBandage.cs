using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class InteractableBandage : MonoBehaviour, IInteractable
{
    public Bandage bandage;
    public GameObject pickUpSound;
    public DialogueTrigger tooManyItemsTrigger;

    public void Interact()
    {
        FindObjectOfType<PlayerInventory>().Add(bandage, out bool success);
        if (success)
        {
            PickUpSound();
            if (Application.isPlaying)
            {
                AnalyticsFunctions.ItemPickUp("Bandage");
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
