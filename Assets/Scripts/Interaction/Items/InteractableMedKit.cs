using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMedKit : MonoBehaviour, IInteractable
{
    public MedKit item;
    public GameObject pickUpSound;

    public void Interact() 
    {
        FindObjectOfType<PlayerInventory>().Add(item);
        PickUpSound();
        Destroy(gameObject);
    }

    void PickUpSound()
    {
        Instantiate(pickUpSound, transform.position, Quaternion.identity);
    }
}
