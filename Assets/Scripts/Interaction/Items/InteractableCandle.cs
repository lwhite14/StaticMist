using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class InteractableCandle : MonoBehaviour, IInteractable
{
    public Candle candle;
    public GameObject pickUpSound;

    public void Interact()
    {
        FindObjectOfType<PlayerInventory>().Add(candle);
        PickUpSound();
        SendDataToAnalytics();
        Destroy(gameObject);
    }

    void PickUpSound()
    {
        Instantiate(pickUpSound, transform.position, Quaternion.identity);
    }

    void SendDataToAnalytics()
    {
        if (InitServices.isRecording)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "itemType", "Candle" },
            };
            Events.CustomData("ItemPickUp", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'ItemPickUp' with: itemType = " + "Flashlight");
        }
    }
}