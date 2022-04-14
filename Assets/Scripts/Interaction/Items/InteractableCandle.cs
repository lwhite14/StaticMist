using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class InteractableCandle : MonoBehaviour, IInteractable
{
    public Candle candle;
    public GameObject pickUpSound;
    public DialogueTrigger tooManyItemsTrigger;

    public void Interact()
    {
        FindObjectOfType<PlayerInventory>().Add(candle, out bool success);
        if (success)
        {
            PickUpSound();
            if (Application.isPlaying)
            {
                AnalyticsFunctions.ItemPickUp("Candle");
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
