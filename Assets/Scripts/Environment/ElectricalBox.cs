using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : MonoBehaviour
{
    public ElectricalGate gate;
    public string lockOneCode;
    public string lockTwoCode;
    public GameObject unlockedSound;
    public GameObject powerUpSound;
    public LightFlicker lockOneFlicker;
    public LightFlicker lockTwoFlicker;
    bool slotOneLocked = true;
    bool slotTwoLocked = true;
    bool canInteract = true;

    public void Interact() 
    {
        if (canInteract)
        {
            if (!slotOneLocked && !slotTwoLocked)
            {
                TurnOn();
            }
            else
            {
                DialogueTrigger.StopAllDialogue();
                GetComponent<DialogueTrigger>().StartPopUp();
            }
        }
    }

    public void CheckIfKey(Key key)
    {
        if (key.code == lockOneCode)
        {
            key.SendDataToAnalytics();
            FindObjectOfType<PlayerInventory>().inventory.RemoveItem(key);
            FindObjectOfType<PlayerInventory>().RefreshUI();
            FindObjectOfType<InventoryUI>().SetViewedItem(null);
            Instantiate(unlockedSound, transform.GetChild(0).position, Quaternion.identity);
            slotOneLocked = false;
            lockOneFlicker.StopFlicker();
        }
        else if (key.code == lockTwoCode) 
        {
            key.SendDataToAnalytics();
            FindObjectOfType<PlayerInventory>().inventory.RemoveItem(key);
            FindObjectOfType<PlayerInventory>().RefreshUI();
            FindObjectOfType<InventoryUI>().SetViewedItem(null);
            Instantiate(unlockedSound, transform.GetChild(0).position, Quaternion.identity);
            slotTwoLocked = false;
            lockTwoFlicker.StopFlicker();
        }
        else
        {
            FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
            FindObjectOfType<CoroutineHelper>().HelperStartExamining("THIS IS THE WRONG KEY...");
        }
    }

    void TurnOn() 
    {
        gate.isLocked = false;
        Instantiate(powerUpSound, transform.GetChild(0).position, Quaternion.identity);
    }

}