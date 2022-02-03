using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableKey : MonoBehaviour, IInteractable
{
    public Key key;
    public GameObject pickUpSound;

    public void Interact() 
    {
        FindObjectOfType<PlayerInventory>().Add(key);
        PickUpSound();
        Destroy(gameObject);
    }

    void PickUpSound() 
    {
        Instantiate(pickUpSound, transform.position, Quaternion.identity);
    }
}
