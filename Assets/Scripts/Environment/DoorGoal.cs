using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGoal : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        CheckIfKey();
    }

    void CheckIfKey() 
    {
        bool hasKey = false;
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        foreach (IItem item in playerInventory.inventory.GetAllItems()) 
        {
            if (item is Key) 
            {
                hasKey = true;
                Goal();
            }
        }
        if (!hasKey) 
        {
            print("you dont have a key");
        }
    }

    void Goal() 
    {
        FindObjectOfType<GameManager>().Goal();
    }
}
