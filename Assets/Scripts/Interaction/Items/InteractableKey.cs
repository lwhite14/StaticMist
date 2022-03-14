using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class InteractableKey : MonoBehaviour, IInteractable
{
    public Key key;
    public GameObject pickUpSound;
    public bool isTutorial = false;

    public void Interact() 
    {
        FindObjectOfType<PlayerInventory>().Add(key);
        PickUpSound();
        if (isTutorial) 
        {
            DialogueManager.instance.EndDialogue();
            StopAllCoroutines();
            StartCoroutine(TutorialKey());
        }
        SendDataToAnalytics();
        Destroy(gameObject);
    }

    void PickUpSound() 
    {
        Instantiate(pickUpSound, transform.position, Quaternion.identity);
    }

    IEnumerator TutorialKey()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
        yield return new WaitForSeconds(5.0f);
        GetComponent<DialogueTrigger>().TriggerNextSentence();
        yield return null; 
    }

    void SendDataToAnalytics() 
    {
        if (InitServices.isRecording)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "itemType", "Key" },
            };
            Events.CustomData("ItemPickUp", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'ItemPickUp' with: itemType = " + "Key");
        }
    }
}
