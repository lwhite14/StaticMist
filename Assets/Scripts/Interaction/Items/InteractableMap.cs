﻿using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class InteractableMap : MonoBehaviour, IInteractable
{
    public Map map;
    public Sprite mapImage;
    public GameObject pickUpSound;
    public DialogueTrigger tooManyItemsTrigger;

    public void Interact()
    {
        Map tempMap = Instantiate(map);
        tempMap.map = mapImage;
        FindObjectOfType<PlayerInventory>().Add(tempMap, out bool success);
        if (success)
        {
            PickUpSound();
            SendDataToAnalytics();
            Destroy(gameObject);
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
