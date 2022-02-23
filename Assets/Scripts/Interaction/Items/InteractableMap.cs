using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class InteractableMap : MonoBehaviour, IInteractable
{
    public Map map;
    public Sprite mapImage;
    public GameObject pickUpSound;

    public void Interact()
    {
        map.SetMapImage(mapImage);
        FindObjectOfType<PlayerInventory>().Add(map);
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
                { "itemType", "Map" },
            };
            Events.CustomData("ItemPickUp", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'ItemPickUp' with: itemType = " + "Map");
        }
    }
}
