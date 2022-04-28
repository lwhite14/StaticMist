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

    public GameObject lockedPopUp;
    public GameObject openedPopUp;

    void Start() 
    {
        lockedPopUp.SetActive(true);
        openedPopUp.SetActive(false);
    }

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
                if (Application.isPlaying)
                {
                    DialogueTrigger.StopAllDialogue();
                    GetComponent<DialogueTrigger>().StartPopUp();
                }
            }
        }
    }

    public void CheckIfKey(Key key)
    {
        if (key.code == lockOneCode)
        {
            if (Application.isPlaying)
                AnalyticsFunctions.ItemUtilise("Key");
            FindObjectOfType<PlayerInventory>().inventory.RemoveItem(key);
            FindObjectOfType<PlayerInventory>().RefreshUI();
            if (Application.isPlaying)
                FindObjectOfType<InventoryUI>().SetViewedItem(null);
            Instantiate(unlockedSound, transform.GetChild(0).position, Quaternion.identity);
            slotOneLocked = false;
            if (Application.isPlaying)
                lockOneFlicker.StopFlicker();
        }
        else if (key.code == lockTwoCode) 
        {
            if (Application.isPlaying)
                AnalyticsFunctions.ItemUtilise("Key");
            FindObjectOfType<PlayerInventory>().inventory.RemoveItem(key);
            FindObjectOfType<PlayerInventory>().RefreshUI();
            if (Application.isPlaying)
                FindObjectOfType<InventoryUI>().SetViewedItem(null);
            Instantiate(unlockedSound, transform.GetChild(0).position, Quaternion.identity);
            slotTwoLocked = false;
            if (Application.isPlaying)
                lockTwoFlicker.StopFlicker();
        }
        else
        {
            FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
            FindObjectOfType<CoroutineHelper>().HelperStartExamining("THIS IS THE WRONG KEY...");
        }

        if (!slotOneLocked && !slotTwoLocked) 
        {
            lockedPopUp.SetActive(false);
            openedPopUp.SetActive(true);
        }
    }

    void TurnOn() 
    {
        gate.Unlock();
        Instantiate(powerUpSound, transform.GetChild(0).position, Quaternion.identity);
    }

}