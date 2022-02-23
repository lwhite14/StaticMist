using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class InteractableBat : MonoBehaviour, IInteractable
{
    public Bat bat;
    public GameObject pickUpSound;

    public void Interact()
    {
        FindObjectOfType<PlayerInventory>().Add(bat);
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
                { "itemType", "Bat" },
            };
            Events.CustomData("ItemPickUp", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'ItemPickUp' with: itemType = " + "Bat");
        }
    }
}
